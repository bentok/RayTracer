namespace RayTracer

open System

module Tuples =
    type Coords = float * float * float
    type PointOrVector = float * float * float * float
    
    let point (coords: Coords): PointOrVector =
        let x, y, z = coords
        (x, y, z, 1.0)
        
    let vector (coords: Coords): PointOrVector =
        let x, y, z = coords
        (x, y, z, 0.0)
        
    let add (a1: PointOrVector) (a2: PointOrVector) =
        let x1, y1, z1, w1 = a1
        let x2, y2, z2, w2 = a2
        x1 + x2, y1 + y2, z1 + z2, Math.Clamp(w1 + w2, 0.0, 1.0)