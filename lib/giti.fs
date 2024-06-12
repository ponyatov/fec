module giti

open System.IO

let giti dirs =
    let mask =
        Map["", [ "*~"; "*.swp"; "*.log"; ""; "/docs/"; "/obj/"; "/meta/"; "" ]
            "/.vscode", []
            "/bin", [ "*" ]
            "/doc", []
            "/inc", []
            "/lib", []
            "/src", []
            "/tmp", [ "*" ]
            "/ref", [ "*" ]]

    for i in dirs do
        File.WriteAllLines("meta" + i + "/.gitignore", mask[i] @ [ "!.gitignore" ])
