module CanvasTests

open Xunit
open Canvas

[<Fact>]
let ``Creating a canvas`` () =
    let width, height, c = canvas 10 20
    Assert.Equal(width, 10)
    Assert.Equal(height, 20)
    Assert.Equal(height, List.length c)

    for row in c do
        Assert.Equal(width, List.length row)

        for pixel in row do
            Assert.Equal(pixel, (0.0, 0.0, 0.0))
