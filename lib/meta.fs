open System.IO

for i in [ "meta" ] do
    Directory.CreateDirectory(i) |> ignore
