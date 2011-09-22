// Learn more about F# at http://fsharp.net
#light

module bffsc

open System.IO
open Opcodes
open AvrBackend

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

let compile source_code emit_instruction = 
    source_code
    |> Seq.map get_opcode
    |> Seq.map emit_instruction
    |> Seq.concat

let write_elf_file machine_code =
    null

let main infile outfile arch =
    let source_code = File.ReadAllText(infile)
    let backend = 
        match arch with
        | "avr" -> avr_emitter
        // | "avr32" -> avr32_emitter
        | _ -> failwith ("Unknown architecture " + arch)

    let machine_code = compile source_code backend
    write_elf_file machine_code