module Utilities

let epsilon = 0.00001

let (=~) a b = a - b < epsilon
