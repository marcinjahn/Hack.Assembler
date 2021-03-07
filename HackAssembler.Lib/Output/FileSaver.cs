using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HackAssembler.Lib.Output
{
    public class FileSaver
    {
        public async Task Save(IEnumerable<string> machineCode, string path)
        {
            await File.WriteAllLinesAsync(path, machineCode);
        }
    }
}