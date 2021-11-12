using System.Security.Cryptography;
using System.Text;

namespace FindDuplicateFiles;

public class FindDupes
{
    public static void FindDupesAndWriteToFile(string path)
    {
        Console.WriteLine($"Scanning path {path}");
        var dictionary = new Dictionary<string, List<FileHashEntry>>();

        FindDupes.RecursiveBuildHash(path, dictionary);
        var filesToDelete = FindDupes.SelectFilesToDelete(dictionary);
        foreach (var f in filesToDelete)
        {
            Console.WriteLine($"Delete: {f}");
        }
        File.WriteAllLines("filesToDelete.txt", filesToDelete);
    }

    public static void DeleteFilesFromFileList(string fileListPath)
    {
        var filesToDelete = File.ReadAllLines(fileListPath);

        long totalLength = 0;
        foreach (var f in filesToDelete)
        {
            Console.WriteLine($"Delete {f}");
            long length = new System.IO.FileInfo(f).Length;
            totalLength += length;
        }
        Console.WriteLine($"Deleting files could save {totalLength}");

        foreach (var f in filesToDelete)
        {
            File.Delete(f);
        }
    }

    public static void RecursiveBuildHash(string path, Dictionary<string, List<FileHashEntry>> dictionary)
    {
        var results = Directory.GetFileSystemEntries(path);
        foreach (var r in results)
        {
            var attr = File.GetAttributes(r);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                // recurse
                Console.WriteLine($"Recursing down folder {r}");
                RecursiveBuildHash(r, dictionary);
            }
            else
            {
                Console.WriteLine($"Hashing file {r}");
                var hashEntry = HashFile(r);
                UpdateDictionary(dictionary, hashEntry);
            }
        }
    }

    public static List<string> SelectFilesToDelete(Dictionary<string, List<FileHashEntry>> dictionary)
    {
        var filesToDelete = new List<string>();
        foreach (var entry in dictionary)
        {
            var fileHashEntries = entry.Value;
            if (fileHashEntries.Count > 1)  // at least 2
            {
                var sortedByNameLength = fileHashEntries.OrderBy(e => e.FileName.Length).ToArray();
                filesToDelete.AddRange(sortedByNameLength.TakeLast(fileHashEntries.Count - 1).Select(e => e.FileName));
            }
        }
        return filesToDelete;
    }

    private static void UpdateDictionary(Dictionary<string, List<FileHashEntry>> dictionary, FileHashEntry hashEntry)
    {
        if (!dictionary.ContainsKey(hashEntry.FileHash))
        {
            dictionary.Add(hashEntry.FileHash, new List<FileHashEntry> { hashEntry });
        }
        else
        {
            dictionary[hashEntry.FileHash].Add(hashEntry);
        }
    }

    private static FileHashEntry HashFile(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        var entry = new FileHashEntry
        {
            FileName = path,
            FileHash = Encoding.UTF8.GetString(new SHA1Managed().ComputeHash(fs))
        };
        return entry;
    }
}