using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger
{
    public interface IEntityFormatter
    {
        bool Handles(Type t);
        Dictionary<string, string> Format(object item, int depth);
    }
}
