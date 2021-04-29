using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using Microsoft.Extensions.Logging;

namespace Webstore.Logger
{

    public class Log4NetLogger : ILogger
    {
        private readonly ILog _Log;
        public Log4NetLogger(string Category, XmlElement Configuratio)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            _Log = LogManager.GetLogger(logger_repository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuratio);
        }

        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(LogLevel logLevel) => logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _Log.IsDebugEnabled,
            LogLevel.Debug => _Log.IsDebugEnabled,
            LogLevel.Information => _Log.IsInfoEnabled,
            LogLevel.Warning => _Log.IsWarnEnabled,
            LogLevel.Error => _Log.IsErrorEnabled,
            LogLevel.Critical => _Log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)


        };

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
                throw new ArgumentOutOfRangeException(nameof(formatter));

            var log_message = formatter(state, exception);

            if (string.IsNullOrEmpty(log_message) && exception is null) return;

            switch(logLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(log_message);
                    break;


                case LogLevel.Information:
                    _Log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _Log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _Log.Error(log_message, exception);
                    break;

                case LogLevel.Critical:
                    _Log.Fatal(log_message, exception);
                    break;
            }
        }
    }
}
