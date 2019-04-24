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

    let inline updateItem item items =
        item :: (List.filter (fun i -> i.ID <> item.ID) items)
        
    type PageType =
        | MainPage
        | EditItemPage of ToDoItem

    type Model = {
        Items : ToDoItem list
        CurrentPage: PageType
    }

    let initModel = {
        Items = [
            { ID = 1; Title = "First thing to do"; Description = "Something to be done"; Done = false }
            { ID = 2; Title = "Second thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 3; Title = "Third thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
            { ID = 4; Title = "Fourth thing to do"; Description = "Something to be done"; Done = false }
            { ID = 5; Title = "Fifth thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 6; Title = "Sixth thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
            { ID = 7; Title = "Seventh thing to do"; Description = "Something to be done"; Done = false }
            { ID = 8; Title = "Eighth thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 9; Title = "Nineth thing to do"; Description = "What do you mean there are three things to do?"; Done = false }        
            { ID = 10; Title = "10th thing to do"; Description = "Something to be done"; Done = false }
            { ID = 11; Title = "11th thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 12; Title = "12th thing to do"; Description = "What do you mean there are three things to do?"; Done = false }        
            { ID = 13; Title = "13th thing to do"; Description = "Something to be done"; Done = false }
            { ID = 14; Title = "14th thing to do"; Description = "Another thing to be done"; Done = false }
            { ID = 15; Title = "15th thing to do"; Description = "What do you mean there are three things to do?"; Done = false }
        ]
        CurrentPage = MainPage
    }

    type Msg = 
        | DoneChanged of int * bool
        | ItemSelected of int
        | EditTitleChanged of string
        | EditDescriptionChanged of string
        | EditSave
        | EditCancel

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | DoneChanged (itemId, newState) ->  { model with Items = (mapIf (fun x -> x.ID = itemId) (fun item -> { item with Done = newState }) model.Items) }, Cmd.none
        | ItemSelected itemId -> 
            let itemToEdit = List.find (fun i -> i.ID = itemId) model.Items
            { model with CurrentPage = EditItemPage(itemToEdit) }, Cmd.none
        | EditTitleChanged newText ->
            let oldItem = 
                match model.CurrentPage with
                | EditItemPage item -> item
                | _ -> { ID = 0; Title = "Seven impossible things"; Description = "This can't be right"; Done = true }
            { model with CurrentPage = EditItemPage({ oldItem with Title = newText }) }, Cmd.none
        | EditDescriptionChanged newText ->
            let oldItem = 
                match model.CurrentPage with
                | EditItemPage item -> item
                | _ -> { ID = 0; Title = "Seven impossible things"; Description = "This can't be right"; Done = true }
            { model with CurrentPage = EditItemPage({ oldItem with Description = newText }) }, Cmd.none
        | EditSave ->
            let editItem = 
                match model.CurrentPage with
                | EditItemPage item -> item
                | _ -> { ID = 0; Title = "Seven impossible things"; Description = "This can't be right"; Done = true }
            { model with CurrentPage = MainPage; Items = (updateItem editItem model.Items)}, Cmd.none            
        | EditCancel ->
            { model with CurrentPage = MainPage }, Cmd.none

    let toDoItemView dispatch item =
        View.StackLayout(
            orientation = StackOrientation.Horizontal,
            children = [
                View.Switch(isToggled = item.Done, toggled = (fun args -> dispatch (DoneChanged(item.ID, not item.Done))))
                View.Label(
                    item.Title,  
                    horizontalOptions = LayoutOptions.Start, 
                    horizontalTextAlignment=TextAlignment.Start,
                    textDecorations = (if item.Done then TextDecorations.Strikethrough else TextDecorations.None),
                    gestureRecognizers = [
                        View.TapGestureRecognizer(command = (fun () -> dispatch (ItemSelected(item.ID))))
                    ]
                )
            ]
        )

    let toDoView dispatch items = 
        let toDoItemView = toDoItemView dispatch
        List.map toDoItemView items

    let toDoEditView dispatch item =
        View.ContentPage(
           View.StackLayout(
                children = [
                    //View.Label("Title"),
                    View.Entry(text = item.Title, textChanged = (fun args -> dispatch (EditTitleChanged(args.NewTextValue))))
                    View.Label("Description")
                    View.Editor(text = item.Description, textChanged = (fun args -> dispatch (EditDescriptionChanged(args.NewTextValue))))
                    View.StackLayout(
                        orientation = StackOrientation.Horizontal,
                        children = [
                            View.Button(
                                text = "Save",
                                command = (fun () -> dispatch(EditSave))
                            )
                            View.Button(text = "Cancel", command = (fun () -> dispatch(EditCancel)))
                        ]
                     )
                ]
            )
        )

    let view (model: Model) dispatch =
        match model.CurrentPage with
        | MainPage ->
            View.ContentPage(
                View.ScrollView(
                    content = View.StackLayout(
                        padding = 20.0,
                        verticalOptions = LayoutOptions.Start,
                        children = [
                            View.ListView(
                                items = toDoView dispatch model.Items,
                                itemTapped = (fun args -> dispatch (ItemSelected(args)))
                            )
                        ]
                    )
                )
            )
        | EditItemPage editItem ->
            toDoEditView dispatch editItem

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
    do runner.EnableLiveUpdate()
#endif    

// So what if we want something a bit more complex
// Master detail with a more complex view model
// TODO Item
// Some kind of list
// Detail page...