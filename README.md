# Ray Tracer Challenge in F#

This repository is a for-fun project I'm working on as I have some free time. I'm going through [Jamis Buck's book ray tracer challenge](https://pragprog.com/titles/jbtracer/the-ray-tracer-challenge/), which is a language-agnostic book on how to build a ray tracer, guiding primarily through cucumber-style TDD test cases.

## F# Implementation

This is an F# implementation, focusing on the elegance and ergonomics of functional programming. Key features include:

- **Function Preference:** Functions are separated by modules rather than classes, aiming for simplicity and ease of composition.
- **Active Pattern Matching & Types:** Preference for active pattern matching and primitive types, avoiding complex types or built-in modules like Array2d and Matrix.
- **Performance Considerations:** While the code is primarily functional, performance-critical sections may end up utilizing classes, mutability, etc. For example, the canvas module now uses arrays instead of lists.
- **Pipeline Friendly:** Code aims at being structured to be easily composed and pipeline-friendly, to appreciate the beauty of F#.

## Project Structure

The project is broken up by modules that group together like functions, such as tuples, matrices, colors, etc. For each "capstone," a separate project uses all modules created up until that point. These projects are prefixed "Samples," so Samples.Tuples, Samples.Canvas, etc.

## Emphasis on Simplicity

While performance considerations may necessitate some rewrites, the overarching approach favors simplicity. It's an opportunity to enjoy the nuances of F# in an engaging, hands-on way.

## Contributing

Feel free to explore the code, fork it, use any or all of it for yourself. This is just for fun and the all credit really goes to Jamis Buck for writing his awesome book anyway. Happy coding!