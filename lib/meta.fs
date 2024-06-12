open System.IO

let dirs =
    [ ""; "/.vscode"; "/bin"; "/doc"; "/lib"; "/inc"; "/src"; "/ref"; "/tmp" ]

let dir =

    for i in dirs do
        Directory.CreateDirectory("meta" + i) |> ignore

dir

let giti =
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

giti

let apt =
    File.WriteAllLines(
        @"meta/apt.txt",
        [ "git make curl"
          "code meld doxygen clang-format"
          "g++ flex bison libreadline-dev" ]
    )

apt

let vscode =
    for i in [ "settings"; "extensions"; "tasks" ] do
        File.WriteAllLines("meta/.vscode/" + i + ".json", [])

vscode
