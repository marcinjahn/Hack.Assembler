using System;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public class InstructionTranslatorFactory
    {
        private readonly AInstructionTranslator _a;
        private readonly CInstructionTranslator _c;
        private readonly LabelInstructionTranslator _l;
        
        public InstructionTranslatorFactory(AInstructionTranslator a, CInstructionTranslator c, LabelInstructionTranslator l)
        {
            _a = a ?? throw new ArgumentNullException(nameof(a));
            _c = c ?? throw new ArgumentNullException(nameof(c));
            _l = l ?? throw new ArgumentNullException(nameof(l));
        }

        public IInstructionTranslator GetFor(InstructionType instructionType)
        {
            return instructionType switch
            {
                InstructionType.A => _a,
                InstructionType.C => _c,
                InstructionType.Label => _l,
                _ => throw new ArgumentOutOfRangeException(nameof(instructionType), instructionType, null)
            };
        }
    }
}