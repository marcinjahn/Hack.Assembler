using System;
using HackAssembler.Lib.Extensions;
using HackAssembler.Lib.Models;
using HackAssembler.Lib.Services;

namespace HackAssembler.Lib.Translation
{
    public class AInstructionTranslator : IInstructionTranslator
    {
        private readonly SymbolicTable _symbolicTable;
        private readonly VariableAddressGenerator _variableAddressGenerator;
        
        public AInstructionTranslator(SymbolicTable symbolicTable, VariableAddressGenerator variableAddressGenerator)
        {
            _symbolicTable = symbolicTable ?? throw new ArgumentNullException(nameof(symbolicTable));
            _variableAddressGenerator = variableAddressGenerator ?? throw new ArgumentNullException(nameof(variableAddressGenerator));
        }

        public ICode Translate(InstructionLine line)
        {
            var argument = line.Text.Remove(0, 1);
            return new MachineCode(
                IsSymbolic(argument) ? 
                    HandleSymbolicArgument(argument) : 
                    GetBinaryForm(argument));
        }

        private string HandleSymbolicArgument(string argument)
        {
            if (_symbolicTable.Contains(argument))
            {
                return GetBinaryForm(_symbolicTable.GetAddress(argument).ToString());
            }
            else
            {
                var newVariableAddress = _variableAddressGenerator.GenerateNew();
                _symbolicTable.Add(argument, newVariableAddress);
                return GetBinaryForm(newVariableAddress.ToString());
            }
        }

        private string GetBinaryForm(string address)
        {
            return "0" + int.Parse(address).ToBinary(15);
        }

        private bool IsSymbolic(string address)
        {
            return !int.TryParse(address, out _);
        }
    }
}