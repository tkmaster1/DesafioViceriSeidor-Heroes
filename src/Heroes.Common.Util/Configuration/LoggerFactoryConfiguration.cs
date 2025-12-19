using Heroes.Common.Util.Logger;
using Microsoft.Extensions.Logging;

namespace Heroes.Common.Util.Configuration;

public static class LoggerFactoryConfiguration
{
    public static void UseLoggerFactory(this ILoggerFactory loggerFactory, string path, string nomeArquivo)
    {
        if (loggerFactory == null) throw new ArgumentNullException(nameof(loggerFactory));

        loggerFactory.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration() { Path = path, NomeArquivo = nomeArquivo }));
    }
}