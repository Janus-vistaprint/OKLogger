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

        public static ILogger GetLogger(string name, string environment)
        {
            var respositoryName = LogManager.GetAllRepositories().First().Name;

            var log = LogManager.GetLogger(respositoryName, name);
            return new Logger(log, environment);
        }

        public static ILogger GetLogger(string name, string environment, object context)
        {
            var respositoryName = LogManager.GetAllRepositories().First().Name;

            var log = LogManager.GetLogger(respositoryName, name);
            return new Logger(log, environment, context);
        }

        public static ILogger GetLogger<T>(string environment, object context)
        {
            var log = LogManager.GetLogger(typeof(T));
            return new Logger(log, environment, context);
        }

        public static ILogger GetLogger<T>(string environment)
        {
            var log = LogManager.GetLogger(typeof(T));
            return new Logger(log, environment);
        }

        public static void Configure(FileInfo config)
        {
            var logRepository =  LogManager.CreateRepository(DefaultRepository);

            XmlConfigurator.Configure(logRepository, config);

        }
    }
}
