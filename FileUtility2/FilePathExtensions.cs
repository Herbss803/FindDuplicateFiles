namespace FileUtility2;

public static class FilePathExtensions
{
    public static bool PathIsDirectory(this string path)
    {
        var attr = File.GetAttributes(path);
        return (attr & FileAttributes.Directory) == FileAttributes.Directory;
    }
}