using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger
{
    public abstract class LoggingDestination
    {
        public abstract void WriteLog(string message, IContextProvider contextProvider, LogLevel loglevel);
        public abstract bool IsLogLevelEnabled(LogLevel logLevel);

    }

    public interface IContextProvider
    {
        Dictionary<string, string> GetContext(); 

    }

    public enum LogLevel
    {
        Debug = 1,
        Info = 2,
        Warning = 3,
        Error = 4,
        Fatal = 5

    }
}
