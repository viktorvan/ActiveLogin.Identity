#r "paket:
nuget FSharp.Core 4.3.4
nuget Fake.Core.Target
nuget Fake.DotNet.Cli
nuget Fake.Dotnet.Testing.Expecto
nuget Fake.IO.FileSystem //"
#load "./.fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "netstandard"
#r "Facades/netstandard" // https://github.com/ionide/ionide-vscode-fsharp/issues/839#issuecomment-396296095

#endif

#nowarn "52"

open System
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators


Target.create "Clean" (fun _ ->
        !! "src/**/bin/**/*"
        ++ "src/**/obj/**/*"
        ++ "test/**/bin/**/*"
        ++ "test/**/obj/**/*"
        |> File.deleteAll
    )
Target.create "Build" (fun _ -> DotNet.build id "ActiveLogin.Identity.sln")
Target.create "Test" (fun _ -> DotNet.test id "test/ActiveLogin.Identity.Swedish.Test")
Target.create "XUnit" (fun _ -> DotNet.test id "test/ActiveLogin.Identity.Swedish.Xunit")
Target.create "FsUnit" (fun _ -> DotNet.test id "test/ActiveLogin.Identity.Swedish.FsUnit")

"Build"
    ==> "Test"


Target.runOrDefaultWithArguments "Build"
