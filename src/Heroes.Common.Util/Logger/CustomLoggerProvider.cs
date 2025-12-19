using Heroes.Common.Util.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Heroes.Common.Util.Logger;

public class CustomLoggerProvider : ILoggerProvider
{
    #region Properties

    readonly CustomLoggerProviderConfiguration _loggerConfig;
    readonly ConcurrentDictionary<string, CustomLogger> _loggers = new();

    #endregion

    #region Constructor

    public CustomLoggerProvider(CustomLoggerProviderConfiguration config) => _loggerConfig = config;

    #endregion

    #region Methods

    public ILogger CreateLogger(string category)
    {
        return _loggers.GetOrAdd(category, name => new CustomLogger(name, _loggerConfig));
    }

    public void Dispose() => GC.SuppressFinalize(this);

    #endregion
}