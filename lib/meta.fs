open System.IO

let dirs =
    [ ""; "/.vscode"; "/bin"; "/doc"; "/lib"; "/inc"; "/src"; "/ref"; "/tmp" ]

let dir =

    for i in dirs do
        Directory.CreateDirectory("meta" + i) |> ignore

dir

let giti =
    for i in dirs do
        File.WriteAllLines("meta" + i + "/.gitignore", [])

giti

let apt =
    File.WriteAllLines(
        @"meta/apt.txt",
        [ "git make curl"
          "code meld doxygen clang-format"
          "g++ flex bison libreadline-dev" ]
    )

apt
