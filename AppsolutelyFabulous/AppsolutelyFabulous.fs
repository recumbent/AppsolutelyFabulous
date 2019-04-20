﻿// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace AppsolutelyFabulous

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 
    type Model = 
      { name : string
      }

    type Msg = 
        | NameChanged of string

    let initModel = { name = "World!" }

    let init () = initModel, Cmd.none

    let update (msg: Msg) model =
        model, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
            children = [ 
                View.Label(text = sprintf "Hello %s" model.name, horizontalOptions = LayoutOptions.Start, horizontalTextAlignment=TextAlignment.Start)
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

