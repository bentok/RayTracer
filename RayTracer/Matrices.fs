module Matrices

let (|Matrix4x4|Matrix3x3|Matrix2x2|) (value: float[][]) =
    match value.Length, value[0].Length with
    | 4, 4 -> Matrix4x4 value
    | 3, 3 -> Matrix3x3 value
    | 2, 2 -> Matrix2x2 value
    | _ -> failwith "Invalid matrix size"

let matrix (value: float[][]) = fun x y -> value[x][y]

let ``==`` (matrix1: float[][]) (matrix2: float[][]) =
    let m1 = matrix1 |> Array.toList |> List.map Array.toList
    let m2 = matrix2 |> Array.toList |> List.map Array.toList
    m1 = m2

let ``*`` (m1: float[][]) (m2: float[][]) =
    let dotProduct v1 v2 =
        Array.fold2 (fun acc x y -> acc + x * y) 0.0 v1 v2

    let multiply size =
        m1
        |> Array.mapi (fun i _ -> Array.init size (fun j -> dotProduct m1[i] (Array.init size (fun k -> m2[k][j]))))

    match m1, m2 with
    | Matrix4x4 _, Matrix4x4 _ -> multiply 4
    | Matrix3x3 _, Matrix3x3 _ -> multiply 3
    | Matrix2x2 _, Matrix2x2 _ -> multiply 2
    | _ -> failwith "Invalid matrix sizes"
