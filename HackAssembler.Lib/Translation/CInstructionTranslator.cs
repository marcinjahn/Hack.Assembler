using System;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public class CInstructionTranslator : IInstructionTranslator
    {
        public CInstructionTranslator(CDestinationParser destParser, CComputationParser compParser,
            CJumpParser jumpParser)
        {
            _destParser = destParser ?? throw new ArgumentNullException(nameof(destParser));
            _compParser = compParser ?? throw new ArgumentNullException(nameof(compParser));
            _jumpParser = jumpParser ?? throw new ArgumentNullException(nameof(jumpParser));
        }

        private readonly CDestinationParser _destParser;
        private readonly CComputationParser _compParser;
        private readonly CJumpParser _jumpParser;
        
        public ICode Translate(InstructionLine line)
        {
            return new MachineCode(
                $"111{CComputationParser.GetCompBits(line)}{CDestinationParser.GetDestBits(line)}{CJumpParser.GetJumpBits(line)}");
        }
    }
}