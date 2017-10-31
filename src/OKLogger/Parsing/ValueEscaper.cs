using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class ValueEscaper : IValueEscaper
    {
        private char[] EscapedChars { get; set; }

        public ValueEscaper(char[] escapedChars)
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
                value = value.Replace(c, '\0'); // \0 is an ASCII value of 0 aka no value
            }
            return value;
        }
    }
}
