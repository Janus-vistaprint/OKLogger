using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace OKLogger.Parsing.JObjects
{
    public class JTokenFormatterFactory : IJTokenFormatterFactory
    {
        protected SortedList<int, IJTokenFormatter> Formatters { get; set; }

        protected int CurrentCustomFormatterPosition = 10000;

        public JTokenFormatterFactory()
        {
            Formatters = new SortedList<int, IJTokenFormatter>();

        }

        public void Add(int priority, IJTokenFormatter formatter)
        {
            Formatters[priority] = formatter;
        }

        public void AddCustomFormatter(IJTokenFormatter formatter)
        {
            Add(CurrentCustomFormatterPosition++, formatter);
        }


        public IJTokenFormatter GetFormatter(JTokenType t)
        {
            foreach (var formatter in Formatters)
            {
                if (formatter.Value.Handles(t))
                    return formatter.Value;
            }
            return null;
        }
    }
}
