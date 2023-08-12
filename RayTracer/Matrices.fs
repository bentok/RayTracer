module Matrices

let (|Matrix4x4|Matrix3x3|Matrix2x2|) (value: float[][]) =
    match value.Length, value[0].Length with
    | 4, 4 -> Matrix4x4 value
    | 3, 3 -> Matrix3x3 value
    | 2, 2 -> Matrix2x2 value
    | _ -> failwith "Invalid matrix size"

let matrix (value: float[][]) = fun x y -> value[x][y]

let identityMatrix =
    [| [| 1.0; 0.0; 0.0; 0.0 |]
       [| 0.0; 1.0; 0.0; 0.0 |]
       [| 0.0; 0.0; 1.0; 0.0 |]
       [| 0.0; 0.0; 0.0; 1.0 |] |]

let equals (matrix1: float[][]) (matrix2: float[][]) =
    let m1 = matrix1 |> Array.toList |> List.map Array.toList
    let m2 = matrix2 |> Array.toList |> List.map Array.toList
    m1 = m2

let multiplyM (m1: float[][]) (m2: float[][]) =
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

let multiplyT (m: float[][]) (t: float * float * float * float) =
    let first, second, third, fourth = t
    
    match m with
    | Matrix4x4 matrix ->
        matrix
        |> Array.map (fun row ->
            row
            |> Array.mapi (fun x y -> x, y)
            |> Array.sumBy (fun (i, x) ->
                x
                * match i with
                  | 0 -> first
                  | 1 -> second
                  | 2 -> third
                  | 3 -> fourth
                  | _ -> failwith "Invalid matrix size"))
        |> fun x -> x[0], x[1], x[2], x[3]
    | _ -> failwith "Only 4x4 matrices are supported"

let transpose (m: float[][]) =
    match m with
    | Matrix4x4 matrix ->
        matrix
        |> Array.mapi (fun i _ -> Array.init 4 (fun j -> m[j][i]))
    | Matrix3x3 matrix ->
        matrix
        |> Array.mapi (fun i _ -> Array.init 3 (fun j -> m[j][i]))
    | Matrix2x2 matrix ->
        matrix
        |> Array.mapi (fun i _ -> Array.init 2 (fun j -> m[j][i]))

let determinant (m: float[][]) =
    match m with
    | Matrix2x2 m ->
        m[0][0] * m[1][1] - m[0][1] * m[1][0]
    | _ -> failwith "Only 2x2 matrices are supported"

let submatrix row col (m: float[][]) =
    m
    |> Array.map (Array.removeAt col)
    |> Array.removeAt row
