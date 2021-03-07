using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HackAssembler.Lib.Models;
using Microsoft.Extensions.Options;

namespace HackAssembler.Lib.Input
{
    public class SourceBrowser
    {

        private readonly SourceBrowserOptions _options;
        private string[] _lines;
        private int _realLineIndex;
        private int _virtualLineIndex;
        
        public SourceBrowser(IOptions<SourceBrowserOptions> options)
        {
            _options = options.Value;
        }

        public async Task Initialize(CancellationToken ct)
        {
            if (_lines is not null)
            {
                return;
            }
            
            _lines = await File.ReadAllLinesAsync(_options.AsmFilePath, ct);
        }

        public InstructionLine GetNextLine()
        {
            //could be recursive as well
            while (true)
            {
                if (!HasMore())
                {
                    return null;
                }

                var line = _lines[_realLineIndex++];
                line = Clean(line);

                if (IsValid(line))
                {
                    return new InstructionLine(line, _virtualLineIndex++);
                }
            }
        }

        public bool HasMore()
        {
            return _lines.Length > _realLineIndex;
        }

        private string Clean(string line)
        {
            var cleanLine = line.Replace(" ", string.Empty);
            var indexOfComment = cleanLine.IndexOf("/", StringComparison.Ordinal);
            if (indexOfComment != -1)
            {
                cleanLine = cleanLine.Substring(0, indexOfComment);
            }

            return cleanLine;
        }
        
        private bool IsValid(string line)
        {
            return !(line.StartsWith('/') || String.IsNullOrWhiteSpace(line));
        }

        public void StartOver()
        {
            _realLineIndex = 0;
            _virtualLineIndex = 0;
        }
    }
}