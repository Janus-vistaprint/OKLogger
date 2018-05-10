using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class BooleanJTokenFormatter : IJTokenFormatter
    {
        
        public bool Handles(JTokenType t)
        {
            return t == JTokenType.Boolean;
        }

        public Dictionary<string, string> Format(JToken item, int depth)
        {
            var val = item as JValue;
            if(val == null)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            var valAsBoolean = val.Value<Boolean>();

            if (valAsBoolean)
            {
                return new Dictionary<string, string>()
                {
                    { string.Empty, "true" }
                };
            }
            else
            {
                return new Dictionary<string, string>()
                {
                    { string.Empty, "false" }
                };
            }
        }
    }
}
