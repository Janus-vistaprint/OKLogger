using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public interface IValueEscaper
    {
        string Escape(string value);
    }
}
