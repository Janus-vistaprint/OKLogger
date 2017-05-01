using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public interface IFormatterFactory
    {
        IEntityFormatter GetParser(Type t); 
    }
}
