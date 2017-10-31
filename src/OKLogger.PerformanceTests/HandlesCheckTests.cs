using OKLogger.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace OKLogger.PerformanceTests
{
    public class HandlesCheckTests
    {
        [Fact]
        public void NumberFormatter()
        {
           var handler = new NumericFormatter();
           var st = Stopwatch.StartNew();
           for (int i = 0; i < 100000; i++)
           {
               handler.Handles(typeof(int));
           }
           st.Stop();
           Console.WriteLine($"Numeric handles check {st.ElapsedMilliseconds}ms");
           Assert.InRange(st.ElapsedMilliseconds, 0, 30);
        }
    }
}
