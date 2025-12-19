using Heroes.Common.Util.Configuration;
using Microsoft.Extensions.Logging;

namespace Heroes.Common.Util.Logger;

public class CustomLogger : ILogger
{
    #region Properties

    public static object _Locker = new object();
    readonly string _LoggerName;
    readonly CustomLoggerProviderConfiguration _ProviderConfiguration;

    #endregion

    #region Constructor

    public CustomLogger(string name, CustomLoggerProviderConfiguration configuration)
    {
        this._LoggerName = name;
        this._ProviderConfiguration = configuration;
    }

    #endregion

    #region Methods

    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
        Exception exception, Func<TState, Exception, string> formatter)
    {
        string mensagem = string.Format("{0}", formatter(state, exception));

        if (logLevel == LogLevel.Error)
            InserirLog(mensagem);
    }

    private void InserirLog(string mensagem)
    {
        if (!Directory.Exists(_ProviderConfiguration.Path))
        {
            Directory.CreateDirectory(_ProviderConfiguration.Path);
        }

        if (!mensagem.Contains("[Database]") && !mensagem.Contains("[Sistema]"))
        {
            string caminhoArquivoLogSistema = "";

            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    caminhoArquivoLogSistema = _ProviderConfiguration.Path + "sistema_" + _ProviderConfiguration.NomeArquivo;
                else
                    caminhoArquivoLogSistema = _ProviderConfiguration.Path + "bd_" + _ProviderConfiguration.NomeArquivo;

                lock (_Locker)
                {
                    if (!File.Exists(caminhoArquivoLogSistema))
                    {
                        using (var stream = new FileStream(caminhoArquivoLogSistema, FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (StreamWriter streamWriter = new(stream))
                            {
                                if (mensagem.ToLower() != "init")
                                    streamWriter.WriteLine(mensagem);

                                streamWriter.Flush();
                                streamWriter.Close();
                            }
                        }
                    }
                    else
                    {
                        using (StreamWriter streamWriter = new(caminhoArquivoLogSistema, true))
                        {
                            if (mensagem.ToLower() != "init")
                                streamWriter.WriteLine(mensagem);

                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                }
            }
        }
        else
        {
            string nomeArquivo = mensagem.Contains("[Database]")
                       ? "bd_" + _ProviderConfiguration.NomeArquivo
                       : "sistema_" + _ProviderConfiguration.NomeArquivo;

            string caminhoArquivoLog = _ProviderConfiguration.Path + nomeArquivo;

            lock (_Locker)
            {
                if (!File.Exists(caminhoArquivoLog))
                {
                    using (var stream = new FileStream(caminhoArquivoLog, FileMode.Create, FileAccess.ReadWrite))
                    {
                        using (StreamWriter streamWriter = new(stream))
                        {
                            if (mensagem.ToLower() != "init")
                                streamWriter.WriteLine(mensagem);
                            streamWriter.Flush();
                            streamWriter.Close();
                        }
                    }
                }
                else
                {
                    using (StreamWriter streamWriter = new(caminhoArquivoLog, true))
                    {
                        if (mensagem.ToLower() != "init")
                            streamWriter.WriteLine(mensagem);

                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
            }
        }
    }

    #endregion
}
