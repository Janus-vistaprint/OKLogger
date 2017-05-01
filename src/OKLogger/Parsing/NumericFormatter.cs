using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class NumericFormatter : IEntityFormatter
    {
        private Type[] HandledTypes = new Type[] {
                typeof(short), typeof(Int16),
                typeof(ushort), typeof(UInt16),
                typeof(int), typeof(Int32),
                typeof(uint), typeof(UInt32),
                typeof(float), typeof(Single),
                typeof(long), typeof(Int64),
                typeof(ulong), typeof(UInt64),
                typeof(double), typeof(Double) ,
                typeof(decimal),typeof(Decimal),

                typeof(short?), typeof(Int16?),
                typeof(ushort?), typeof(UInt16?),
                typeof(int?), typeof(Int32?),
                typeof(uint?), typeof(UInt32?),
                typeof(float?), typeof(Single?),
                typeof(long?), typeof(Int64?),
                typeof(ulong?), typeof(UInt64?),
                typeof(double?), typeof(Double?) ,
                typeof(decimal?),typeof(Decimal?)
            };

        public bool Handles(Type t)
        {

            return HandledTypes.Contains(t);
        }

        public Dictionary<string, string> Format(object item, int depth)
        {
            if (item == null) return new Dictionary<string, string>() { { string.Empty, string.Empty } };

            
            return new Dictionary<string, string>()
            {
                { string.Empty, item.ToString() }
            };
        }
    }
}
