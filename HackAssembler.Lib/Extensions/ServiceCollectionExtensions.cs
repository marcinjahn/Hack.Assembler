using System.IO;
using HackAssembler.Lib.Input;
using HackAssembler.Lib.Output;
using HackAssembler.Lib.Services;
using HackAssembler.Lib.Translation;
using Microsoft.Extensions.DependencyInjection;

namespace HackAssembler.Lib.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreAssemblerServices(this IServiceCollection services)
        {
            services.AddTransient<AInstructionTranslator>();
            services.AddTransient<LabelInstructionTranslator>();
            services.AddTransient<CInstructionTranslator>();
            services.AddTransient<CComputationParser>();
            services.AddTransient<CDestinationParser>();
            services.AddTransient<CJumpParser>();
            services.AddTransient<InstructionTranslatorFactory>();
            services.AddSingleton<SymbolicTable>();
            services.AddTransient<VariableAddressGenerator>();
            services.AddTransient<Assembler>();
            services.AddTransient<InstructionTypeResolver>();
            services.AddTransient<LabelsScanner>();

            return services;
        }

        public static IServiceCollection AddInputFileSupport(this IServiceCollection services)
        {
            services.AddTransient<SourceBrowser>();
            services.AddTransient<SourceBrowserFactory>();

            return services;
        }

        public static IServiceCollection AddOutputFileSupport(this IServiceCollection services)
        {
            services.AddTransient<FileSaver>();

            return services;
        }
    }
}