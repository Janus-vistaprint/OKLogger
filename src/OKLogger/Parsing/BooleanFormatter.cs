using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace OKLogger.Parsing
{
    public class BooleanFormatter : IEntityFormatter
    {
        private HashSet<Type> HandledTypes = new HashSet<Type> {
                typeof(bool), typeof(Boolean)
            };

        public bool Handles(Type t)
        {
            return HandledTypes.Contains(t);
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>() { { string.Empty, string.Empty } };

            bool itemAsBoolean = (bool)item;
            if(itemAsBoolean)
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
