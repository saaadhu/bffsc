// Learn more about F# at http://fsharp.net
#light

module bffsc

open System
open System.IO

open Opcodes
open Lexer
open AvrBackend
open HexFile

let compile source_code (emit_instructions:Opcodes.opcode -> int list) = 
    let known_opcode op = op <> Unknown
    let gen_code =
        source_code
        |> Seq.map get_opcode
        |> Seq.filter known_opcode
        |> Seq.map emit_instructions
        |> Seq.concat
    List.append generate_prologue_code (Seq.toList gen_code)


let main infile outfile arch =
    let source_code = File.ReadAllText(infile)
    let backend = 
        match arch with
        | "avr" -> avr_emitter
        | _ -> failwith ("Unknown architecture " + arch)

    let machine_code = compile source_code backend
    let hex_file_contents = get_hex_file_contents machine_code
    File.WriteAllText(outfile, hex_file_contents)

let cmd_args = Environment.GetCommandLineArgs()
main cmd_args.[1] cmd_args.[2] cmd_args.[3]