using System;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public class CDestinationParser
    {
        public static string GetDestBits(InstructionLine line)
        {
            var indexOfEquals = line.Text.IndexOf("=", StringComparison.Ordinal);
            if (indexOfEquals == -1)
            {
                return "000"; //null
            }

            var destination = line.Text.Substring(0, indexOfEquals);

            return destination switch
            {
                "M" => "001",
                "D" => "010",
                "MD" => "011",
                "A" => "100",
                "AM" => "101",
                "AD" => "110",
                "AMD" => "111",
                _ => throw new ArgumentOutOfRangeException(nameof(line),
                    "The supplied DESTINATION command is not supported")
            };
        }
    }
}