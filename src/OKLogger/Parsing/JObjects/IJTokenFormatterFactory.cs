using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger.Parsing.JObjects
{
    public interface IJTokenFormatterFactory
    {
        IJTokenFormatter GetFormatter(JTokenType t);

        void AddCustomFormatter(IJTokenFormatter formatter);
    }
}
