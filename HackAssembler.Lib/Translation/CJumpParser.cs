using System;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public class CJumpParser
    {
        public static string GetJumpBits(InstructionLine line)
        {
            var colonIndex = line.Text.IndexOf(";", StringComparison.Ordinal);
            if (colonIndex != -1)
            {
                var jump = line.Text.Substring(colonIndex + 1);

                return jump switch
                {
                    "JGT" => "001",
                    "JEQ" => "010",
                    "JGE" => "011",
                    "JLT" => "100",
                    "JNE" => "101",
                    "JLE" => "110",
                    "JMP" => "111",
                    _ => throw new ArgumentOutOfRangeException(nameof(line),
                        "The supplied JUMP command is not supported")
                };
            }
            else
            {
                //null
                return "000";
            }
        }
    }
}