using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using OKLogger.Parsing;

namespace Gallery.Logs
{
    public class PropertyParser
    {
        private IFormatterFactory Formatters { get; set; }
        private string FieldContentDelimiter { get; set; }

        public PropertyParser(IFormatterFactory formatters, string fieldContentDelimiter)
        {
            Formatters = formatters;
            FieldContentDelimiter = fieldContentDelimiter;

        }

        /// <summary>
        /// Turns an object into a set of key value pairs
        /// </summary>
        /// <param name="prefix">Prefix to append to key value values</param>
        /// <param name="o">Object to parse</param>
        /// <returns>Key value pairs</returns>
        public IDictionary<string,string> Parse(object o)
        {
            var results = new Dictionary<string, string>();

            if (o == null) return results;
            var formatter = Formatters.GetParser(o.GetType());
            return formatter.Format(o,0);
        }



        /// <summary>
        /// Turns multiple objects into a set of key value pairs, merging them 
        /// </summary>
        /// <param name="prefix">Prefix to append to key value values</param>
        /// <param name="data">Objects to parse</param>
        /// <returns>Key value pairs</returns>
        public IDictionary<string, string> Parse(params object[] data)
        {
            if (data == null || data.Length == 0) return new Dictionary<string,string>();

            IEnumerable<KeyValuePair<string,string>> values = new List<KeyValuePair<string, string>>();

            foreach (var o in data)
            {
                var dict = Parse(o);
                values = values.Concat(dict);
            }

            var results = new Dictionary<string, string>();

            var keys = values.Select(x => x.Key).Distinct().OrderBy(x=> x);

            foreach(var key in keys)
            {
                results[key] = string.Join(FieldContentDelimiter, values.Where(x => x.Key == key).Select(y => y.Value));
            }

            return results;
        }
    }
}
