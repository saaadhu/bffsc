module AvrBackend
open Opcodes
// Uses X to store the pointer

let generate_load_to_r = 
    0b1001000100001100; // LD R16, X

let generate_inc_r = 
    0b1001010100000011; // INC R16

let generate_write_to_x =
    0b1001001100001100; // ST R16, X

let generate_increment_machine_code =
    [generate_load_to_r; generate_inc_r; generate_write_to_x]

let avr_emitter opcode =
    match opcode with
    | Increment -> generate_increment_machine_code
