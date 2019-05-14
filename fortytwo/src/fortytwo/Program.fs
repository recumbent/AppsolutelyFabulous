module Fortytwo.App

open System
open System.IO
open System.Threading
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

// ----
// Data
// ----

let quotes = [|
    "A learning experience is one of those things that says, 'You know that thing you just did? Don't do that.’"
    "I may not have gone where I intended to go, but I think I have ended up where I needed to be."
    "Let's think the unthinkable, let's do the undoable. Let us prepare to grapple with the ineffable itself, and see if we may not eff it after all."
    "The quality of any advice anybody has to offer has to be judged against the quality of life they actually lead."
    "I refuse to answer that question on the grounds that I don't know the answer."
    "It is a mistake to think you can solve any major problems just with potatoes."
    "All opinions are not equal. Some are a very great deal more robust, sophisticated and well supported in logic and argument than others."
    "I love deadlines. I love the whooshing noise they make as they go by."
    "The idea was fantastically, wildly improbable. But like most fantastically, wildly improbable ideas it was at least as worthy of consideration as a more mundane one to which the facts had been strenuously bent to fit."
    "I'd far rather be happy than right any day."
    "Only a child sees things with perfect clarity, because it hasn't developed all those filters which prevent us from seeing things that we don't expect to see."
    "Reality is frequently inaccurate."
    "What I need... is a strong drink and a peer group."
    "It is folly to say you know what is happening to other people. Only they know, if they exist. They have their own Universes of their own eyes and ears."
    "There is no point in using the word 'impossible' to describe something that has clearly happened."
    "One is never alone with a rubber duck."
    "Beethoven tells you what it's like to be Beethoven and Mozart tells you what it's like to be human. Bach tells you what it's like to be the universe."
    "Anything that thinks logically can be fooled by something else that thinks at least as logically as it does."
    "Let the past hold on to itself and let the present move forward into the future."
    "I don't accept the currently fashionable assertion that any view is automatically as worthy of respect as any equal and opposite view."
    "Time is an illusion. Lunchtime doubly so."
    "The impossible often has a kind of integrity to it which the merely improbable lacks."
    "Life is wasted on the living."
    "There are some people you like immediately, some whom you think you might learn to like in the fullness of time, and some that you simply want to push away from you with a sharp stick."
    "I'd take the awe of understanding over the awe of ignorance any day."
    "All you really need to know for the moment is that the universe is a lot more complicated than you might think, even if you start from a position of thinking it's pretty damn complicated in the first place."
    "Anything invented after you're thirty-five is against the natural order of things."
    "A life that is burdened with expectations is a heavy life. Its fruit is sorrow and disappointment."
    "If you try and take a cat apart to see how it works, the first thing you have on your hands is a non-working cat."
    "People who need to bully you are the easiest to push around."
    "We have normality. I repeat, we have normality. Anything you still can't cope with is therefore your own problem."
    "Beauty doesn't have to be about anything. What's a vase about? What's a sunset or a flower about? What, for that matter, is Mozart's Twenty-third Piano Concerto about?"
    "Anyone who is capable of getting themselves made President should on no account be allowed to do the job."
    "Even a manically depressed robot is better to talk to than nobody."
    "My universe is my eyes and my ears. Anything else is hearsay."
    "If it looks like a duck, and quacks like a duck, we have at least to consider the possibility that we have a small aquatic bird of the family anatidae on our hands."
    "A cup of tea would restore my normality."
    "Human beings, who are almost unique in having the ability to learn from the experience of others, are also remarkable for their apparent disinclination to do so."
    "It can be very dangerous to see things from somebody else's point of view without the proper training."
    "You live and learn. At any rate, you live."
    "Don't believe anything you read on the net. Except this. Well, including this, I suppose."
    "Don't Panic."
    |]


// ---------------------------------
// Web app
// ---------------------------------

let rnd = System.Random()  

let webApp =
    warbler(fun _ ->
        Thread.Sleep(4000) 
        text (quotes.[rnd.Next(Array.length quotes)]))

// ---------------------------------
// Error handler
// ---------------------------------

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(ex, "An unhandled exception has occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

// ---------------------------------
// Config and Main
// ---------------------------------

let configureApp (app : IApplicationBuilder) =
    (app.UseGiraffeErrorHandler errorHandler)
        .UseGiraffe(webApp)

let configureServices (services : IServiceCollection) =
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    builder.AddFilter(fun l -> l.Equals LogLevel.Error)
           .AddConsole()
           .AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseIISIntegration()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0