using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class StringFormatter : IEntityFormatter
    {
        private Type[] HandledTypes = new Type[] { typeof(string), typeof(String) };

        private IValueEscaper Scrub { get; set; }

        public StringFormatter(IValueEscaper scrub)
        {
            Scrub = scrub;
        }

        public bool Handles(Type t)
        {
            return HandledTypes.Contains(t);
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
