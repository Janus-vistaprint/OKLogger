using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OKLogger.Parsing;
using Xunit;

namespace OKLogger.PerformanceTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestCharacterReplacer()
        {
           var random = new Random();
           var escaper = new ValueEscaper((new char[] { '"' }));
           var strings = new List<string>(10000);
           for (int i = 0; i < 10000; i++)
           {
               strings.Add(Extensions.StupidLargeStringMe(14000, random));
                
           }
           var timer = Stopwatch.StartNew();
           foreach(var str in strings)
           {
               escaper.Escape(str);
           }
           timer.Stop();
           Assert.InRange(timer.ElapsedMilliseconds, 0, 200);
           Console.WriteLine($"Character replace quote {timer.ElapsedMilliseconds}ms");
        }
    }
    public static class Extensions
    {
        private const string chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&\"";
        public static char GetLetter(this Random rand)
        {
            int num = rand.Next(0, chars.Length - 1);
            return chars[num];
        }
        public static string StupidLargeStringMe(this Random random, int size) => StupidLargeStringMe(size, random); 
        public static string StupidLargeStringMe(int size, Random random) => new String(Enumerable.Range(1, size).Select(a => random.GetLetter()).ToArray());
    }
}
