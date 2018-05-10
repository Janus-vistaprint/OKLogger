using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger.Parsing.JObjects
{
    public class DefaultJTokenFormatters : JTokenFormatterFactory
    {
        public const int MaxDepth = 3;
        public IValueEscaper Scrubber = new CharEscaper('"');

        public DefaultJTokenFormatters()
         : base()
        {
            Add(1, new StringJTokenFormatter(Scrubber));
            Add(2, new FloatJTokenFormatter());
            Add(3, new DateJTokenFormatter(Scrubber));
            Add(4, new GuidJTokenFormatter());
            Add(5, new BooleanJTokenFormatter());
            Add(6, new IntegerJTokenFormatter());



            Add(1001, new ArrayJTokenFormatter(this, ",", MaxDepth));
            Add(1002, new JObjectFormatter(this, ",", MaxDepth));
        }
    }
}
