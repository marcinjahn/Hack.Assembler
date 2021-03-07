using System.Threading;
using System.Threading.Tasks;
using HackAssembler.Lib;
using HackAssembler.Lib.Input;
using HackAssembler.Lib.Models;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace HackAssembler.Lib.Tests
{
    public class SourceBrowserTests
    {
        [TestCase("FileWithoutCommentsNorEmptyLines.asm", 4)]
        [TestCase("FileWithComments.asm", 3)]
        [TestCase("FileWithEmptyLines.asm", 3)]
        [TestCase("FileWithCommentsAndEmptyLines.asm", 2)]
        public async Task GetNextLine_FilesAreProvied_CorrectNumberOfLinesIsReturned(string fileName, int expectedLinesNumber)
        {
            var sut = new SourceBrowser(
                new OptionsWrapper<SourceBrowserOptions>(
                    new SourceBrowserOptions{AsmFilePath = $"{TestContext.CurrentContext.TestDirectory}/SourceBrowserTestFiles/{fileName}"}));
            await sut.Initialize(CancellationToken.None);

            var numberOfLines = 0;
            while (sut.GetNextLine() != null)
            {
                numberOfLines++;
            }
            
            Assert.AreEqual(expectedLinesNumber, numberOfLines);
        }
        
        [TestCase("FileWithoutCommentsNorEmptyLines.asm", 3)]
        [TestCase("FileWithComments.asm", 2)]
        [TestCase("FileWithEmptyLines.asm", 2)]
        [TestCase("FileWithCommentsAndEmptyLines.asm", 1)]
        public async Task GetNextLine_FilesAreProvied_CorrectLastAddressIsReturned(string fileName, int expectedLinesNumber)
        {
            var sut = new SourceBrowser(
                new OptionsWrapper<SourceBrowserOptions>(
                    new SourceBrowserOptions{AsmFilePath = $"{TestContext.CurrentContext.TestDirectory}/SourceBrowserTestFiles/{fileName}"}));
            await sut.Initialize(CancellationToken.None);
            
            InstructionLine line = null;
            var nextLine = sut.GetNextLine();
            while (nextLine != null)
            {
                line = nextLine;
                nextLine = sut.GetNextLine();
            }
            
            Assert.AreEqual(expectedLinesNumber, line.Address);
        }

        [Test]
        public async Task GetNextLine_FileIsProvided_ReturnedLineHasNoWhitespace()
        {
            var sut = new SourceBrowser(
                new OptionsWrapper<SourceBrowserOptions>(
                    new SourceBrowserOptions{AsmFilePath = $"{TestContext.CurrentContext.TestDirectory}/SourceBrowserTestFiles/FileWithComandWithWhitespaces.asm"}));
            await sut.Initialize(CancellationToken.None);

            var line = sut.GetNextLine().Text;
            
            Assert.AreEqual("a=b", line);
        }
    }
}