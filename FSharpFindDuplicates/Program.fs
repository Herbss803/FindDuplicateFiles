open System
open System.IO

// For more information see https://aka.ms/fsharp-console-apps

let args = Environment.GetCommandLineArgs();

let fileHasExtension ext filename = 
    if String.Compare(Path.GetExtension(filename:string),ext, StringComparison.InvariantCultureIgnoreCase) = 0
    then Some(filename)
    else None

let rec getFilesWithExtension ext filePath accumulator = 
    let filterForExtension = fileHasExtension ext
    let files = Directory.GetFiles filePath
    let filesMatchingExtension = files |> Array.choose filterForExtension
    let arr = Array.append accumulator filesMatchingExtension
    let dirs = Directory.GetDirectories filePath
    dirs |> Array.map (func d)
    for d in dirs do
        let subAccumulator = getFilesWithExtension ext filePath arr

    //Array.append arr dirs
    //for d in dirs 
    //    getFilesWithExtension ext filePath arr

    //Array.fold (fun (_x:Map<string, string>) y -> _x.Add(y,y)) foldMap dirs

let path = args[1]
let files = getFilesWithExtension ".txt" path Array.empty

for f in files do
    printfn "%s" f

//let map = Map
//getFilesWithExtension "mkv" args[1] map


//    let filesList = files |> List.ofArray
//    let sizes = List.map (fun f -> FileInfo(f).Length ) filesList
//    List.sum sizes

//let fileSystemEntries = Directory.GetFileSystemEntries args[1]
//for f in fileSystemEntries do
//    printfn $"{f}"

//let fileSize = getFileSize args[1]
//printfn "total file size is: %d" fileSize


