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

    // Use case 1
    FindDupes.FindDupesAndWriteToFile(path);
    FindDupes.DeleteFilesFromFileList("filesToDelete.txt");

    // Use case 2
    //var foundFiles = FileTypeSeparator.FindAllFilesByExtension(path, new List<string>() { ".m2ts", ".mp4", ".MTS", ".modd", ".mov", ".MOV", ".moff", ".m4v", ".wmv" });
    //FileTypeSeparator.MoveFilesToPath("d:\\MasterBackupVideos", foundFiles);

    // Use case 3
    // FileTypeSeparator.FindAllFileTypes(path);

    var readLine = Console.ReadLine();
}
catch (Exception e)
{
    throw new Exception("Please pass path as first argument");
}