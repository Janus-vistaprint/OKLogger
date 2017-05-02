using System;
using System.Collections.Generic;
using System.Text;
using log4net.Util;
using log4net.Core;
using System.IO;
using System.Linq;

namespace OKLogger.Layouts
{
    public class KeyValueConverter : PatternConverter
    {
        public string Delimiter { get; set; }

        public KeyValueConverter()
        {
            Delimiter = ",";

        }


        protected override void Convert(TextWriter writer, object state)
        {
            var logEntry = state as LoggingEvent;

            var instanceProps = ExtractProps(logEntry.Properties, Logger.CustomPropertyPrefix);
            var contextProps = ExtractProps(logEntry.Properties, Logger.CustomContextPrefix);

            writer.Write(string.Join(Delimiter, instanceProps.Union(contextProps)));
        }

        protected List<string> ExtractProps(PropertiesDictionary props, string prefix)
        {
            // extract optional custom properties which have a special prefix
            if (props == null) new List<string>();
            var customProps = props.GetKeys()
                .Where(x => x.StartsWith(prefix))
                .Select(x => x.Substring(prefix.Length, x.Length - prefix.Length))
                .OrderBy(x => x)
                .ToList();

            if (customProps.Count() == 0) new List<string>();

            var keyValues = new List<string>();

            foreach (var propName in customProps)
            {
                var val = (string)props[prefix + propName];
                val = val.Replace('"', ' '); // remove quotes
                keyValues.Add($"{propName}=\"{val}\"");
            }

            return keyValues;
        }
    }
}
