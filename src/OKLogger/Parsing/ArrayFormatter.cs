using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class ArrayFormatter : IEntityFormatter
    {
        private string Delimiter { get; set; }
        private IValueEscaper Scrub { get; set; }

        public ArrayFormatter(string delimiter, IValueEscaper scrub)
        {
            Delimiter = delimiter;
            Scrub = scrub;
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>();

            var formattedItems = new List<string>();
            var arr = (IList)item;
            for (int i = 0; i < arr.Count; i++)
            {
                formattedItems.Add(arr[i].ToString());
            }

            return new Dictionary<string, string>
            {
                { string.Empty, Scrub.Escape(string.Join(Delimiter, formattedItems)) }
            };
        }

        public bool Handles(Type t)
        {
            return t.IsArray && t.GetElementType().GetTypeInfo().IsValueType;
        }
    }
}
