using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class DictionaryFormatter : IEntityFormatter
    {
        private string Delimiter { get; set; }
        private IValueEscaper Scrub { get; set; }
        private TypeInfo typeInfo = typeof(Dictionary<,>).GetTypeInfo();
        public DictionaryFormatter(string delimiter,IValueEscaper scrub)
        {
            Delimiter = delimiter;
            Scrub = scrub;

        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>();

            var formattedItems = new Dictionary<string,string>();

            var dict = item as IDictionary;

            foreach(var k in dict.Keys)
            {
                var key = k.ToString();
                var val = dict[k].ToString();
                formattedItems[key] = Scrub.Escape(val);
            }

            return formattedItems;
        }

        public bool Handles(Type t)
        {

            if (!t.GetTypeInfo().IsGenericType)
                return false;

            if (!t.GetGenericTypeDefinition().GetTypeInfo().IsAssignableFrom(typeInfo))
                return false;

            var keyType = t.GenericTypeArguments[0];
            return keyType == typeof(string) || keyType == typeof(String);

        }
    }
}
