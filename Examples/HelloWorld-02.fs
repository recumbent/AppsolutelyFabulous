namespace HelloWorld

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open System

module App = 
    type Model = 
      { Name : string 
        Time : DateTime
      }

    type Msg = 
        | NameChanged of string
        | TimeChanged of DateTime

    let initModel = 
        { Name = "World"; 
          Time = DateTime.MinValue
        }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
        | NameChanged newName ->
            { model with Name = newName }, Cmd.none
        | TimeChanged newTime ->
            { model with Time = newTime }, Cmd.none

    let formatTime (dt: DateTime) =
        dt.ToString("HH:mm:ss")

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(
            padding = 20.0, 
            verticalOptions = LayoutOptions.Start,
            children = [ 
                View.Label(
                    text = sprintf "Hello %s!" model.Name
                    )
                View.Label(
                    text = sprintf "Time: %s" (formatTime model.Time)
                )
                View.Editor(
                    text = model.Name,
                    textChanged = 
                        (fun args -> 
                            dispatch (NameChanged(args.NewTextValue)))
                    )
            ]))

    let timerTick dispatch =
        let timer = new System.Timers.Timer(1000.0)
        timer.Elapsed.Subscribe (fun _ -> dispatch (TimeChanged System.DateTime.Now)) |> ignore
        timer.Enabled <- true
        timer.Start()

    let program =
        Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> Program.withSubscription (fun _ -> Cmd.ofSub App.timerTick)
        |> Program.runWithDynamicView app