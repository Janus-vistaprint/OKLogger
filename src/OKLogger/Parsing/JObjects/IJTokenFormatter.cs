using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger.Parsing.JObjects
{
    public interface IJTokenFormatter
    {
        bool Handles(JTokenType t);
        Dictionary<string, string> Format(JToken item, int depth);
    }
}
