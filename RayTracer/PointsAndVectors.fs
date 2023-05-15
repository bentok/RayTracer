module PointsAndVectors

open System
open Utilities

type PointOrVector = float * float * float * float

let (|Point|Vector|) (value: PointOrVector) =
    match value with
    | _, _, _, x when x = 1.0 -> Point
    | _, _, _, x when x = 0.0 -> Vector
    | _ -> failwith "Value is not a point or vector"

let (==) (v1: PointOrVector) (v2: PointOrVector) =
    let x1, y1, z1, _ = v1
    let x2, y2, z2, _ = v2
    x1 =~ x2 && y1 =~ y2 && z1 =~ z2

let add (a1: PointOrVector) (a2: PointOrVector) =
    let x1, y1, z1, w1 = a1
    let x2, y2, z2, w2 = a2
    x1 + x2, y1 + y2, z1 + z2, Math.Clamp(w1 + w2, 0.0, 1.0)

let subtract (a1: PointOrVector) (a2: PointOrVector) =
    let x1, y1, z1, w1 = a1
    let x2, y2, z2, w2 = a2
    x1 - x2, y1 - y2, z1 - z2, Math.Clamp(w1 - w2, 0.0, 1.0)

let negate (t: PointOrVector) =
    let x, y, z, w = t
    -x, -y, -z, -w

let multiply (t: PointOrVector) m =
    let x, y, z, w = t
    x * m, y * m, z * m, w * m

let divide (t: PointOrVector) d =
    let x, y, z, w = t
    x / d, y / d, z / d, w / d

let magnitude (v: PointOrVector) =
    let x, y, z, w = v
    x ** 2 + y ** 2 + z ** 2 + w ** 2 |> sqrt

let normalize v =
    let x, y, z, w = v
    let m = magnitude v
    x / m, y / m, z / m, w / m

let dot (a1: PointOrVector) (a2: PointOrVector) =
    let x1, y1, z1, w1 = a1
    let x2, y2, z2, w2 = a2
    x1 * x2 + y1 * y2 + z1 * z2 + w1 * w2

let cross (a1: PointOrVector) (a2: PointOrVector) =
    let x1, y1, z1, _ = a1
    let x2, y2, z2, _ = a2
    y1 * z2 - z1 * y2, z1 * x2 - x1 * z2, x1 * y2 - y1 * x2, 0.0
