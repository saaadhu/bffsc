module Lexer
open Opcodes

let get_opcode char =
    match char with
    | '<' -> MoveLeft
    | '>' -> MoveRight
    | '+' -> Increment
    | '-' -> Decrement
    | ',' -> Input
    | '.' -> Output
    | '[' -> LoopStart
    | ']' -> LoopEnd
    | _ -> Unknown