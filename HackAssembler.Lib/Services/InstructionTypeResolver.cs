using System;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Services
{
    public class InstructionTypeResolver
    {
        public InstructionType GetInstructionType(InstructionLine line)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));

            if (line.Text.StartsWith("@"))
            {
                return InstructionType.A;
            }
            else if (line.Text.StartsWith("("))
            {
                return InstructionType.Label;
            }
            else
            {
                return InstructionType.C;
            }
        }
    }
}