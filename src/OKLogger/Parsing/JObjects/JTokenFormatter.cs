using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OKLogger.Parsing.JObjects
{
    /// <summary>
    /// Entity formatter for JTokens
    /// </summary>
    public class JTokenFormatter : IEntityFormatter
    {
        private IJTokenFormatterFactory Formatters { get; set; }
        private string FieldContentDelimiter { get; set; }

        private int MaxDepth { get; set; }

        public JTokenFormatter(IJTokenFormatterFactory formatters, string fieldContentDelimiter, int maxDepth)
        {
            Formatters = formatters;
            FieldContentDelimiter = fieldContentDelimiter;
            MaxDepth = maxDepth;
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            var val = item as JToken;
            if(val == null)
            {
                return new Dictionary<string, string>();
            }

            var formatter = Formatters.GetFormatter(val.Type);
            if(formatter == null)
            {
                return new Dictionary<string, string>();
            }

            return formatter.Format(val, depth);
            
        }

        private Type[] HandledTypes = new Type[] { typeof(JObject), typeof(JArray), typeof(JContainer) };

        public bool Handles(Type t)
        {
            return HandledTypes.Contains(t);
        }



    }
}
