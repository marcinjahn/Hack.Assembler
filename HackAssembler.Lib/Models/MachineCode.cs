using System;
using System.Collections.Generic;

namespace HackAssembler.Lib.Models
{
    public class MachineCode : ICode
    {
        private readonly string _bits;

        public MachineCode(string bits)
        {
            _bits = bits ?? throw new ArgumentNullException(nameof(bits));
        }
        
        public void AppendTo(ICollection<string> result)
        {
            result.Add(_bits);
        }

        public override string ToString()
        {
            return _bits;
        }
    }
}