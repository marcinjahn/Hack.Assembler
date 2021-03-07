using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;
using HackAssembler.Lib;
using HackAssembler.Lib.Extensions;
using HackAssembler.Lib.Input;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HackAssembler.AssemblerCLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var command = GetCommand();
            // Parse the incoming args and invoke the handler
            await command.InvokeAsync(args);
        }
        
        private static IHostBuilder GetHostBuilder(FileInfo input, FileInfo output)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddCoreAssemblerServices();
                    services.AddInputFileSupport();
                    services.AddOutputFileSupport();
                    services.AddHostedService<HostedService>();
                    services.Configure<HostedServiceOptions>(options =>
                    {
                        options.InputPath = input.FullName;
                        options.OutputPath = output?.FullName;
                    });
                })
                .ConfigureLogging((hostBuilderContext, loggingBuilder) =>
                {
                    loggingBuilder.AddConfiguration(hostBuilderContext.Configuration.GetSection("Logging"));
                    loggingBuilder.AddConsole();
                });

            return builder;
        }

        private static RootCommand GetCommand()
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<FileInfo>(
                    new[] {"--input", "-i"},
                    "An ASM file to compile into machine code"),
                new Option<FileInfo>(
                    new[] {"--output", "-o"},
                    () => null,
                    "A result file to create"),
            };

            rootCommand.Description = "nand2tetris Hack Assembler CLI";

            // Note that the parameters of the handler method are matched according to the names of the options
            rootCommand.Handler = CommandHandler.Create((Func<FileInfo, FileInfo, Task>)(async (input, output) =>
            {
                try
                {
                    var builder = GetHostBuilder(input, output);
                    await builder.RunConsoleAsync(options => options.SuppressStatusMessages = true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Program run into an exception");
                    Console.WriteLine(e.Message);
                }
            }));

            return rootCommand;
        }
    }
}