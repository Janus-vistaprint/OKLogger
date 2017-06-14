using System;
using System.Collections.Generic;
using System.Text;
using OKLogger.Parsing;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Threading.Tasks;

namespace OKLogger
{
    public class Logger : ILogger
    {
        public const string CustomPropertyPrefix = "custom_";
        public const string CustomContextPrefix = "context_";

        public const string EnvironmentProperty = "vpenv";
        public const string ProcessIdProperty = "pid";
        public const string FieldContentDelimiter = ",";

        protected ILog Log { get; set; }
        protected string Environment { get; set; }

        protected IFormatterFactory Formatters { get; set; }

        private PropertyParser PropParser { get; set; }


        private IDictionary<string, string> Context { get; set; }

        /// <summary>
        /// functions called before each loggign event to add extra context
        /// </summary>
        private List<Func<object>> ContextCallbacks { get; set; }

        /// <summary>
        /// Builds a gallery logger
        /// </summary>
        /// <param name="log">log4net base log</param>
        /// <param name="environment">DEV / PROD, TEST. Outputs as env property</param>
        public Logger(ILog log, string environment)
            : this(log, environment, null)
        {

        }

        public Logger(ILog log, string environment, object context)
        {
            Log = log;
            Environment = environment;
            Formatters = new DefaultFormatters();
            PropParser = new PropertyParser(Formatters, FieldContentDelimiter);
            if (context != null)
            {
                Context = PropParser.Parse(context);
            }
            ContextCallbacks = new List<Func<object>>();
        }

        private void LogEvent(Level level, object[] properties, string message, Exception ex)
        {
            try
            {
                if (IsLogLevelEnabled(level))
                {
                    Task.Run(() =>
                    {
                        var props = PropParser.Parse(properties);
                        var logEvent = new LoggingEvent(typeof(Logger), Log.Logger.Repository, Log.Logger.Name, level, message, ex); ;

                        logEvent.Properties[EnvironmentProperty] = Environment;
                        //logEvent.Properties[ProcessIdProperty] = Process.GetCurrentProcess().Id;

                        foreach (var contextCallback in ContextCallbacks)
                        {
                            var contextResult = contextCallback();
                            var contextProperties = PropParser.Parse(contextResult);

                            foreach (var keyPair in contextProperties)
                            {
                                var keyName = Logger.CustomPropertyPrefix + (string.IsNullOrWhiteSpace(keyPair.Key) ? "data" : keyPair.Key);
                                logEvent.Properties[keyName] = keyPair.Value;
                            }

                        }

                        if (properties != null)
                        {
                            foreach (var keyPair in props)
                            {
                                var keyName = Logger.CustomPropertyPrefix + (string.IsNullOrWhiteSpace(keyPair.Key) ? "data" : keyPair.Key);
                                logEvent.Properties[keyName] = keyPair.Value;
                            }
                        }

                        if (Context != null)
                        {
                            foreach (var keyPair in Context)
                            {
                                var keyName = Logger.CustomContextPrefix + (string.IsNullOrWhiteSpace(keyPair.Key) ? "data" : keyPair.Key);
                                logEvent.Properties[keyName] = keyPair.Value;
                            }
                        }
                        Log.Logger.Log(logEvent);
                    }).ConfigureAwait(false);

                }
            }
            catch (Exception exc)
            {
                Log.Error("Unable to log message. This really should not happen", exc);
            }
        }

        private bool IsLogLevelEnabled(Level level)
        {
            if (level == null)
            {
                Log.Error("Log level cannot be null");
                return true;
            }
            

            
            if (level.Equals(Level.Debug))
            {
                return Log.IsDebugEnabled;
            }
            if (level.Equals(Level.Info))
            {
                return Log.IsInfoEnabled;
            }
            if (level.Equals(Level.Warn))
            {
                return Log.IsWarnEnabled;
            }
            if (level.Equals(Level.Error))
            {
                return Log.IsErrorEnabled;
            }
            if (level.Equals(Level.Fatal))
            {
                return Log.IsFatalEnabled;
            }

            throw new Exception("Unknown log level");

        }

        #region Convenience functions

        public void Debug(string message)
        {
            LogEvent(Level.Debug, new object[] { }, message, null);
        }

        public void Debug(string message, Exception exception)
        {
            LogEvent(Level.Debug, new object[] { }, message, exception);

        }

        public void Debug(string message, params object[] args)
        {
            LogEvent(Level.Debug, args, message, null);

        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            LogEvent(Level.Debug, args, message, exception);

        }

        public void Error(string message)
        {
            LogEvent(Level.Error, new object[] { }, message, null);
        }

        public void Error(string message, Exception exception)
        {
            LogEvent(Level.Error, new object[] { }, message, exception);

        }

        public void Error(string message, params object[] args)
        {
            LogEvent(Level.Error, args, message, null);

        }

        public void Error(string message, Exception exception, params object[] args)
        {
            LogEvent(Level.Error, args, message, exception);

        }

        public void Fatal(string message)
        {
            LogEvent(Level.Fatal, new object[] { }, message, null);
        }

        public void Fatal(string message, Exception exception)
        {
            LogEvent(Level.Fatal, new object[] { }, message, exception);

        }

        public void Fatal(string message, params object[] args)
        {
            LogEvent(Level.Fatal, args, message, null);

        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            LogEvent(Level.Fatal, args, message, exception);

        }

        public void Info(string message)
        {
            LogEvent(Level.Info, new object[] { }, message, null);
        }

        public void Info(string message, Exception exception)
        {
            LogEvent(Level.Info, new object[] { }, message, exception);

        }

        public void Info(string message, params object[] args)
        {
            LogEvent(Level.Info, args, message, null);

        }

        public void Info(string message, Exception exception, params object[] args)
        {
            LogEvent(Level.Info, args, message, exception);

        }

        public void Warn(string message)
        {
            LogEvent(Level.Warn, new object[] { }, message, null);
        }

        public void Warn(string message, Exception exception)
        {
            LogEvent(Level.Warn, new object[] { }, message, exception);

        }

        public void Warn(string message, params object[] args)
        {
            LogEvent(Level.Warn, args, message, null);

        }

        public void Warn(string message, Exception exception, params object[] args)
        {
            LogEvent(Level.Warn, args, message, exception);

        }



        #endregion

        public ILogger WithContext(Func<object> callback)
        {
            ContextCallbacks.Add(callback);
            return this;
        }

        public ILogger GetChildLogger(string suffix)
        {
            var childName = $"{Log.Logger.Name}.{suffix}";
            return OKLogManager.GetLogger(childName, Environment, Context);
        }

    }
}
