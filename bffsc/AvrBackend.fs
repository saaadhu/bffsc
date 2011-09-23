module AvrBackend
open Opcodes
// Uses X to store the pointer
let generate_initialize_x =
    0b1110011010100000 // LDI R26, 0x60

let generate_load_to_r = 
    0b1001000100001100; // LD R16, X

let generate_inc_r = 
    0b1001010100000011; // INC R16

let generate_inc_x = 
    0b10010101101000011; // INC X

let generate_dec_r = 
    0b1001010100001010; // DEC R16

let generate_dec_x = 
    0b10010101101001010; // DEC X

let generate_write_to_x =
    0b1001001100001100; // ST R16, X

let generate_set_r17 =
    0b1110111100011111 // LDI R17, 0xFF

let generate_clear_r17 =
    0b1110000000010000 // LDI R17, 0x00

let generate_in_porta =
    0b1011001100001011 // IN PORTA, R16

let generate_out_ddra =
    0b1011111100011010 // OUT DDRA, R17
    
let generate_out_ddrb =
    0b1011101100010111 // OUT DDRB, R17

let generate_out_portb =
    0b1011101100001000 // OUT PORTB, R16

let generate_increment_machine_code =
    [generate_load_to_r; generate_inc_r; generate_write_to_x]

let generate_read_machine_code =
    [generate_in_porta; generate_write_to_x] // IN R16, PORTA; ST R16, X

let generate_write_machine_code =
    [generate_load_to_r; generate_out_portb] // LD R16, X; OUT PORTB, R16

let generate_decrement_machine_code =
    [generate_load_to_r; generate_dec_r; generate_write_to_x]

let generate_moveright_machine_code =
    [generate_inc_x]

let generate_moveleft_machine_code =
    [generate_dec_x]

let generate_porta_input =
    [generate_set_r17; generate_out_ddra]

let generate_portb_output =
    [generate_clear_r17; generate_out_ddrb]

let generate_prologue_code =
    let port_init_code = List.append generate_porta_input  generate_portb_output
    List.append port_init_code [generate_initialize_x]

let avr_emitter opcode =
    match opcode with
    | Increment -> generate_increment_machine_code
    | Decrement -> generate_decrement_machine_code
    | MoveLeft -> generate_moveleft_machine_code
    | MoveRight -> generate_moveright_machine_code
    | Input -> generate_read_machine_code
    | Output -> generate_write_machine_code
