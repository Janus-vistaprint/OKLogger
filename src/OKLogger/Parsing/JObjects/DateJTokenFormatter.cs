using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class DateJTokenFormatter : IJTokenFormatter
    {
        private string DatePattern { get; set; }

        private IValueEscaper Scrub { get; set; }

        public DateJTokenFormatter(IValueEscaper scrub, string datePattern = "u")
        {
            DatePattern = datePattern;
            Scrub = scrub;
        }

        public Dictionary<string, string> Format(JToken item, int depth)
        {
            var val = item as JValue;
            if (val == null)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            DateTime? valAsDatetime = (DateTime)val;

            if(!valAsDatetime.HasValue)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            return new Dictionary<string, string>()
            {
                { string.Empty, Scrub.Escape(valAsDatetime.Value.ToString(DatePattern)) }
            };

        }

        public bool Handles(JTokenType t)
        {
            return t == JTokenType.Date;
        }
    }
}
