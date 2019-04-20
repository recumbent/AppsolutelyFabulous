// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace AppsolutelyFabulous

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 
    type Model = 
      { Name : string
      }

    type Msg = 
        | NameChanged of string

    let initModel = { Name = "World!" }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | NameChanged newName -> { model with Name = newName }, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
            content = View.StackLayout(
                padding = 20.0,
                verticalOptions = LayoutOptions.Center,
                children = [ 
                    View.Label(sprintf "Hello %s" model.Name, horizontalOptions = LayoutOptions.Start, horizontalTextAlignment=TextAlignment.Start)
                    View.Entry(
                        text = model.Name,
                        textChanged = (fun args -> dispatch (NameChanged(args.NewTextValue)))
                    )
            ]))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.runWithDynamicView app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

