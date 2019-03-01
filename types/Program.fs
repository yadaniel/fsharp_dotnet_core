open System

// sum type
type S = | A | B | C
// sum types with same and similar structure
type S1 = | A | B | C
type S2 = | A | B | C | D

// product type
type P = {x:int; y:int}
type T = int*int
type V = V of x:int * y:int * z:int
// product types with same and similar structure
type P1 = {x:int; y:int}
type P2 = {x:int; y:int; z:int}
type T1 = int*int
type T2 = int*int*int
type V1 = V of x:int * y:int * z:int
type V2 = V of x:int * y:int * z:int * q:bool


// values of sum type
let s0 = A                  // S2 ... because last known
let s1 = S.A                // S ... because explicit
let p1 = {x=0;y=10}:P       // P ... because explicit
let p2 = {P.x=0;y=10}       // P ... because explicit
let p3 : P = {x=0;y=10}     // P ... because explicit
let p4 = {x=0; y=10}        // P1 ... because last known
let t0 = 0,10               // T1 ... because last known
let t1 = (0,10)             // T1 ... because last known
let t2 : T = (0,10)         // T ... because explicit

// mixed sum and product types ... discriminated union
type Avec = A | A0 of unit | A1 of int | A2 of int*int | A3 of int*int*int

let a = Avec.A
let a0 = Avec.A0 ()
let a1 = Avec.A1 0      // () are optional
let a1a = Avec.A1 (0)
let a2 = Avec.A2 (0,0)  // () are not optional
let a3 = Avec.A3 (0,0,0)

let f (x:int) (y:int) : int =
    x + y

 
[<EntryPoint>]
let main argv =
    printfn "test"
    f 1 2 |> printfn "%i"
    0

