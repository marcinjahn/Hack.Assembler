using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Translation
{
    public class LabelInstructionTranslator : IInstructionTranslator
    {
        public ICode Translate(InstructionLine line)
        {
            return new LabelCode(line.Text);
        }
    }
}