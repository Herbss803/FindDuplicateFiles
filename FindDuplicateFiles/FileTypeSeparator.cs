using FileUtility2;

namespace FindDuplicateFiles;

public class FileTypeSeparator
{
    public static List<string> FindAllFilesByExtension(string path, ICollection<string> extensionFilter)
    {
        var acc = new List<string>();
        RecursiveFindFiles(path, extensionFilter, acc);
        return acc;
    }

    public static void MoveFilesToPath(string destFolder, ICollection<string> sourceFiles)
    {
        Directory.CreateDirectory(destFolder);
        var total = sourceFiles.Count;
        var index = 0;
        foreach (var f in sourceFiles)
        {
            index++;
            var targetFolderPath = PathHelpers.GetTargetPath(f, destFolder);
            var dirSection = Path.GetDirectoryName(targetFolderPath);
            Directory.CreateDirectory(dirSection);
            Console.WriteLine($"Moving to {targetFolderPath} {index}/{total}");
            File.Move(f, targetFolderPath, true);
        }
    }

    private static void RecursiveFindFiles(string path, ICollection<string> extensionFilters,
        ICollection<string> accumulator)
    {
        var folderFiles = Directory.GetFileSystemEntries(path);
        foreach (var r in folderFiles)
        {
            if (r.PathIsDirectory())
            {
                RecursiveFindFiles(r, extensionFilters, accumulator);
            }
            else
            {
                var fileExtension = Path.GetExtension(r);
                if (extensionFilters.Any(f =>
                        f.Equals(fileExtension, StringComparison.InvariantCultureIgnoreCase)))
                {
                    accumulator.Add(r);
                }
            }
        }
    }
}