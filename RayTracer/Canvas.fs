module Canvas

open System
open Colors

type Canvas = int * int * Color list list

let canvas width height : Canvas =
    let c = [ 1..height ] |> List.map (fun x -> [ for i in 1..width -> 0.0, 0.0, 0.0 ])

    width, height, c

let writePixel (c: Canvas) (x: int) (y: int) (color: Color) : Canvas =
    let _, _, canvas = c

    let indexed = canvas |> List.map List.indexed |> List.indexed

    canvas[0].Length,
    canvas.Length,
    indexed
    |> List.map (fun (idx, row) ->
        if idx = y then
            row |> List.map (fun (idx, column) -> if idx = x then color else column)
        else
            row |> List.map snd)

let pixelAt (c: Canvas) (x: int) (y: int) : Color =
    let _, _, canvas = c

    canvas[y][x]

let constructPpmPixelData (c: Canvas) =
    let _, _, colors = c
    let to255 = fun x -> x * 255.0 |> ceil |> (fun y -> Math.Clamp(y, 0.0, 255.0)) |> int
    let toColorComponent =
        fun (r, g, b) ->
            to255 r,
            to255 g,
            to255 b
    let stringifyRow = List.map string >> String.concat " "
    let tupleToList = fun (r, g, b) -> [ r; g; b ]
    
    colors
    |> List.map (List.map toColorComponent)
    |> List.map (List.map tupleToList)
    |> List.map (List.fold (fun acc x -> acc @ x) [])
    |> List.map stringifyRow
    |> String.concat "\n"


let canvasToPpm (c: Canvas) =
    $"""
P3
{c |> fun (w, h, _) -> w |> string} {c |> fun (w, h, _) -> h |> string}
255
{constructPpmPixelData c}
"""
