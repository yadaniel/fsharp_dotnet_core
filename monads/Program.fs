open System
open System.Text
open System.Text.RegularExpressions

// sum types
type ('a,'b,'c,'d) SumType1 = A of 'a | B of 'b | C of 'c | D of 'd
type SumType2<'a,'b,'c,'d> =
    | A of 'a
    | B of 'b
    | C of 'c
    | D of 'd

// product types
type ProductType1<'a,'b> = P of p:'a*'b
type ('a,'b) ProductType2 = { p:'a*'b }

// discriminated union
type ProductType3<'a,'b,'c> = P of p:'a*'b | Q of ('a*'b->'c)
type ('a,'b,'c) ProductType4 = { p:'a*'b; q:'a*'b->'c }

type E5B = {a:int; b:double}

type SumProduct =
    | E1
    | E2 of unit
    | E3 of unit*unit   // works as well
    | E4 of x:unit      // can be named
    // | E5a of {a:int; b:double}   // depricated
    | E5b of E5B                    // workaround 1 using external record type
    | E5c of {|a:int; b:double|}    // workaround 2 using anonymous record type
    | E5d of a:int * b:double       // workaround 3 using named tuple
    | E6 of int
    | E7 of x: int
    | E8 of x: int*int
    | E9 of x:int * y:int

type Eff =
    | Read  of f: (unit -> string)  // required () around ->
    | Write of f: (string -> unit)
    | Rewind of f: (unit -> unit)

type IO<'a> = Eff -> 'a

// monad context = computation expression

type Wrapper<'a> = Wrapped of 'a | None
type WrapBuilder() =
    member this.Bind (w:Wrapper<'a>,f:('a->Wrapper<'b>)) =
    // member this.Bind (w:('a) Wrapper,f:('a->Wrapper<'b>)) =
    // member this.Bind (w: ('a) Wrapper, f: (('a,'b) 'a->('b) Wrapper) ) =    // FS0062
        match w with
            | Wrapped v -> f v
            | None -> None
    member this.Return (v: 'a) = Wrapped v

let test () : double =
    let wrap = new WrapBuilder()
    let r = wrap {
        // using let! and return
        // let! calls Bind
        // return calls Return
        let! x1 = Wrapped 1.0
        let! x2 = Wrapped System.Double.NaN
        let! x3 = Wrapped System.Double.PositiveInfinity
        let! x4 = Wrapped System.Double.NegativeInfinity
        // let! x5 = None   // inserting None will propagate None because of the Bind implementation
        let! x5 = Wrapped 2.0
        //
        // addition is not defined for None, but the result is Wrapper<'a> type
        // the result is Wrapper<'a> type, which wraps double => nan is part of double definition
        return x1 + x2 + x3 + x4 + x5   
    }
    let res = match r with
                | Wrapped v -> printf "wrapped v\n"; v
                | None -> printf "none\n"; System.Double.NaN
    res

let test1 () : int =
    let t: int*int = (1,1)
    let (x,y) = t
    x + y

let test2() : unit =
    failwith "not implemented"

[<EntryPoint>]
let main argv =
    test() |> printf "%A\n"
    test1() |> ignore
    // test2()
    0

