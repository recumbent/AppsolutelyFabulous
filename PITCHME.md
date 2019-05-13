# Appsolutely Fabulous

James Murphy - @recumbent

---

# Fabulous?

"F# Functional App Development, using Xamarin.Forms"

Note:

Why do we need something new?

There is a really good story for cross platform application development using .NET - and by and large this is MVVM, which is a fabulous pattern that works really well... but its also _very_ object oriented. 

---

# So where does Fabulous come from?

Don Syme + Xamarin

Note:

I'm not quite sure what drove the decision, but someone decided there ought to be a functional first approach to writing mobile client application - and if you're Microsoft you address this problem by sending Done Syme - creator of F# to Xamarin in the hope that something interesting will happen.

In this case they went looking for prior art and that led them to...

---

# Fable Elmish

Fable Logo + Elmish logo

Note

Fable - which is an F# to javascript transpiler and Elmish which is an F# framework for fable that resembles elm
So what it elm?

---

# Elm

A delightful language for reliable webapps.

https://elm-lang.org/

Note:

Well... elm describes itself as "A delightful language for reliable webapps" - that's a bold claim, but there are good reasons why one might find it delightful and there's a strong argument that functional programming lends itself to reliable applications 

---

# So what is Elm? 

- Functional language with an ML syntax
  - Statically typed
  - All functions are pure
     - Always returns the same result for a given input
     - _*No side effects*_
- DSL for creating html
- Framework for web applications

Note:

So what is elm?
Well firstly its a functional language with an ML syntax - so in that respect very similar to F#
Its statically typed, so the compiler is your friend
Everything is a function - and all functions are pure  that means that for any given input we will always get the same output and also that there are _no side effects_ - which makes things interesting and we'll come back to that in a moment

There is a DSL for creating html

And there is a framework that makes it possible to run an application, and this is the core reason for things being elmish

---

# The Elm Architecture

Model -> View -> Update

Note:
As I said, that you can only write pure functions is interesting, user input is not deterministic and rendering a UI to the screen is a side effect - and this is where the framework, more specifically The Elm Architecture comes in.

The Elm Architecture is the Model, View, Update pattern - that was the inspiration for redux amongst other things - and its a framework this pattern that enables your pure functions to create a proper user interace.

So what is the model view architecture?

---

# Model

Note:
We start with a Model that defines the current state of your app, it could be all your data for a small app

---

# Model -> View

Note:
The view is a pure function that takes your model and returns the view, in the case of Elm that would be http, but for fabulous its Xamarin Forms. The view gets passed to the runtime framework which does a diff and renders the ui to the screen

---

# View -> Update

Note:

Your users interaction with the view will generate a message - from a list you define - and that will get passed with the model to the update function, Update is a pure function that will return a new model

---

# Update -> Model

Note:
The new model will be passed to the view function and round we go again. Some of you may have noticed that this limits you to whatever you have in your model (which makes it ephemeral...) and you'd be right, so there's more cheating to get round that and I'll come back to that later.

---

# A minimal application

With mockup

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
@[16-19](Init)
@[44-52](App)

---

# Run...

---

# Live coding demo?

---?code=Examples/HelloWorld.fs&lang=fsharp&color=#1E1F21&title=Hello World App with Subscription

@[12](Add time to Model)
@[21](Initialise time)
@[33-34](Format time)
@[45-46](Add time to View)

---

# Screenshot with no ticking

---?code=Examples/HelloWorld.fs&lang=fsharp&color=#1E1F21&title=Hello World App with Subscription

@[17](Time Changed)
@[30-31](Update with changed time)
@[56-60](Timer function)
@[73](Add subscription)

---

An application with navigation

---

An application with interaction with the real world
