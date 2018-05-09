using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class ArrayJTokenFormatter : IJTokenFormatter
    {
        private IJTokenFormatterFactory Formatters { get; set; }
        private string FieldContentDelimiter { get; set; }
        private int MaxDepth { get; set; }

        private ResultMerger ResultMerger = new ResultMerger();

        public ArrayJTokenFormatter(IJTokenFormatterFactory formatters, string fieldContentDelimiter, int maxDepth)
        {
            Formatters = formatters;
            FieldContentDelimiter = fieldContentDelimiter;
            MaxDepth = maxDepth;
        }

        public bool Handles(JTokenType t)
        {
            return t == JTokenType.Array;
        }

        public Dictionary<string, string> Format(JToken item, int depth)
        {
            if (depth > MaxDepth || item == null) return new Dictionary<string, string>();

            var arr = item as JArray;

            if (arr == null || arr.Count < 1)
            {
                return new Dictionary<string, string>() { { string.Empty, string.Empty } };
            }

            var result = new Dictionary<string, string>();


            var elementValues = new List<Dictionary<string, string>>();

            for (int i = 0; i < arr.Count; i++)
            {

                JToken valAtIndex = arr[i];

                try
                {
                    var val = arr[i];
                    if (val == null) continue;

                    var formatter = Formatters.GetFormatter(val.Type);
                    if (formatter == null) continue;

                    var propValue = formatter.Format(val, depth + 1);
                    elementValues.Add(propValue);


                }
                catch  { } // swallow exception
            }

            // if all elements are single values we just concatenate them in a delimited list
            // otherwise, make the separate keys
            var isValueArray = elementValues.All(val => val.Count == 1);
            if(isValueArray)
            {
                result[string.Empty] = string.Join(FieldContentDelimiter, elementValues.Select(x => x[string.Empty]));
                return result;
            }
            else
            {
                for(var i = 0; i < elementValues.Count; i++)
                {
                    ResultMerger.Merge(result, $"_{i}", elementValues[i]);
                }
            }


            return result;
        }


    }
}
