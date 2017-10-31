using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class DefaultFormatters : FormatterFactory
    {
        public const int MaxDepth = 3;
        public IValueEscaper Scrubber = new ValueEscaper(new char[] { '"' });

        public DefaultFormatters()
            :base()
        {
            Add(1, new StringFormatter(Scrubber));
            Add(2, new NumericFormatter());
            Add(3, new EnumFormatter());
            Add(4, new DateTimeFormatter(Scrubber));
            Add(5, new GuidFormatter());

            
            Add(10, new ArrayFormatter(",", Scrubber));
            Add(15, new GenericListFormatter(",", Scrubber));
            Add(18, new DictionaryFormatter(",", Scrubber));


            Add(20, new ObjectFormatter(this, ",", MaxDepth));

        }
    }
}
