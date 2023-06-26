module CanvasTests

open Xunit
open FsUnit
open Canvas

[<Fact>]
let ``Creating a canvas`` () =
    let width, height, c = canvas 10 20
    width |> should equal 10
    height |> should equal 20
    height |> should equal (List.length c)

    for row in c do
        width |> should equal (List.length row)

        for pixel in row do
            pixel |> should equal (0.0, 0.0, 0.0)

[<Fact>]
let ``Writing pixels to a canvas`` () =
    let c = canvas 10 20
    let red = 1.0, 0.0, 0.0
    let newCanvas = writePixel c 2 3 red
    let result = pixelAt newCanvas 2 3

    result |> should equal red

[<Fact>]
let ``Constructing the PPM header`` () =
    let c = canvas 5 3
    let ppm = makePpmHeader c

    ppm
    |> should
        equal
        """P3
5 3
255"""

[<Fact>]
let ``Constructing the PPM pixel data`` () =
    let c = canvas 5 3
    let c1 = 1.5, 0.0, 0.0
    let c2 = 0.0, 0.5, 0.0
    let c3 = -0.5, 0.0, 1.0

    let newCanvas =
        writePixel c 0 0 c1
        |> fun c -> writePixel c 2 1 c2
        |> fun c -> writePixel c 4 2 c3

    let ppm = makePpmPixelData newCanvas

    ppm
    |> should
        equal
        """255 0 0 0 0 0 0 0 0 0 0 0 0 0 0
0 0 0 0 0 0 0 128 0 0 0 0 0 0 0
0 0 0 0 0 0 0 0 0 0 0 0 0 0 255"""

[<Fact>]
let ``Splitting long lines in PPM files`` () =
    let c = canvas 10 2
    let c1 = 1.0, 0.8, 0.6

    let newCanvas =
        writePixel c 0 0 c1
        |> fun c -> writePixel c 1 0 c1
        |> fun c -> writePixel c 2 0 c1
        |> fun c -> writePixel c 3 0 c1
        |> fun c -> writePixel c 4 0 c1
        |> fun c -> writePixel c 5 0 c1
        |> fun c -> writePixel c 6 0 c1
        |> fun c -> writePixel c 7 0 c1
        |> fun c -> writePixel c 8 0 c1
        |> fun c -> writePixel c 9 0 c1
        |> fun c -> writePixel c 0 1 c1
        |> fun c -> writePixel c 1 1 c1
        |> fun c -> writePixel c 2 1 c1
        |> fun c -> writePixel c 3 1 c1
        |> fun c -> writePixel c 4 1 c1
        |> fun c -> writePixel c 5 1 c1
        |> fun c -> writePixel c 6 1 c1
        |> fun c -> writePixel c 7 1 c1
        |> fun c -> writePixel c 8 1 c1
        |> fun c -> writePixel c 9 1 c1

    let ppm = makePpmPixelData newCanvas

    ppm
    |> should
        equal
        """255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153
255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204
153 255 204 153 255 204 153 255 204 153 255 204 153"""

[<Fact>]
let ``PPM files are terminated by a newline character`` () =
    let c = canvas 5 3
    let ppm = canvasToPpm c
    ppm.EndsWith("\n") |> should equal true
