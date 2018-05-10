using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger.Tests
{
    public class SampleToken<V>
    {
        public JToken Token { get; set; }
        public V Value { get; set; }

        public SampleToken()
        {

        }

        public SampleToken(JToken t, V v)
        {
            Token = t;
            Value = v;
        }

        public SampleToken(V v)
        {
            Value = v;
            Token = new JValue(v);
        }
    }



    public class SampleTokens
    {
        public static SampleToken<string> String = new SampleToken<string>("This is a test string");

        public static SampleToken<int> Int = new SampleToken<int>(42);
        public static SampleToken<double> Double = new SampleToken<double>(3.14);

        public static SampleToken<Guid> Guid = new SampleToken<Guid>(new Guid("203a9f71-7d62-44aa-8147-caf8680f17ec"));

        public static SampleToken<bool> TrueBool = new SampleToken<bool>(true);
        public static SampleToken<bool> FalseBool = new SampleToken<bool>(false);

        public static SampleToken<object> TestObject = new SampleToken<object>()
        {
            Value = new SampleObject
            {
                Alpha = "a",
                Beta = 2

            },
            Token = JObject.FromObject(
                new SampleObject
                {
                    Alpha = "a",
                    Beta = 2

                }
            )


        };


    }

    public class SampleObject
    {
        public string Alpha { get; set; }
        public int Beta { get; set; }

    }
}
