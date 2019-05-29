module Main

open System
open System.Text
open System.Text.RegularExpressions

type Qx = Q | Q0 of unit | Q1 of int | Q2 of int*int

[<Literal>]
let FOO = "foo"

let f() = 
    match System.Console.ReadLine() with
    | FOO -> printfn "FOO as literal"
    | "bar" -> printfn "bar as constant"
    | _ -> printfn "other"

let _ = f()
let _ = f()
let _ = f()

module A0 =
    module A1 =
        module A2 =
            ()

// prefix operators available
// ! (or repetitions of !)
// ~ (or repetitions of ~)
// +
// -
// +.
// -.
// %
// %%
// ?
// &  - warning FS0086 should not be redefined
// && - warning FS0086

// infix
let (==) (a:int) (b:float) = true
let (===) (a:int) (b:float) = true
let (====) (a:int) (b:float) = true

// prefix
let (?) (x:double) : bool = (abs x) < 1e-6
let (!) (x:double) = x**10.0
let (!!) (x:double) = x**100.0
let (!!!) (x:double) = x**1000.0
let (%) (x:double) = x
let (%%) (x:double) = x
let (~~) (x:double) = x
let (+) (x:double) = x
let (++) (x:double) = x
let (+++) (x:double) = x
let (-) (x:double) = x
let (--) (x:double) = x
let (---) (x:double) = x
let (+.) (x:double) = x
let (.+) (x:double) = x
let (++.) (x:double) = x
let (.++) (x:double) = x
let (-.) (x:double) = x
let (--.) (x:double) = x

 
let test_infix() =
    printfn "\ninfix"
    printfn "%b" (1 === 1.0)

let test_prefix() =
    printfn "\nprefix"
    printfn "%f" (!1.1)
    printfn "%f" (!!1.1)

[<EntryPoint>]
let main argv : int =
    test_infix()
    test_prefix()
    0

