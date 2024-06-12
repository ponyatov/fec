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
    for i in [ "c_cpp_properties"; "settings"; "extensions"; "tasks" ] do
        File.WriteAllLines("meta/.vscode/" + i + ".json", [])

vscode


let hpp =
    File.WriteAllLines(
        @"meta/inc/hpp.hpp",
        [ "#pragma once"
          ""
          "#include <stdio.h>"
          "#include <stdlib.h>"
          "#include <assert.h>"
          ""
          "extern int main(int argc, char *argv[]);"
          "extern void arg(int argc, char *argv);" ]
    )

hpp

let cpp =
    File.WriteAllLines(
        @"meta/src/cpp.cpp",
        [ //
          "#include \"hpp.hpp\""
          ""
          "int main(int argc, char *argv[]) {"
          "    arg(0, argv[0]);"
          "    for (int i = 1; i < argc; i++) {  //"
          "        arg(i, argv[i]);"
          "    }"
          "    return 0;"
          "}"
          ""
          "void arg(int argc, char *argv) {  //"
          "    printf(\"argv[%i] = <%s>\\n\", argc, argv);"
          "}" ]
    )

cpp

let ini = //
    File.WriteAllLines(
        @"meta/lib/ini.ini",
        [ //
          "# line comment"
          ""
          "nop halt" ]
    )

ini
