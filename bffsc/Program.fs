// Learn more about F# at http://fsharp.net
#light

module bffsc

open System.IO

open Opcodes
open Lexer
open AvrBackend
open HexFile

let compile source_code emit_instruction = 
    source_code
    |> Seq.map get_opcode
    |> Seq.map emit_instruction
    |> Seq.concat

let main infile outfile arch =
    let source_code = File.ReadAllText(infile)
    let backend = 
        match arch with
        | "avr" -> avr_emitter
        | _ -> failwith ("Unknown architecture " + arch)

    let machine_code = compile source_code backend
    let hex_file_contents = get_hex_file_contents machine_code
    File.WriteAllText(outfile, hex_file_contents)