using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class JObjectFormatter : IJTokenFormatter
    {
        private IJTokenFormatterFactory Formatters { get; set; }
        private string FieldContentDelimiter { get; set; }
        private int MaxDepth { get; set; }

        private ResultMerger ResultMerger = new ResultMerger();

        public JObjectFormatter(IJTokenFormatterFactory formatters, string fieldContentDelimiter, int maxDepth)
        {
            Formatters = formatters;
            FieldContentDelimiter = fieldContentDelimiter;
            MaxDepth = maxDepth;
        }


        public Dictionary<string, string> Format(JToken item, int depth)
        {
            if (depth > MaxDepth || item == null) return new Dictionary<string, string>();

            var obj = item as JObject;

            if(obj == null)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            var result = new Dictionary<string, string>();

            foreach(var prop in obj.Properties())
            {

                try
                {
                    var val = prop.Value;
                    if (val == null) continue;

                    var formatter = Formatters.GetFormatter(val.Type);
                    if (formatter == null) continue;

                    var propValue = formatter.Format(val, depth + 1);

                    ResultMerger.Merge(result, prop.Name, propValue);

                }
                catch  { } // swallow exception
            }

            return result;
        }

        public bool Handles(JTokenType t)
        {
            return t == JTokenType.Object;
        }
    }
}
