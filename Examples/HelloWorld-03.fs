namespace HelloWorld

open System.Diagnostics
open Fabulous.Core
open Fabulous.DynamicViews
open Xamarin.Forms
open System
open FSharp.Data

module App = 
    type PageType =
        | MainPage
        | QuotePage

    type RequestState =
        | NotRequested
        | Requested
        | Received of string
        | Error of string

    type Model = 
      { Page : PageType
        Name : string 
        Time : DateTime
        Quote : RequestState
      }

    type Msg = 
        | NameChanged of string
        | TimeChanged of DateTime
        | PageChanged of PageType
        | QuoteReceived of string
        | RefreshQuote

    let initModel = 
        { Page = MainPage;
          Name = "World"; 
          Time = DateTime.MinValue;
          Quote = NotRequested
        }

    let init () = initModel, Cmd.none

    let getQuote =
        let endpoint = 
            match Device.RuntimePlatform with
            | Device.Android -> "http://10.0.2.2:5000"
            | _ -> "http://localhost:5000"
        async {
            do! Async.SwitchToThreadPool()
            let! quote = Http.AsyncRequestString(endpoint)
            return QuoteReceived quote
        } |> Cmd.ofAsyncMsg

    let update msg model =
        match msg with
        | NameChanged newName ->
            { model with Name = newName }, Cmd.none
        | TimeChanged newTime ->
            { model with Time = newTime }, Cmd.none
        | PageChanged newPage -> 
            match newPage with
            | MainPage ->
                { model with Page = newPage },
                Cmd.none
            | QuotePage -> 
                { model with Page = newPage; Quote = Requested },
                getQuote
        | QuoteReceived quote -> 
            { model with Quote = Received quote }, Cmd.none
        | RefreshQuote ->
            { model with Quote = Requested}, getQuote

    let formatTime (dt: DateTime) =
        dt.ToString("HH:mm:ss")

    let quoteText requestState =
        match requestState with
        | NotRequested -> "Not Requested"
        | Requested -> "Waiting..."
        | Received quote -> quote
        | Error error -> sprintf "Error: %s" error

    let view (model: Model) dispatch =
        match model.Page with
        | MainPage ->
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
                    View.Button(
                        text = "Show Quote",
                        command = (fun () -> dispatch(PageChanged QuotePage))
                    )
                ]))
        | QuotePage ->
            View.NavigationPage(
                pages = [
                    View.ContentPage(
                        title = "A Quote",
                        useSafeArea = true,
                        content = View.StackLayout(
                            padding = 20.0,
                            verticalOptions = LayoutOptions.CenterAndExpand,
                            children = [
                                View.Label(
                                    text = quoteText model.Quote
                                )
                                View.Button(
                                    text = "Refresh Quote",
                                    command = (fun () -> dispatch(RefreshQuote)),
                                    canExecute = 
                                        match model.Quote with
                                        | Received _ | Error _ -> true
                                        | _ -> false
                                    )
                            ]
                        )
                    )
                    ],
                popped= (fun _ -> dispatch(PageChanged MainPage)),
                poppedToRoot = (fun _ -> dispatch(PageChanged MainPage))
                )
                .ToolbarItems([View.ToolbarItem(text="Close", command=(fun () -> dispatch (PageChanged MainPage)))] )
                .HasNavigationBar(true).HasBackButton(true)

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