// Copyright 2018 Fabulous contributors. See LICENSE.md for license.
namespace AppsolutelyFabulous

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms

module App = 

    // Helper
    let inline mapIf pred f =
        List.map (fun x -> if pred x then f x else x)

    type ToDoItem = 
      { 
        ID : int
        Title : string
        Description: string
        Done : bool
      }

    type Model = {
        Items : ToDoItem list
    }

    let initModel = {
        Items = [
            { ID = 1; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 2; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 3; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
            { ID = 4; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 5; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 6; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
            { ID = 7; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 8; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 9; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }        
            { ID = 10; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 11; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 12; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }        
            { ID = 13; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 14; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 15; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
        ]
    }

    type Msg = 
        | DoneChanged of int * bool

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | DoneChanged (itemId, newState) ->  { model with Items = (mapIf (fun x -> x.ID = itemId) (fun item -> { item with Done = newState }) model.Items) }, Cmd.none

    let toDoItemView dispatch item =
        View.StackLayout(
            orientation = StackOrientation.Horizontal,
            children = [
                View.Switch(isToggled = item.Done, toggled = (fun args -> dispatch (DoneChanged(item.ID, not item.Done))))
                View.Label(
                    item.Title,  
                    horizontalOptions = LayoutOptions.Start, 
                    horizontalTextAlignment=TextAlignment.Start,
                    textDecorations = if item.Done then TextDecorations.Strikethrough else TextDecorations.None
                )
            ]
        )

    let toDoView dispatch items = 
        let toDoItemView = toDoItemView dispatch
        List.map toDoItemView items

    let view (model: Model) dispatch =
        View.ContentPage(
            View.ScrollView(
                content = View.StackLayout(
                    padding = 20.0,
                    verticalOptions = LayoutOptions.Start,
                    children = toDoView dispatch model.Items
                )
            )
        )

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

// So what if we want something a bit more complex
// Master detail with a more complex view model
// TODO Item
// Some kind of list
// Detail page...