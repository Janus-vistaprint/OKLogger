using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OKLogger.Parsing
{
    class ExceptionFormatter : IEntityFormatter
    {
        TypeInfo exceptionTypeInfo = typeof(Exception).GetTypeInfo();
        public Dictionary<string, string> Format(object item, int depth)
        {
            System.Diagnostics.Trace.TraceInformation("OKLogger:Dropping Exception passed as data. Please pass exceptions with the exception parameter");
            return new Dictionary<string, string>();
        }

        public bool Handles(Type t) => t.GetTypeInfo().IsAssignableFrom(exceptionTypeInfo);
    }
}
