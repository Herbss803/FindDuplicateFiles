namespace FileUtilities
open System
open System.IO

module Finder =
    let fileHasExtension ext filename = 
        if String.Compare(Path.GetExtension(filename:string),ext, StringComparison.InvariantCultureIgnoreCase) = 0
        then Some(filename)
        else None
    
    let rec getFilesWithExtension ext filePath = 
        let filterForExtension = fileHasExtension ext
        let files = Directory.GetFiles filePath
        let filesMatchingExtension = files |> Array.choose filterForExtension
        let dirs = Directory.GetDirectories filePath
        Array.fold (fun acc d -> 
                    let subfolderResults = getFilesWithExtension ext d
                    Array.append subfolderResults acc
                    ) filesMatchingExtension dirs
