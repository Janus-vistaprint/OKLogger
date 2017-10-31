using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class CharEscaper : IValueEscaper
    {
        private char _charToEscape = '"';
        private readonly char _charToReplace;

        public CharEscaper(char charToEscape, char charToReplace = '_')
        {
            _charToEscape = charToEscape;
            _charToReplace = charToReplace;
        }

        public string Escape(string value)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            return value.Replace(_charToEscape, _charToReplace);
        }
    }
}
