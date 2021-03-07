using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HackAssembler.Lib;
using HackAssembler.Lib.Input;
using HackAssembler.Lib.Models;
using HackAssembler.Lib.Output;
using HackAssembler.Lib.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HackAssembler.AssemblerCLI
{
    public class HostedService : BackgroundService
    {
        public HostedService(SourceBrowserFactory sourceBrowserFactory, Assembler assembler, IOptions<HostedServiceOptions> options,
            FileSaver saver, IHostApplicationLifetime appLifetime)
        {
            _sourceBrowserFactory = sourceBrowserFactory ?? throw new ArgumentNullException(nameof(sourceBrowserFactory));
            _assembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
            _options = options.Value;
        }

        private readonly SourceBrowserFactory _sourceBrowserFactory;
        private readonly Assembler _assembler;
        private readonly HostedServiceOptions _options;
        private readonly FileSaver _saver;
        private readonly IHostApplicationLifetime _appLifetime;
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("HACK Assembler");
            Console.WriteLine($"Input file: {_options.InputPath}");
            Console.WriteLine($"Output file: {_options.OutputPath ?? "NONE"}");
            
            var sourceBrowser = _sourceBrowserFactory.Create(_options.InputPath);
            await sourceBrowser.Initialize(stoppingToken);
            
            var asmLines = new List<InstructionLine>();
            while (sourceBrowser.HasMore())
            {
                asmLines.Add(sourceBrowser.GetNextLine());
            }
            var machineCode = _assembler.Assemble(asmLines);

            await DealWithOutput(machineCode);
            
            _appLifetime.StopApplication();
        }

        private async Task DealWithOutput(IReadOnlyCollection<string> machineCode)
        {
            if (_options.OutputPath is null)
            {
                Console.WriteLine("Machine code:");
                foreach (var line in machineCode)
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                await _saver.Save(machineCode, _options.OutputPath);
                Console.WriteLine("Output saved in file");
            }

        }
    }
}