using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class StringFormatter : IEntityFormatter
    {
        private IValueEscaper Scrub { get; set; }

        public StringFormatter(IValueEscaper scrub)
        {
            Scrub = scrub;
        }

        public bool Handles(Type t)
        {
            return t == typeof(String);
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            return new Dictionary<string, string>()
            {
                { string.Empty, Scrub.Escape(item.ToString()) }
            };
        }
    }
}
