using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var props = ReflectionFactory.GetProperties(item);
            foreach (var prop in props)
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
                        string key = prop.Name + suffix;
                        string value;
                        if (key.Length > 4000)
                        {
                            key = kv.Key.Substring(0, 4000); //short circuit strings larger than 8kb
                        }
                        if(!string.IsNullOrEmpty(kv.Value) && kv.Value.Length > 4000)
                        {
                            value = kv.Value.Substring(0, 4000);
                        }
                        else
                        {
                            value = kv.Value;
                        }
                        result[key] = value;
                    }
                }
                catch (Exception e) { } // swallow exception
            }

            return result;
        }
    }
}
