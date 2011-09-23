module Lexer

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
    | _ -> failwith ("Illegal character " + char.ToString())