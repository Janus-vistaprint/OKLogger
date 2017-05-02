using System;
using log4net;
using System.Reflection;
using log4net.Config;

namespace OKLogger.TestHanress
{
    class Program
    {
        static void Main(string[] args)
        {
            OKLogManager.Configure(new System.IO.FileInfo("logging.config.xml"));

            var log = OKLogManager.GetLogger("Test.Awesome", "PROD");

            log.Info("This is just a test", new
            {
                alpha = "1",
                beta = new {
                    howdy = "nifty",
                    active = true
                }
            });


        }
    }
}