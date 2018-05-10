using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OKLogger.Parsing.JObjects;

namespace OKLogger.Parsing
{
    public class DefaultFormatters : FormatterFactory
    {
        public const int MaxDepth = 3;
        public IValueEscaper Scrubber = new CharEscaper('"');

        public DefaultFormatters()
            :base()
        {
            Add(1, new StringFormatter(Scrubber));
            Add(2, new NumericFormatter());
            Add(3, new EnumFormatter());
            Add(4, new DateTimeFormatter(Scrubber));
            Add(5, new GuidFormatter());

            Add(9, new ExceptionFormatter());

            Add(100, new ArrayFormatter(",", Scrubber));
            Add(101, new GenericListFormatter(",", Scrubber));
            Add(102, new DictionaryFormatter(",", Scrubber));

            Add(500, new JTokenFormatter(new DefaultJTokenFormatters(), ",", MaxDepth));

            Add(Int16.MaxValue, new ObjectFormatter(this, ",", MaxDepth));

        }
    }
}
