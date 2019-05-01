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

Well... elm describes itself as "A delightful language for reliable webapp" - given that it bears a strong resemblence F# the language might well be delightful and we all want reliable web apps...

---

# So what is Elm? 

- Functional language with an ML syntax
  - All functions are pure
     - Always returns the same result for a given input
     - _*No side effects*_
- A DSL for creating html
- Framework for web applications
     
Note:

And in many respects the reasons elm claims to be delightful and reliable are the reasons to want to be like elm - more specifically elm is a Function Language, in Elm all functions are pure - for a given input we will always get the same out and there are _no side effects_ - which makes things interesting and we'll come back to that in a moment

There is a DSL for creating html

And there is a framework for running the applications - and this is where things start to get interesting

---

# The Elm Architecture

Model -> View -> Update

Note:
As I said, that you can only write pure functions is interesting, user input is not deterministic and rendering a UI to the screen is a side effect - and this is where the framework, more specifically The Elm Architecture comes in.

The Elm Architecture is the Model, View, Update pattern - that was the inspiration for redux amongst other things - and its this pattern that enables your pure functions to create a proper user interace.

So what is the model view architecture?

I think this is best illustrated with code...

---

# Model, View, Update

---

# Hello world in Elm to illustrate the pattern with type annotations.

---

# Getting started

dotnet new -i

dotnet new...

---

# A minimal application

---

An application with navigation

---

An application with interaction with the real world
