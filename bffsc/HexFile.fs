
module HexFile
open System

//let rec split_chunks (machine_code:seq<int>) : list<list<int>> = 
//    let chunk_size = 2
//    let machine_code_length = Seq.length(machine_code)
//
//    if (chunk_size > machine_code_length) then [Seq.toList machine_code]
//    else List.append [Seq.toList (Seq.take chunk_size machine_code)] (split_chunks (Seq.skip chunk_size machine_code))
//
//let encode_line line =
//    let byte_count = line.Length / 2
//

let rec calculate_sum (string:String) =
    if String.IsNullOrEmpty(string) then 0
    else
        let byte = string.Substring(0, 2)
        let num_byte = Convert.ToByte(byte, 16)
        calculate_sum (string.Substring(2)) + (int)num_byte

let get_hex_file_contents (machine_code:seq<int>) = 
    let size = String.Format("{0:X2}", (Seq.length machine_code) * 2)
    let address = "0000"
    let record_type = "00"
    let data = machine_code |> Seq.map (fun code -> String.Format("{0:X4}", code)) |> Seq.concat

    let body = size + address + record_type + new String(Seq.toArray data)
    let sum = calculate_sum body
    let checksum = ((sum &&& 0xFF) ^^^ 0xFF) + 1

    ":" + body + String.Format("{0:X2}", checksum) + Environment.NewLine + ":00000001FF"
