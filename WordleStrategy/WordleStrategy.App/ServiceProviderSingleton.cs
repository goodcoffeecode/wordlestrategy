using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WordleStrategy.App
{
    internal static class ServiceProviderSingleton
    {
        private static readonly ServiceCollection _services;
        internal static IServiceProvider Instance { get; private set; }

        static ServiceProviderSingleton()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _services = new ServiceCollection();

            Instance = _services
                .AddLogging(config => config.AddConsole())
                .Configure<FilePathOptions>(o => config.GetSection("FilePaths").Bind(o))
                .AddSingleton<IWordStreamer, WordStreamerFromFile>()
                .AddSingleton<ICharacterRanker, CharacterRanker>()
                .AddSingleton<IWordRanker, NoDuplicateLettersWordRanker>()
                .AddSingleton<Runner>()
                .BuildServiceProvider();
        }
    }
}