# Appsolutely Fabulous

Functional mobile client development with F# and Xamarin Forms

James Murphy - @recumbent

---

@snap[west span-40]
### James Murphy - Software Engineer
@snapend

@snap[east span-40]
![RML](images/RML380Z.jpg)
@snapend

---

# Fabulous?

Note:

Why do we need something new?

There is a really good story for cross platform application mobile development using .NET with Xamarin, and for all platforms wth Xamarin forms, but whilst you can - and people do - use F# the core tends to be  MVVM, which is a pattern I like and one that that works really well... but its also _very_ object oriented. And to build an app you will have a view, in XAML, and code behind, and a you'll wire that to a view model... there may be a better way

---

#### So where does Fabulous come from?

Don Syme + Xamarin

Note:

I'm not quite sure what drove the decision or what the exact process was, but someone decided there ought to be a functional first approach to writing mobile client application - and if you're Microsoft you address this problem by sending Done Syme - creator of F# to Xamarin in the hope that something interesting will happen.

In this case they went looking for prior art and that led them to...

---

@snap[north]
## Fable Elmish
@snapend

@snap[south-west span-40 fragment]
![fable](images/fable_logo.png)
@snapend

@snap[south-east span-40 fragment]
![elmish](images/elmish.png)
@snapend

Note:

Fable - which is an F# to javascript transpiler and Elmish which is an F# framework for fable that resembles elm
So what it elm?

---

@snap[west span-40]
# Elm
![elm](images/Elm_logo.png)
@snapend

@snap[east span-40 fragment]
A delightful language for reliable webapps.
https://elm-lang.org/
@snapend

Note:

Well... elm describes itself as "A delightful language for reliable webapps" - that's a bold claim, but there are good reasons why one might find it delightful and there's a strong argument that functional programming lends itself to reliable applications 

---

### So what is Elm? 

@ul

- Functional language with an ML syntax
  - Statically typed
  - All functions are pure
     - Always returns the same result for a given input
     - _*No side effects*_
- DSL for creating markup
- Framework for web applications

@ulend

Note:

So what is elm?
Well firstly its a functional language with an ML syntax - so in that respect very similar to F#
Its statically typed, so the compiler is your friend
Everything is a function - and all functions are pure  that means that for any given input we will always get the same output and also that there are _no side effects_ - which makes things interesting and we'll come back to that in a moment

There is a DSL for creating html

And there is a framework that makes it possible to run an application, and this is the core reason for things being elmish

---

@snap[west span-50]
### The Elm Architecture
@snapend

@snap[east span-30]
@box[bg-white text-black rounded fragment](Model)
@box[bg-white text-black rounded fragment](View)
@box[bg-white text-black rounded fragment](Update)
@snapend

Note:
As I said, that you can only write pure functions is interesting, user input is not deterministic and rendering a UI to the screen is a side effect - and this is where the framework, more specifically The Elm Architecture comes in.

The Elm Architecture is the Model, View, Update pattern - that was the inspiration for redux amongst other things - and its a framework this pattern that enables your pure functions to create a proper user interace.

So what is the model view architecture?

---

# Model

Note:
We start with a Model that defines the current state of your app, it could be all your data for a small app

---

# View

Note:

The view is a pure function that takes your model and returns the view, in the case of Elm that would be http, but for fabulous its Xamarin Forms. The view gets passed to the runtime framework which does a diff and renders the ui to the screen

---

# Update

Note:

Your users interaction with the view will generate a message - from a list you define - and that will get passed with the model to the update function, Update is a pure function that will return a new model

---

# Model

Note:

The new model will be passed to the view function and round we go again. Some of you may have noticed that this limits you to whatever you have in your model (which makes it ephemeral...) and you'd be right, so there's more cheating to get round that and I'll come back to that later.

---

@snap[west span-40]
### A minimal application
@snapend

@snap[east span-40]
![wireframe](images/wireframe-01.jpg)
@snapend

Note:
So lets take a look at a minimal application - how about a minimal hello world? Per this classy mockup?

The value we type in the box will appear in the greeting

---?code=Examples/HelloWorld-01.fs&lang=fsharp&color=#1E1F21&title=Hello World App

@[9-10](Model)
@[24-39](View - Content Page)
@[29-39](View - Children)
@[12-13](Message)
@[19-22](Update)
@[41-42](Program)
@[14-17](Init)
@[44-52](App)

---

# Live Demo 01

---

@snap[west span-40]
### New feature
@snapend

@snap[east span-40]
![wireframe](images/wireframe-02.jpg)
@snapend


---?code=Examples/HelloWorld-02.fs&lang=fsharp&color=#1E1F21&title=Hello World App with Subscription

@[12](Add time to Model)
@[21](Initialise time)
@[33-34](Format time)
@[45-46](Add time to View)

---

# Screenshot with no ticking

---?code=Examples/HelloWorld-02.fs&lang=fsharp&color=#1E1F21&title=Hello World App with Subscription

@[17](Time Changed)
@[30-31](Update with changed time)
@[56-60](Timer function)
@[73](Add subscription)

---

# Live Demo 02

---

# A _slightly_ more complex App

Note:

A single page is seldom enough, nor do apps live in isolation. We need to be able to launch long running code, to make external requests, etc

So lets add a page with a random quote on it

---?code=Examples/HelloWorld-03.fs&lang=fsharp&color=#1E1F21

@[21-26](Model with Page and Quote)
@[11-13](Page Type)
@[15-19](Request State)
@[35-40](Initialise new properties)
@[104-107](View Button on Main Page)
@[31](Page changed message)
@[61-68](Handle page changed)
@[63-65](Main page)
@[66-68](Quote page - command)
@[44-53](getQuote command)
@[52](QuoteReceived message)
@[69-70](Handle QuoteReceived)
@[109-137](Quote Page)
@[77-82](Quote Text)

---

Live demo 03

---

Summary