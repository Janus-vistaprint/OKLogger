using System;
using System.Collections.Generic;
using System.Text;
using log4net.Util;
using log4net.Core;
using System.IO;

namespace OKLogger.Layouts
{
    public class StripNewLinesConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {

            var loggingEvent = state as LoggingEvent;

            if (loggingEvent.ExceptionObject == null) return;

            var scrubbedException = loggingEvent.GetExceptionString().Replace("\n", "\t").Replace("\r", "");
            writer.Write(scrubbedException);
        }

    }
}
