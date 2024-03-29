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

[<Fact>]
let ``Multiplying two matrices`` () =
    let m1 =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 5.0; 6.0; 7.0; 8.0 |]
           [| 9.0; 8.0; 7.0; 6.0 |]
           [| 5.0; 4.0; 3.0; 2.0 |] |]

    let m2 =
        [| [| -2.0; 1.0; 2.0; 3.0 |]
           [| 3.0; 2.0; 1.0; -1.0 |]
           [| 4.0; 3.0; 6.0; 5.0 |]
           [| 1.0; 2.0; 7.0; 8.0 |] |]

    let expected =
        [| [| 20.0; 22.0; 50.0; 48.0 |]
           [| 44.0; 54.0; 114.0; 108.0 |]
           [| 40.0; 58.0; 110.0; 102.0 |]
           [| 16.0; 26.0; 46.0; 42.0 |] |]

    (m1, m2) ||> multiplyM |> equals expected |> should equal true

[<Fact>]
let ``A matrix multiplied by a tuple`` () =
    let m =
        [| [| 1.0; 2.0; 3.0; 4.0 |]
           [| 2.0; 4.0; 4.0; 2.0 |]
           [| 8.0; 6.0; 4.0; 1.0 |]
           [| 0.0; 0.0; 0.0; 1.0 |] |]

    let t = 1.0, 2.0, 3.0, 1.0

    let expected = 18.0, 24.0, 33.0, 1.0

    (m, t) ||> multiplyT |> should equal expected

[<Fact>]
let ``Multiplying a matrix by the identity matrix`` () =
    let m =
        [| [| 0.0; 1.0; 2.0; 4.0 |]
           [| 1.0; 2.0; 4.0; 8.0 |]
           [| 2.0; 4.0; 8.0; 16.0 |]
           [| 4.0; 8.0; 16.0; 32.0 |] |]

    (m, identityMatrix) ||> multiplyM |> equals m |> should equal true

[<Fact>]
let ``Multiplying the identity matrix by a tuple`` () =
    let t = 1.0, 2.0, 3.0, 4.0

    (identityMatrix, t) ||> multiplyT |> should equal t

[<Fact>]
let ``Transposing a matrix`` () =
    let m =
        [| [| 0.0; 9.0; 3.0; 0.0 |]
           [| 9.0; 8.0; 0.0; 8.0 |]
           [| 1.0; 8.0; 5.0; 3.0 |]
           [| 0.0; 0.0; 5.0; 8.0 |] |]

    let expected =
        [| [| 0.0; 9.0; 1.0; 0.0 |]
           [| 9.0; 8.0; 8.0; 0.0 |]
           [| 3.0; 0.0; 5.0; 5.0 |]
           [| 0.0; 8.0; 3.0; 8.0 |] |]

    m |> transpose |> equals expected |> should equal true

[<Fact>]
let ``Transposing the identity matrix`` () =
    identityMatrix |> transpose |> equals identityMatrix |> should equal true

[<Fact>]
let ``Calculating the determinant of a 2x2 matrix`` () =
    let m = [| [| 1.0; 5.0 |]; [| -3.0; 2.0 |] |]

    m |> determinant |> should equal 17.0

[<Fact>]
let ``A submatrix of a 3x3 matrix is a 2x2 matrix`` () =
    let m = [| [| 1.0; 5.0; 0.0 |]; [| -3.0; 2.0; 7.0 |]; [| 0.0; 6.0; -3.0 |] |]

    let expected = [| [| -3.0; 2.0 |]; [| 0.0; 6.0 |] |]

    m |> submatrix 0 2 |> equals expected |> should equal true

[<Fact>]
let ``A submatrix of a 4x4 matrix is a 3x3 matrix`` () =
    let m =
        [| [| -6.0; 1.0; 1.0; 6.0 |]
           [| -8.0; 5.0; 8.0; 6.0 |]
           [| -1.0; 0.0; 8.0; 2.0 |]
           [| -7.0; 1.0; -1.0; 1.0 |] |]

    let expected =
        [| [| -6.0; 1.0; 6.0 |]; [| -8.0; 8.0; 6.0 |]; [| -7.0; -1.0; 1.0 |] |]

    m |> submatrix 2 1 |> equals expected |> should equal true

[<Fact>]
let ``Calculating a minor of a 3x3 matrix`` () =
    let m = [| [| 3.0; 5.0; 0.0 |]; [| 2.0; -1.0; -7.0 |]; [| 6.0; -1.0; 5.0 |] |]

    let b = submatrix 1 0 m

    b |> determinant |> should equal 25.0

    m |> minor 1 0 |> should equal 25.0

[<Fact>]
let ``Calculating a cofactor of a 3x3 matrix`` () =
    let m = [| [| 3.0; 5.0; 0.0 |]; [| 2.0; -1.0; -7.0 |]; [| 6.0; -1.0; 5.0 |] |]

    m |> minor 0 0 |> should equal -12.0

    m |> cofactor 0 0 |> should equal -12.0

    m |> minor 1 0 |> should equal 25.0

    m |> cofactor 1 0 |> should equal -25.0
    
[<Fact>]
let ``Calculating the determinant of a 3x3 matrix`` () =
    let m = [| [| 1.0; 2.0; 6.0 |]; [| -5.0; 8.0; -4.0 |]; [| 2.0; 6.0; 4.0 |] |]

    m |> cofactor 0 0 |> should equal 56.0

    m |> cofactor 0 1 |> should equal 12.0

    m |> cofactor 0 2 |> should equal -46.0

    m |> determinant |> should equal -196.0

[<Fact>]
let ``Calculating the determinant of a 4x4 matrix`` () =
    let m =
        [| [| -2.0; -8.0; 3.0; 5.0 |]
           [| -3.0; 1.0; 7.0; 3.0 |]
           [| 1.0; 2.0; -9.0; 6.0 |]
           [| -6.0; 7.0; 7.0; -9.0 |] |]

    m |> cofactor 0 0 |> should equal 690.0

    m |> cofactor 0 1 |> should equal 447.0

    m |> cofactor 0 2 |> should equal 210.0

    m |> cofactor 0 3 |> should equal 51.0

    m |> determinant |> should equal -4071.0
