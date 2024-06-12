open System.IO

open config

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
        $"meta/inc/{MODULE}.hpp",
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
        $"meta/src/{MODULE}.cpp",
        [ //
          $"#include \"{MODULE}.hpp\""
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
        $"meta/lib/{MODULE}.ini",
        [ //
          "# line comment"
          ""
          "nop halt" ]
    )

ini

let readme = //
    File.WriteAllLines(
        @"meta/README.md",
        [ $"# `{MODULE}`"
          $"## {TITLE}"
          ""
          $"(c) {AUTHOR} <<{EMAIL}>> {YEAR} {LIC}"
          ""
          $"github: https://github.com/ponyatov/{MODULE}" ]
    )

readme

let cf = //
    File.WriteAllLines(
        @"meta/.clang-format",
        [ "
BasedOnStyle: Google
IndentWidth:  4
TabWidth:     4
UseTab:       Never
ColumnLimit:  80
UseCRLF:      false

SortIncludes: false

AllowShortBlocksOnASingleLine: Always
AllowShortFunctionsOnASingleLine: All" ]
    )

cf

let mk = //

    let var = [ "# var"; "MODULE  = $(notdir $(CURDIR))"; "" ]

    let dirs =
        [ //
          "# dirs"
          "CWD = $(CURDIR)"
          "BIN = $(CWD)/bin"
          "DOC = $(CWD)/doc"
          "SRC = $(CWD)/src"
          "TMP = $(CWD)/tmp"
          "GZ  = $(HOME)/gz"
          "" ]

    let tool =
        [ //
          "# tool"
          "CURL   = curl -L -o"
          "CF     = clang-format -style=file -i"
          "" ]

    let src =
        [ //
          "# src"
          "C += $(wildcard src/*.c*)"
          "H += $(wildcard inc/*.h*)"
          "S += lib/$(MODULE).ini $(wildcard lib/*.f)"
          "" ]

    let cfg = [ "# cfg"; "" ]
    let all = [ "# all"; "" ]
    let format = [ "# format"; "" ]
    let rule = [ "# rule"; "" ]
    let doc = [ "# doc"; "" ]
    let install = [ "# install"; "" ]
    let merge = [ "# merge"; "" ]
    File.WriteAllLines(@"meta/Makefile", var @ dirs @ tool @ src @ cfg @ all @ format @ rule @ doc @ install @ merge)

mk

open lic
lic YEAR AUTHOR
