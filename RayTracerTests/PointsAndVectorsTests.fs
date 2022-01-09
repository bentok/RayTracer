module PointsAndVectors

open Xunit
open RayTracer.PointsAndVectors

[<Fact>]
let ``A tuple with w=1.0 is a point`` () =
    let x, y, z, w = point (4.3, -4.2, 3.1)
    Assert.Equal(x, 4.3)
    Assert.Equal(y, -4.2)
    Assert.Equal(z, 3.1)
    Assert.Equal(w, 1.0)

[<Fact>]
let ``A tuple with w=0.0 is a vector`` () =
    let x, y, z, w = vector (4.3, -4.2, 3.1)
    Assert.Equal(x, 4.3)
    Assert.Equal(y, -4.2)
    Assert.Equal(z, 3.1)
    Assert.Equal(w, 0.0)

[<Fact>]
let ``Adding two tuples`` () =
    let a1 = 3.0, -2.0, 5.0, 1.0
    let a2 = -2.0, 3.0, 1.0, 0.0
    let result = add a1 a2
    Assert.Equal(result, (1.0, 1.0, 6.0, 1.0))

[<Fact>]
let ``Subtracting two points`` () =
    let p1 = point (3, 2, 1)
    let p2 = point (5, 6, 7)
    let result = subtract p1 p2
    Assert.Equal(result, vector (-2, -4, -6))

[<Fact>]
let ``Subtracting a vector from a point`` () =
    let p = point (3, 2, 1)
    let v = vector (5, 6, 7)
    let result = subtract p v
    Assert.Equal(result, point (-2, -4, -6))

[<Fact>]
let ``Subtracting two vectors`` () =
    let v1 = vector (3, 2, 1)
    let v2 = vector (5, 6, 7)
    let result = subtract v1 v2
    Assert.Equal(result, vector (-2, -4, -6))

[<Fact>]
let ``Subtracting a vector from the zero vector`` () =
    let zero = vector (0, 0, 0)
    let v2 = vector (1, -2, 3)
    let result = subtract zero v2
    Assert.Equal(result, vector (-1, 2, -3))

[<Fact>]
let ``Negating a tuple`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = negate a
    Assert.Equal(result, (-1.0, 2.0, -3.0, 4.0))

[<Fact>]
let ``Multiplying a tuple by a scalar`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = multiply a 3.5
    Assert.Equal(result, (3.5, -7.0, 10.5, -14.0))

[<Fact>]
let ``Multiplying a tuple by a fraction`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = multiply a 0.5
    Assert.Equal(result, (0.5, -1.0, 1.5, -2.0))

[<Fact>]
let ``Dividing a tuple by a scalar`` () =
    let a = 1.0, -2.0, 3.0, -4.0
    let result = divide a 2
    Assert.Equal(result, (0.5, -1.0, 1.5, -2.0))

[<Fact>]
let ``Computing the magnitude of vector(1, 0, 0)`` () =
    let v = vector (1, 0, 0)
    let result = magnitude v
    Assert.Equal(result, 1.0)

[<Fact>]
let ``Computing the magnitude of vector(0, 1, 0)`` () =
    let v = vector (0, 1, 0)
    let result = magnitude v
    Assert.Equal(result, 1.0)

[<Fact>]
let ``Computing the magnitude of vector(0, 0, 1)`` () =
    let v = vector (0, 0, 1)
    let result = magnitude v
    Assert.Equal(result, 1.0)

[<Fact>]
let ``Computing the magnitude of vector(1, 2, 3)`` () =
    let v = vector (1, 2, 3)
    let result = magnitude v
    Assert.Equal(result, sqrt 14.0)

[<Fact>]
let ``Computing the magnitude of vector(-1, -2, -3)`` () =
    let v = vector (-1, -2, -3)
    let result = magnitude v
    Assert.Equal(result, sqrt 14.0)

[<Fact>]
let ``Normalizing vector(4, 0, 0) gives (1, 0, 0)`` () =
    let v = vector (4, 0, 0)
    let result = normalize v
    Assert.Equal(result, vector (1, 0, 0))

[<Fact>]
let ``Normalizing vector(1, 2, 3)`` () =
    let v = vector (1, 2, 3)
    let result = normalize v
    Assert.True(equalV result (0.26726, 0.53452, 0.80178, 0.0))

[<Fact>]
let ``The magnitude of a normalized vector`` () =
    let v = vector (1, 2, 3)
    let result = normalize v
    Assert.Equal(magnitude result, 1)

[<Fact>]
let ``The dot product of two tuples`` () =
    let a = vector (1, 2, 3)
    let b = vector (2, 3, 4)
    let result = dot a b
    Assert.Equal(result, 20)

[<Fact>]
let ``The cross product of two vectors`` () =
    let a = vector (1, 2, 3)
    let b = vector (2, 3, 4)
    let result1 = cross a b
    let result2 = cross b a
    Assert.Equal(result1, vector (-1, 2, -1))
    Assert.Equal(result2, vector (1, -2, 1))
