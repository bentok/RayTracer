open RayTracer.PointsAndVectors

type Projectile = {
    position: PointOrVector
    velocity: PointOrVector }

type Environment = {
    gravity: PointOrVector
    wind: PointOrVector }

let tick env proj =
    { position = add proj.position proj.velocity
      velocity = add proj.velocity env.gravity |> add env.wind }

let projectile = {
    position = point (0, 1, 0)
    velocity = vector (1, 1, 0) |> normalize }

let environment = {
    gravity = vector (0, -0.1, 0)
    wind = vector (-0.01, 0, 0) }

let simulate env =
    let rec loop (proj: Projectile) =
        printfn $"Position: %A{proj.position}. Velocity %A{proj.velocity}"
        
        match proj.position with
        | _, y, _, _ when y > 0 -> tick env proj |> loop
        | _ -> printfn "Projectile has reached zero velocity"
    loop projectile
    
simulate environment