using System;
using System.Collections.Generic;
using System.Text;
using OKLogger;
using Xunit;
using System.Linq;
using System.Diagnostics;

namespace OKLogger.PerformanceTests
{
    public class LogMe
    {
        [Fact]
        public void Log()
        {
            var log = new Moq.Mock<log4net.ILog>();
            log.SetupGet(a => a.IsDebugEnabled).Returns(true);
            var logger = new Logger(log.Object, "dev");
            int size = 10000;
            //int size = 10;
            var random = new Random();
            var objs = new List<Object>(size);
            for (int i = 0; i < size; i++)
            {
                objs.Add(new { data = Extensions.StupidLargeStringMe(14000, random), time = 5, arr = Enumerable.Range(1, random.Next(10, 1000)).Select(a => random.StupidLargeStringMe(random.Next(1, 1000))) });
            }
            var timer = Stopwatch.StartNew();
            foreach(var obj in objs)
            {
                logger.PropParser.Parse(obj);
            }
            timer.Stop();
            Console.WriteLine($"prop parser {timer.ElapsedMilliseconds}ms");
            
        }
    }
}
