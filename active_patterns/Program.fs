open System
open System.Text
open System.Text.RegularExpressions

// special syntax (||) makes F# to activate the pattern within a match expression
// match_param only
// (|X|) match_param - single case complete active pattern used for data transformation
// (|X|Y|) match_param - multi case complete active pattern used for data categorization
// (|X|_|) match_param - partial active pattern used for data binary data categorization (returns option)
// (|X|Y|_|) match_param - not allowed (FS:0827)
// param and match_param
// (|X|) param match_param - single case complete active pattern with parameter
// (|X|Y|) param match_param - not allowed
// (|X|_|) param match_param - parametrized partial active pattern
// (|X|Y|_|) param match_param - not allowed

let (|UpperLower|) (param:bool) (match_param:string) =
    if param then
        match_param.ToUpper()
    else
        match_param.ToLower()

let test v b =
    match v with
    | UpperLower b "FOO" -> printfn "FOO"
    | UpperLower (not b) "bar" -> printfn "bar"
    | _ -> printfn "catch all ... otherwise exception"

// complete active pattern
let (|UpperCase|) (match_param:string) = match_param.ToUpper()

// the return value from UpperCase is compared to what is given within match
// and can be optionally bound to local name
let test0 v =
    match v with
    | UpperCase "ABC" as w -> printfn "ABC, given %A" w
    | UpperCase "XYZ" -> printfn "XYZ"
    | _ as w -> printfn "else, giveb %A" w

// complete active pattern
let (|A|B|C|X|) match_param =
    match match_param with
    | "A" -> A
    | "B" -> B
    | "C" -> C
    | _ -> X

let test1(v) = 
    match v with
    | A -> printfn "A letter"
    | B -> printfn "B letter"
    | C -> printfn "C letter"
    | X -> printfn "other"

// complete active pattern with parameter
// error FS0191: Only active patterns returning exactly one result may accept arguments.
// error FS0722: Only active patterns returning exactly one result may accept arguments
(*
let (|Range1|Range2|OutOfRange|) param1 param2 param3 match_param =
    match match_param with
    | t when (t >= param1) && (t < param2)  -> Range1
    | t when (t >= param2) && (t < param3)  -> Range2
    | _ -> OutOfRange

let test2 v r1 r2 r3 =
    match v with
    | Range1 r1 r2 r3 -> printfn "Range1: %A within %A and %A" v r1 r2
    | Range2 r2 r2 r3 -> printfn "Range2: %A within %A and %A" v r2 r3
    | OutOfRange r1 r2 r3 -> printfn "OutOfRange: %A not within %A and %A" v r1 r3
*)

(*
let (|A|B|C|_|) match_param = ...
    does not compile
    complete active pattern must return A|B|C
    partial active pattern must return Some A|None
*)

// partial active pattern without parameter
let (|X|_|) match_param =
    match match_param with
    | "X" -> Some X
    | _ -> None

let test3(v) = 
    match v with
    | X -> printfn "Some X"
    | _ -> printfn "None"

// partial active pattern with parameter
let (|Y|_|) param match_param =
    if param then
        match match_param with
        | "Y" -> Some Y
        | _ -> None
    else None

let test4 v b =
    match v with
    | Y b -> printfn "Some Y"
    | _ -> printfn "None"

[<EntryPoint>]
let main argv =
    test "foo" true
    test "foo" false
    test "bar" true
    test "bar" false
    //
    test0 "abc"
    test0 "xYz"
    test0 "foo"
    //
    test1 "A"
    test1 "B"
    test1 "C"
    test1 "D"
    //
    test3 "X"
    test3 "Y"
    //
    test4 "Y" true
    test4 "Z" true
    test4 "Y" false
    test4 "Z" false
    0

