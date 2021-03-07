using Microsoft.Extensions.Options;

namespace HackAssembler.Lib.Input
{
    public class SourceBrowserFactory
    {
        public SourceBrowser Create(string filePath)
        {
            return new SourceBrowser(new OptionsWrapper<SourceBrowserOptions>(
                new SourceBrowserOptions {AsmFilePath = filePath}));
        }
    }
}