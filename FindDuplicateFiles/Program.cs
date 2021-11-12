// See https://aka.ms/new-console-template for more information
// string[] args

using FindDuplicateFiles;

try
{
    var path = args[0];
    if (string.IsNullOrEmpty(path))
    {
        throw new Exception("Please pass path as first arg");
    }

    // FindDupes.FindDupesAndWriteToFile(path);
    FindDupes.DeleteFilesFromFileList("filesToDelete.txt");

    var readLine = Console.ReadLine();
}
catch (Exception e)
{
    throw new Exception("Please pass path as first argument");
}