using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class GuidFormatter : IEntityFormatter
    {
        private Type[] HandledTypes = new Type[] { typeof(Guid), typeof(Guid?) };

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>() { { string.Empty, string.Empty } };

            return new Dictionary<string, string>()
            {
                { string.Empty, item.ToString() }
            };
        }

        public bool Handles(Type t)
        {
            return HandledTypes.Contains(t);
        }
    }

}