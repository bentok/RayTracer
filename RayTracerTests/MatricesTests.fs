module MatricesTests

open Xunit
open FsUnit
open Matrices

[<Fact>]
let ``Constructing and inspecting a 4x4 matrix by array indices`` () =
    let m =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.5; 6.5; 7.5; 8.5 |]
           [| 9.0; 10.0; 11.0; 12.0 |]
           [| 13.5; 14.5; 15.5; 16.5 |] |]

    match m with
    | Matrix4x4 m ->
        m[0][0] |> should equal 1.0
        m[0][3] |> should equal 4.0
        m[1][0] |> should equal 5.5
        m[1][2] |> should equal 7.5
        m[2][2] |> should equal 11.0
        m[3][0] |> should equal 13.5
        m[3][2] |> should equal 15.5
    | _ -> failwith "Expected a 4x4 matrix"

let ``Constructing and inspecting a 4x4 matrix by matrix utility`` () =
    let m =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.5; 6.5; 7.5; 8.5 |]
           [| 9.0; 10.0; 11.0; 12.0 |]
           [| 13.5; 14.5; 15.5; 16.5 |] |]

    let m' = m, matrix m

    match m' with
    | Matrix4x4 _, mtrx ->
        mtrx 0 0 |> should equal 1.0
        mtrx 0 3 |> should equal 4.0
        mtrx 1 0 |> should equal 5.5
        mtrx 1 2 |> should equal 7.5
        mtrx 2 2 |> should equal 11.0
        mtrx 3 0 |> should equal 13.5
        mtrx 3 2 |> should equal 15.5
    | _ -> failwith "Expected a 4x4 matrix"

[<Fact>]
let ``A 2x2 matrix ought to be representable`` () =
    let m = [| [| -3.0; 5.0 |]; [| 1.0; -2.0 |] |]

    match m with
    | Matrix2x2 m ->
        m[0][0] |> should equal -3.0
        m[0][1] |> should equal 5.0
        m[1][0] |> should equal 1.0
        m[1][1] |> should equal -2.0
    | _ -> failwith "Expected a 2x2 matrix"

[<Fact>]
let ``A 3x3 matrix ought to be representable`` () =
    let m = [| [| -3.0; 5.0; 0.0 |]; [| 1.0; -2.0; -7.0 |]; [| 0.0; 1.0; 1.0 |] |]

    match m with
    | Matrix3x3 m ->
        m[0][0] |> should equal -3.0
        m[1][1] |> should equal -2.0
        m[2][2] |> should equal 1.0
    | _ -> failwith "Expected a 3x3 matrix"

[<Fact>]
let ``Matrix equality with identical indices`` () =
    let m1 =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.0; 6.0; 7.0; 8.0 |]
           [| 9.0; 8.0; 7.0; 6.0 |]
           [| 5.0; 4.0; 3.0; 2.0 |] |]

    let m2 =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.0; 6.0; 7.0; 8.0 |]
           [| 9.0; 8.0; 7.0; 6.0 |]
           [| 5.0; 4.0; 3.0; 2.0 |] |]

    m1 |> equals m2 |> should equal true

[<Fact>]
let ``Matrix equality with different matrices`` () =
    let m1 =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.0; 6.0; 7.0; 8.0 |]
           [| 9.0; 8.0; 7.0; 6.0 |]
           [| 5.0; 4.0; 3.0; 2.0 |] |]

    let m2 =
        [| [| 2.0; 3.0; 4.0; 5.0 |]
           [| 6.0; 7.0; 8.0; 9.0 |]
           [| 8.0; 7.0; 6.0; 5.0 |]
           [| 4.0; 3.0; 2.0; 1.0 |] |]

    m1 |> equals m2 |> should equal false
