using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class ObjectFormatter : IEntityFormatter
    {
        private IFormatterFactory Formatters { get; set;}
        private string FieldContentDelimiter { get; set; }
        private int MaxDepth { get; set; }

        public ObjectFormatter(IFormatterFactory formatters, string fieldContentDelimiter, int maxDepth)
        {
            Formatters = formatters;
            FieldContentDelimiter = fieldContentDelimiter;
            MaxDepth = maxDepth;
        }

        public bool Handles(Type t)
        {
            return typeof(object).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()) && !t.IsArray;
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            
            if (depth > MaxDepth || item == null) return new Dictionary<string, string>();
            var result = new Dictionary<string, string>();

            var props = item.GetType().GetRuntimeProperties();
            foreach(var prop in props)
            {
                // see if we have a parser for this property
                var parser = Formatters.GetParser(prop.PropertyType);
                if (parser == null) continue;

                try
                {
                    var val = prop.GetValue(item);
                    if (val == null) continue;
                    var propKeyValue = parser.Format(val, depth +1);
                    foreach (var kv in propKeyValue)
                    {
                        var suffix = string.IsNullOrWhiteSpace(kv.Key) ? string.Empty : "_" + kv.Key;
                        result[prop.Name + suffix] = kv.Value;
                    }
                }
                catch { } // swallow exception
            }

            return result;
        }
    }
}
