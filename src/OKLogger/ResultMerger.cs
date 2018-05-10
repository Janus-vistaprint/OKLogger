using System;
using System.Collections.Generic;
using System.Text;

namespace OKLogger
{
    public class ResultMerger
    {

        public void  Merge(Dictionary<string, string> root, string rootName, Dictionary<string, string> child)
        {
            foreach (var kv in child)
            {
                var suffix = string.IsNullOrWhiteSpace(kv.Key) ? string.Empty : "_" + kv.Key;
                string key = rootName + suffix;
                string value;
                if (key.Length > 4000)
                {
                    key = kv.Key.Substring(0, 4000); //short circuit strings larger than 8kb
                }
                if (!string.IsNullOrEmpty(kv.Value) && kv.Value.Length > 4000)
                {
                    value = kv.Value.Substring(0, 4000);
                }
                else
                {
                    value = kv.Value;
                }
                root[key] = value;
            }

        }
    }
}
