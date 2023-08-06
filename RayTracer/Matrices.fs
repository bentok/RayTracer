module Matrices

let (|Matrix4x4|Matrix3x3|Matrix2x2|) (value: float[][]) =
    match value.Length, value[0].Length with
    | 4, 4 -> Matrix4x4 value
    | 3, 3 -> Matrix3x3 value
    | 2, 2 -> Matrix2x2 value
    | _ -> failwith "Invalid matrix size"

let matrix (value: float[][]) = fun x y -> value[x][y]

let equals (matrix1: float[][]) (matrix2: float[][]) =
    let m1 = matrix1 |> Array.toList |> List.map Array.toList
    let m2 = matrix2 |> Array.toList |> List.map Array.toList
    m1 = m2
