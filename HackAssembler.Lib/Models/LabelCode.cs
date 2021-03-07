using System;
using System.Collections.Generic;

namespace HackAssembler.Lib.Models
{
    public class LabelCode : ICode
    {
        private readonly string _label;
        
        public LabelCode(string label)
        {
            _label = label?.Replace("(", string.Empty).Replace(")", string.Empty) ?? 
                     throw new ArgumentNullException(nameof(label));
        }
        
        public void AppendTo(ICollection<string> result)
        {
            // Label has no representation in machine code
        }

        public override string ToString()
        {
            return _label;
        }
    }
}