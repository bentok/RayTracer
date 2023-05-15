module Colors

open Utilities

type Color = float * float * float

let add (c1: Color) (c2: Color) =
    let r1, g1, b1 = c1
    let r2, g2, b2 = c2
    r1 + r2, g1 + g2, b1 + b2

let subtract (c1: Color) (c2: Color) =
    let r1, g1, b1 = c1
    let r2, g2, b2 = c2
    r1 - r2, g1 - g2, b1 - b2

let multiply (c: Color) m =
    let r, g, b = c
    r * m, g * m, b * m

let hadamardProduct (c1: Color) (c2: Color) =
    let r1, g1, b1 = c1
    let r2, g2, b2 = c2
    r1 * r2, g1 * g2, b1 * b2
