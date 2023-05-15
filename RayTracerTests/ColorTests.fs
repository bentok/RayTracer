module ColorsTests

open Xunit
open FsUnit
open Colors
open Utilities

[<Fact>]
let ``Colors are (red, green, blue) tuples`` () =
    let r, g, b = -0.5, 0.4, 1.7
    r |> should equal -0.5
    g |> should equal 0.4
    b |> should equal 1.7

[<Fact>]
let ``Adding colors`` () =
    let c1 = 0.9, 0.6, 0.75
    let c2 = 0.7, 0.1, 0.25
    let r, g, b = add c1 c2
    r |> should equal 1.6
    g |> should equal 0.7
    b |> should equal 1.0

[<Fact>]
let ``Subtracting colors`` () =
    let c1 = 0.9, 0.6, 0.75
    let c2 = 0.7, 0.1, 0.25
    let r, g, b = subtract c1 c2
    r =~ 2 |> should be True
    g |> should equal 0.5
    b |> should equal 0.5

[<Fact>]
let ``Multiplying a color by a scalar`` () =
    let c = 0.2, 0.3, 0.4
    let r, g, b = multiply c 2
    r =~ 0.4 |> should be True
    g =~ 0.6 |> should be True
    b =~ 0.8 |> should be True

[<Fact>]
let ``Multiplying colors`` () =
    let c1 = 1.0, 0.2, 0.4
    let c2 = 0.9, 1.0, 0.1
    let r, g, b = hadamardProduct c1 c2
    r =~ 0.9 |> should be True
    g =~ 0.2 |> should be True
    b =~ 0.04 |> should be True
