using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Webstore.Logger
{
    public static class Log4NetLoggerFactoryExtensions
    {
        private static string CheckFilePath(string filePath)
        {
            if (filePath is not { Length: > 0 }) throw new ArgumentException("Указан некорректный путь к файлу", nameof(filePath));

            if (Path.IsPathRooted(filePath)) return filePath;

            var assembly = Assembly.GetEntryAssembly();
            var dir = Path.GetDirectoryName(assembly!.Location);
            return Path.Combine(dir, filePath);


        }
        public static ILoggerFactory AddLog4Net(this ILoggerFactory factory, string ConfigurationFile ="log4net.config")
        {
            factory.AddProvider(new Log4NetLoggerProvider(ConfigurationFile));

            return factory;
        }
    }
}
