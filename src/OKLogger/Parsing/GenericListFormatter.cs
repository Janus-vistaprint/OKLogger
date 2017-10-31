using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class GenericListFormatter : IEntityFormatter
    {
        private string Delimiter { get; set; }
        private IValueEscaper Scrub { get; set; }
        private TypeInfo ListTypeInfo = typeof(List<>).GetTypeInfo();
        public GenericListFormatter(string delimiter, IValueEscaper scrub)
        {
            Scrub = scrub;
            Delimiter = delimiter;
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
            var typeInfo = t.GetTypeInfo();
            if (!typeInfo.IsGenericType)
                return false;

            return typeInfo.GetGenericTypeDefinition().GetTypeInfo().IsAssignableFrom(ListTypeInfo);

        }
    }
}
