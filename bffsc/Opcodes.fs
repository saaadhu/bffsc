#light

module Opcodes

type opcode =
    | MoveLeft
    | MoveRight
    | Increment
    | Decrement
    | Input
    | Output
    | LoopStart
    | LoopEnd
    | Unknown

