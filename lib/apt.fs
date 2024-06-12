module apt

open System.IO

let apt =
    File.WriteAllLines(
        @"meta/apt.txt",
        [ "git make curl"
          "code meld doxygen clang-format"
          "g++ flex bison libreadline-dev" ]
    )
