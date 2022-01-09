module Canvas

open Xunit
open RayTracer.Canvas

[<Fact>]
let ``Colors are (red, green, blue) tuples`` () =
    let r, g, b = color (-0.5, 0.4, 1.7)
    Assert.Equal(r, -0.5)
    Assert.Equal(g, 0.4)
    Assert.Equal(b, 1.7)

[<Fact>]
let ``Adding colors`` () =
    let c1 = color (0.9, 0.6, 0.75)
    let c2 = color (0.7, 0.1, 0.25)
    let r, g, b = add c1 c2
    Assert.Equal(r, 1.6)
    Assert.Equal(g, 0.7)
    Assert.Equal(b, 1.0)

[<Fact>]
let ``Subtracting colors`` () =
    let c1 = color (0.9, 0.6, 0.75)
    let c2 = color (0.7, 0.1, 0.25)
    let r, g, b = subtract c1 c2
    Assert.True(equal r 0.2)
    Assert.Equal(g, 0.5)
    Assert.Equal(b, 0.5)

[<Fact>]
let ``Multiplying a color by a scalar`` () =
    let c = color (0.2, 0.3, 0.4)
    let r, g, b = multiply c 2
    Assert.True(equal r 0.4)
    Assert.True(equal g 0.6)
    Assert.True(equal b 0.8)

[<Fact>]
let ``Multiplying colors`` () =
    let c1 = color (1, 0.2, 0.4)
    let c2 = color (0.9, 1, 0.1)
    let r, g, b = hadamardProduct c1 c2
    Assert.True(equal r 0.9)
    Assert.True(equal g 0.2)
    Assert.True(equal b 0.04)
