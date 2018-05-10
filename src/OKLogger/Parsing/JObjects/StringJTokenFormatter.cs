using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class StringJTokenFormatter : IJTokenFormatter
    {
        private IValueEscaper Scrub { get; set; }

        public StringJTokenFormatter(IValueEscaper scrub)
        {
            Scrub = scrub;
        }

        public Dictionary<string, string> Format(JToken item, int depth)
        {
            var valueAsString = item.Value<string>();

            return new Dictionary<string, string>()
            {
                { string.Empty, Scrub.Escape(valueAsString.ToString()) }
            };
        }

        public bool Handles(JTokenType t)
        {
            return t == JTokenType.String;
        }
    }
}
