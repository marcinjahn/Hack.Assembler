using System;
using System.Collections.Generic;
using HackAssembler.Lib.Models;

namespace HackAssembler.Lib.Services
{
    public class LabelsScanner
    {
        private readonly InstructionTypeResolver _typeResolver;

        public LabelsScanner(InstructionTypeResolver typeResolver)
        {
            _typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
        }

        public void FindSymbols(IEnumerable<InstructionLine> asmLines, SymbolicTable symbolicTable)
        {
            var labelsCount = 0;
            foreach (var line in asmLines)
            {
                if (_typeResolver.GetInstructionType(line) != InstructionType.Label)
                    continue;

                var label = new LabelCode(line.Text);
                symbolicTable.Add(label.ToString(), line.Address - labelsCount++);
            }
        }
    }
}