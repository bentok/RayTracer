module Canvas

open System
open System.IO
open Colors

type Canvas = int * int * Color list list

let canvas width height : Canvas =
    let c = [ 1..height ] |> List.map (fun x -> [ for i in 1..width -> 0.0, 0.0, 0.0 ])

    width, height, c

let writePixel canvas x y color : Canvas =
    let _, _, colors = canvas

    let indexed = colors |> List.map List.indexed |> List.indexed

    colors[0].Length,
    colors.Length,
    indexed
    |> List.map (fun (idx, row) ->
        if idx = y then
            row |> List.map (fun (idx, column) -> if idx = x then color else column)
        else
            row |> List.map snd)

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
            | [] -> List.rev (currentLine :: result)
            | h :: t ->
                let newLine =
                    match currentLine with
                    | "" -> h
                    | _ -> currentLine + " " + h

                if newLine.Length > 70 then
                    splitOrConcat t h (currentLine :: result)
                else
                    splitOrConcat t newLine result

        splitOrConcat colors "" [] |> String.concat "\n"

    let stringifyRow = List.map string >> concatenateColors
    let tupleToList = fun (r, g, b) -> [ r; g; b ]

    colors
    |> List.map (List.map toColorComponent)
    |> List.map (List.map tupleToList)
    |> List.map (List.fold (fun acc x -> acc @ x) [])
    |> List.map stringifyRow
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
