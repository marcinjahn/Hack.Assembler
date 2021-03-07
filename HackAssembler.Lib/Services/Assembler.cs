using System;
using System.Collections.Generic;
using System.Linq;
using HackAssembler.Lib.Models;
using HackAssembler.Lib.Translation;

namespace HackAssembler.Lib.Services
{
    public class Assembler
    {
        private readonly LabelsScanner _labelsScanner;
        private readonly SymbolicTable _symbolicTable;
        private readonly InstructionTypeResolver _typeResolver;
        private readonly InstructionTranslatorFactory _translatorFactory;

        public Assembler(LabelsScanner labelsScanner, SymbolicTable symbolicTable, InstructionTypeResolver typeResolver,
            InstructionTranslatorFactory translatorFactory)
        {
            _labelsScanner = labelsScanner ?? throw new ArgumentNullException(nameof(labelsScanner));
            _symbolicTable = symbolicTable ?? throw new ArgumentNullException(nameof(symbolicTable));
            _typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            _translatorFactory = translatorFactory ?? throw new ArgumentNullException(nameof(translatorFactory));
        }

        public IReadOnlyCollection<string> Assemble(IEnumerable<InstructionLine> asmLines)
        {
            if (asmLines == null) throw new ArgumentNullException(nameof(asmLines));

            var instructionLines = asmLines as InstructionLine[] ?? asmLines.ToArray();
            _labelsScanner.FindSymbols(instructionLines, _symbolicTable);

            var machineCode = new List<string>();
            foreach (var asmLine in instructionLines)
            {
                var type = _typeResolver.GetInstructionType(asmLine);
                var translator = _translatorFactory.GetFor(type);
                var code = translator.Translate(asmLine);
                code.AppendTo(machineCode);
            }

            return machineCode.AsReadOnly();
        }
    }
}