module Tuples

open PointsAndVectors
open Canvas

type Projectile =
    { position: PointOrVector
      velocity: PointOrVector }

type Environment =
    { gravity: PointOrVector
      wind: PointOrVector }

let tick env proj =
    { position = add proj.position proj.velocity
      velocity = add proj.velocity env.gravity |> add env.wind }

let projectile =
    { position = 0.0, 1.0, 0.0, 1.0
      velocity = (1.0, 1.0, 0.0, 0.0) |> normalize |> multiply 11.25 }

let environment =
    { gravity = 0.0, -0.1, 0.0, 0.0
      wind = -0.01, 0.0, 0.0, 0.0 }

let c = canvas 900 550

let color = 0.8, 0.0, 0.0

let simulate env canvas =
    let rec loop (proj: Projectile) (cc: Canvas) =
        printfn $"Position: %A{proj.position}. Velocity %A{proj.velocity}"

        match proj.position with
        | x, y, _, _ when y > 0 ->
            (tick env proj, writePixel cc (x |> ceil |> int) (500 - (y |> ceil |> int)) color)
            ||> loop
        | _ -> cc

    loop projectile canvas

simulate environment c |> canvasToPpm |> writeStringToPPMFile "projectile.ppm"
