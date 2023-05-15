module PointsAndVectorTests

open Xunit
open PointsAndVectors
open FsUnit

[<Fact>]
let ``A tuple with w=1.0 is a point`` () =
    let point = 4.3, -4.2, 3.1, 1.0
    let x, y, z, w = point
    x |> should equal 4.3
    y |> should equal -4.2
    z |> should equal 3.1
    w |> should equal 1.0


[<Fact>]
let ``A tuple with w=0.0 is a vector`` () =
    let x, y, z, w = 4.3, -4.2, 3.1, 0.0
    x |> should equal 4.3
    y |> should equal -4.2
    z |> should equal 3.1
    w |> should equal 0.0

[<Fact>]
let ``Adding two tuples`` () =
    let a1 = 3.0, -2.0, 5.0, 1.0
    let a2 = -2.0, 3.0, 1.0, 0.0
    let result = add a1 a2
    result |> should equal (1.0, 1.0, 6.0, 1.0)

[<Fact>]
let ``Subtracting two points`` () =
    let p1 = 3.0, 2.0, 1.0, 1.0
    let p2 = 5.0, 6.0, 7.0, 1.0
    let result = subtract p1 p2
    result |> should equal (-2.0, -4.0, -6.0, 0.0)

[<Fact>]
let ``Subtracting a vector from a point`` () =
    let p = 3.0, 2.0, 1.0, 1.0
    let v = 5.0, 6.0, 7.0, 0.0
    let result = subtract p v
    result |> should equal (-2, -4, -6, 1.0)

[<Fact>]
let ``Subtracting two vectors`` () =
    let v1 = 3.0, 2.0, 1.0, 0.0
    let v2 = 5.0, 6.0, 7.0, 0.0
    let result = subtract v1 v2
    result |> should equal (-2, -4, -6, 0.0)

[<Fact>]
let ``Subtracting a vector from the zero vector`` () =
    let zero = 0.0, 0.0, 0.0, 0.0
    let v2 = 1.0, -2.0, 3.0, 0.0
    let result = subtract zero v2
    result |> should equal (-1, 2, -3, 0.0)

[<Fact>]
let ``Negating a tuple`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = negate a
    result |> should equal (-1.0, 2.0, -3.0, 4.0)

[<Fact>]
let ``Multiplying a tuple by a scalar`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = multiply a 3.5
    result |> should equal (3.5, -7.0, 10.5, -14.0)

[<Fact>]
let ``Multiplying a tuple by a fraction`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = multiply a 0.5
    result |> should equal (0.5, -1.0, 1.5, -2.0)

[<Fact>]
let ``Dividing a tuple by a scalar`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = divide a 2
    result |> should equal (0.5, -1.0, 1.5, -2.0)

[<Fact>]
let ``Computing the magnitude of vector(1, 0, 0)`` () =
    let v = 1.0, 0.0, 0.0, 0.0
    let result = magnitude v
    result |> should equal 1.0

[<Fact>]
let ``Computing the magnitude of vector(0, 1, 0)`` () =
    let v = 0.0, 1.0, 0.0, 0.0
    let result = magnitude v
    result |> should equal 1.0

[<Fact>]
let ``Computing the magnitude of vector(0, 0, 1)`` () =
    let v = 0.0, 0.0, 1.0, 0.0
    let result = magnitude v
    result |> should equal 1.0

[<Fact>]
let ``Computing the magnitude of vector(1, 2, 3)`` () =
    let v = 1.0, 2.0, 3.0, 0.0
    let result = magnitude v
    result |> should equal (sqrt 14.0)

[<Fact>]
let ``Computing the magnitude of vector(-1, -2, -3)`` () =
    let v = -1.0, -2.0, -3.0, 0.0
    let result = magnitude v
    result |> should equal (sqrt 14.0)

[<Fact>]
let ``Normalizing vector(4, 0, 0) gives (1, 0, 0)`` () =
    let v = 4.0, 0.0, 0.0, 0.0
    let result = normalize v
    result |> should equal (1.0, 0.0, 0.0, 0.0)

[<Fact>]
let ``Normalizing vector(1, 2, 3)`` () =
    let v = 1.0, 2.0, 3.0, 0.0
    let result = v |> normalize
    result == (0.26726, 0.53452, 0.80178, 0.0) |> should equal true

[<Fact>]
let ``The magnitude of a normalized vector`` () =
    let v = 1.0, 2.0, 3.0, 0.0
    let result = normalize v
    magnitude result |> should equal 1

[<Fact>]
let ``The dot product of two tuples`` () =
    let a = 1.0, 2.0, 3.0, 0.0
    let b = 2.0, 3.0, 4.0, 0.0
    let result = dot a b
    result |> should equal 20

[<Fact>]
let ``The cross product of two vectors`` () =
    let a = 1.0, 2.0, 3.0, 0.0
    let b = 2.0, 3.0, 4.0, 0.0
    let result1 = cross a b
    let result2 = cross b a
    result1 |> should equal (-1.0, 2.0, -1.0, 0.0)
    result2 |> should equal (1.0, -2.0, 1.0, 0.0)
