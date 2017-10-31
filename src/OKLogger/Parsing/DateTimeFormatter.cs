using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class DateTimeFormatter : IEntityFormatter
    {
       
        private HashSet<Type> HandledTypes = new HashSet <Type> {  typeof(DateTime), typeof(DateTime?) };

        private string DatePattern { get; set; }

        private IValueEscaper Scrub { get; set; }

        public DateTimeFormatter(IValueEscaper scrub,string datePattern = "u")
        {
            DatePattern = datePattern;
            Scrub = scrub;
        }
         
        public Dictionary<string, string> Format(object item, int depth)
        {

            if (item == null)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            DateTime val;
            if (!DateTime.TryParse((item ?? "").ToString(), out val))
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };

            }


            return new Dictionary<string, string>()
            {
                { string.Empty, Scrub.Escape(val.ToString(DatePattern)) }
            };
        }

        public bool Handles(Type t)
        {
            return HandledTypes.Contains(t);

        }
    }
}
