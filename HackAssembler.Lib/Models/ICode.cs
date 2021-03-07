using System.Collections.Generic;

namespace HackAssembler.Lib.Models
{
    public interface ICode
    {
        void AppendTo(ICollection<string> result);
    }
}