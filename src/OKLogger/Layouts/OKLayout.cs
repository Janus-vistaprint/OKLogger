using System;
using System.Collections.Generic;
using System.Text;
using log4net.Util;
using log4net.Layout;

namespace OKLogger.Layouts
{
    public class OKLayout : PatternLayout
    {
        public string Delimiter;

        public OKLayout()
        {

        }

        protected override PatternParser CreatePatternParser(string pattern)
        {
            var parser = base.CreatePatternParser(pattern);
            parser.PatternConverters["props"] = new ConverterInfo
            {
                Name = "props",
                Type = typeof(KeyValueConverter)

            };
            parser.PatternConverters["se"] = new ConverterInfo
            {
                Name = "se",
                Type = typeof(StripNewLinesConverter)

            };
            return parser;
        }


    }
}
