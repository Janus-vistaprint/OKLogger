﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKLogger.Parsing
{
    public class FormatterFactory : IFormatterFactory
    {
        private int NextCustomFormatterPosition = 1000;

        protected SortedList<int, IEntityFormatter> Formatters { get; set; }

        public FormatterFactory()
        {
            Formatters = new SortedList<int, IEntityFormatter>();

        }

        public void Add(int priority, IEntityFormatter formatter)
        {
            Formatters[priority] = formatter;
        }

        public void AddCustomFormatter(IEntityFormatter formatter)
        {
            Add(NextCustomFormatterPosition++, formatter);
        }

        public IEntityFormatter GetParser(Type t)
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
