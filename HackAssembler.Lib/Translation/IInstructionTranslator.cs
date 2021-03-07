using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public interface IInstructionTranslator
    {
        ICode Translate(InstructionLine line);
    }
}