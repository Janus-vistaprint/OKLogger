using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class ValueEscaper : IValueEscaper
    {
        private string[] EscapedChars { get; set; }

        public ValueEscaper(string[] escapedChars)
        {
            EscapedChars = escapedChars;
        }

        public string Escape(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            foreach(var c in EscapedChars)
            {
                value = value.Replace(c, string.Empty);
            }

            return value;
        }
    }
}
