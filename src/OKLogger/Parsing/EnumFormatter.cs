using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class EnumFormatter : IEntityFormatter
    {
        public bool Handles(Type t)
        {

            return t.GetTypeInfo().IsEnum;
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>() { { string.Empty, string.Empty } };

            var t = item.GetType();
            var enumName = Enum.GetName(t, item);

            return new Dictionary<string, string>()
            {
                { string.Empty, enumName }
            };
        }
    }
}
