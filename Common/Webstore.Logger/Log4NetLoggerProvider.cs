using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace Webstore.Logger
{
    public class Log4NetLoggerProvider : ILoggerProvider
    {
        private readonly string _configurationFile;
        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new();
        public Log4NetLoggerProvider(string ConfigurationFile) => _configurationFile = ConfigurationFile;

        public ILogger CreateLogger(string category)
        =>
            _loggers.GetOrAdd(category, c =>
            {
                var xml = new XmlDocument();
                xml.Load(_configurationFile);
                return new Log4NetLogger(category, xml["log4net"]);
            });
        

        public void Dispose() => _loggers.Clear();
    }
}
