using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using System.Linq;
using log4net.Config;
using System.Reflection;
using System.IO;

namespace OKLogger
{
    public class OKLogManager
    {
        public const string DefaultRepository = "OKLogger";

        public static ILogger GetLogger(string name, string environment, IEnumerable<IEntityFormatter> customFormatters= null)
        {
            
            var log = LogManager.GetLogger(DefaultRepository, name);
            return new Logger(log, environment,customFormatters);
        }

        public static ILogger GetLogger(string name, string environment, object context, IEnumerable<IEntityFormatter> customFormatters= null)
        {

            var log = LogManager.GetLogger(DefaultRepository, name);
            return new Logger(log, environment, context,customFormatters);
        }

        public static ILogger GetLogger<T>(string environment, object context, IEnumerable<IEntityFormatter> customFormatters= null)
        {
            var log = LogManager.GetLogger(DefaultRepository, typeof(T).Name);
            return new Logger(log, environment, context,customFormatters);
        }

        public static ILogger GetLogger<T>(string environment, IEnumerable<IEntityFormatter> customFormatters = null)
        {
            var log = LogManager.GetLogger(DefaultRepository, typeof(T).Name);
            return new Logger(log, environment, customFormatters);
        }

        public static void Configure(FileInfo config)
        {
            var logRepository =  LogManager.CreateRepository(DefaultRepository);
            XmlConfigurator.Configure(logRepository, config);

        }
        public static void Configure()
        {
            var logRepository =  LogManager.CreateRepository(DefaultRepository);
            XmlConfigurator.Configure(logRepository);

        }
    }
}
