namespace RayTracer

open Colors

module Canvas =

    type Canvas = int * int * Color list list

    let canvas width height : Canvas =
        let c =
            [ 1 .. height ]
            |> List.map (fun x -> [ for i in 1 .. width -> color (0, 0, 0) ])

        width, height, c

    let writePixel (c: Canvas) (x: int) (y: int) (color: Color) =
        let _, _, canvas = c

        let indexed =
            canvas |> List.map List.indexed |> List.indexed

        indexed
        |> List.map
            (fun (idx, row) ->
                if idx = y then
                    row
                    |> List.map (fun (idx, column) -> if idx = x then color else column)
                else
                    row |> List.map snd)
