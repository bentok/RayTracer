module Canvas

open System
open System.IO
open Colors

type Canvas = int * int * Color array array

let canvas width height : Canvas =
    let c = [| for _ in 1..height -> [| for _ in 1..width -> 0.0, 0.0, 0.0 |] |]

    width, height, c

let writePixel canvas x y color : Canvas =
    let _, _, colors = canvas

    let indexed =
        colors |> Array.mapi (fun i row -> i, row |> Array.mapi (fun j col -> j, col))

    colors |> Array.head |> Array.length,
    colors.Length,
    indexed
    |> Array.map (fun (idx, row) ->
        if idx = y then
            row |> Array.map (fun (idx, column) -> if idx = x then color else column)
        else
            row |> Array.map snd)

let pixelAt (c: Canvas) x y =
    let _, _, colors = c

    colors[y][x]

let makePpmHeader (c: Canvas) =
    let width, height, _ = c

    $"""P3
{width |> string} {height |> string}
255"""

let makePpmPixelData canvas =
    let _, _, colors = canvas

    let to255 =
        fun x -> x * 255.0 |> ceil |> (fun y -> Math.Clamp(y, 0.0, 255.0)) |> int

    let toColorComponent = fun (r, g, b) -> to255 r, to255 g, to255 b

    let concatenateColors colors =
        let rec splitOrConcat colors currentLine result =
            match colors with
            | [||] -> Array.rev (Array.append [| currentLine |] result)
            | _ ->
                let h = colors |> Array.head
                let t = Array.sub colors 1 (colors.Length - 1)

                let newLine =
                    match currentLine with
                    | "" -> h
                    | _ -> currentLine + " " + h

                if newLine.Length > 70 then
                    splitOrConcat t h (Array.append [| currentLine |] result)
                else
                    splitOrConcat t newLine result

        splitOrConcat colors "" [||] |> String.concat "\n"


    let stringifyRow = Array.map string >> concatenateColors
    let tupleToArray = fun (r, g, b) -> [| r; g; b |]

    colors
    |> Array.map (Array.map toColorComponent)
    |> Array.map (Array.map tupleToArray)
    |> Array.map (Array.fold Array.append [||])
    |> Array.map stringifyRow
    |> String.concat "\n"

let canvasToPpm canvas =
    $"""{makePpmHeader canvas}
{makePpmPixelData canvas}
"""

let writeStringToPPMFile (fileName: string) (ppmString: string) =
    let outputPath = Path.Combine(Directory.GetCurrentDirectory(), "output")
    Directory.CreateDirectory(outputPath) |> ignore // Ensure output directory exists
    let filePath = Path.Combine(outputPath, fileName)

    try
        File.WriteAllText(filePath, ppmString)
        printfn "PPM file was successfully written to %s" filePath
    with
    | :? IOException as ioEx -> printfn "IO Exception: %s" ioEx.Message
    | :? UnauthorizedAccessException as uaEx -> printfn "Unauthorized Access Exception: %s" uaEx.Message
    | ex -> printfn "An error occurred: %s" ex.Message
