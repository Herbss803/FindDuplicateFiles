open System
open FileUtilities

// For more information see https://aka.ms/fsharp-console-apps

let args = Environment.GetCommandLineArgs();
let path = args[1]
let files = Finder.getFilesWithExtension ".m2ts" path

for f in files do
    printfn "%s" f


