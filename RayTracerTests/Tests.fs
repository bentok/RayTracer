module Tests

open System
open Xunit
open RayTracer.Tuples

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
    let a1 = point (3, -2, 5)
    let a2 = vector (-2, 3, 1)
    let result = add a1 a2
    Assert.Equal(result, (1.0, 1.0, 6.0, 1.0))