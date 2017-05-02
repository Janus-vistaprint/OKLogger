using System;
using log4net;
using System.Reflection;
using log4net.Config;
using System.Threading;

namespace OKLogger.TestHanress
{
    class Program
    {
        static void Main(string[] args)
        {
            OKLogManager.Configure(new System.IO.FileInfo("logging.config.xml"));

            // get a basic logger
            var logger_basic = OKLogManager.GetLogger("Gallery.Web", "dev");
            var logger_basic_prog = OKLogManager.GetLogger<Program>("dev");

            // get a logger with context. context properties are added to each logging message
            var logger = OKLogManager.GetLogger("Gallery.Web", "dev", new
            {
                Session_Id = "abc_123",
                Http_Referer = "http://www.google.com"
            });

            logger.Info("Writing integers", new
            {
                AnInt = 4,
                ADouble = 3.3
            });

            var userInfo = new
            {
                User_Name = "bkennedy",
                User_Id = 3
            };

            // log a simple message
            // output: timestamp=2016-08-18 13:36:22,331, host=DEVBKENNEDY,process=[10.30900],severity=DEBUG,env=dev,Http_Referer="http://www.google.com",Session_Id="abc_123",logger=Gallery.Web,log_message=An event occurred
            logger.Debug("An actual event occurred now!");
            logger.Debug("C041CC74-2931-47CE-95BD-32AA2B4FAB30");



            // use a child logger
            var childLogger = logger.GetChildLogger("ChildName");
            childLogger.Info("From child logger");

            // log messages with helpful metadata
            // output: timestamp=2016-08-18 13:36:23,151, host=DEVBKENNEDY,process=[10.30900],severity=DEBUG,env=dev,destination_ip="10.10.1.56",source_ip="192.168.1.1",traffic_type="bidirectional",User_Id="3",User_Name="bkennedy",Http_Referer="http://www.google.com",Session_Id="abc_123",logger=Gallery.Web,log_message=Anomalous traffic detected
            Random r = new Random();
            var ips = new string[] { "192.168.1.1", "192.168.1.5", "192.168.1.56", "10.10.1.56", "10.10.1.75", "10.10.2.23" };
            var types = new string[] { "inbound", "outbound", "bidirectional" };
            for (var i = 0; i < 20; i++)
            {
                logger.Debug("Anomalous traffic detected", new
                {
                    source_ip = ips[r.Next(0, 6)],
                    destination_ip = ips[r.Next(0, 6)],
                    traffic_type = types[r.Next(0, 3)]
                }, userInfo);

            }


            var s = DateTime.Now;

            for (var i = 0; i < 100; i++)
            {
                // log a message with extra properties from multiple objects
                // objects can be merged 
                // Ouput: 2016-08-17 10:36:22,663 [9] DEBUG Alpha="1" Beta="some string" User_Id="3" User_Name="bkennedy" Gallery.Web Sample test message  
                logger.Debug("Sample test message", new
                {
                    Alpha = 1,
                    Beta = "this is a new string"
                }, userInfo);
            }
            var e = DateTime.Now;
            var duration = e.Subtract(s).TotalMilliseconds;

            // log a message with complex, nested state
            // Ouput: timestamp=2016-08-18 13:40:07,109, host=DEVBKENNEDY,process=[10.10168],severity=DEBUG,env=dev,message_type="keep-alive",Receiver_Name="bkennedy",Receiver_User_Id="2",Sender_Name="admin",Sender_User_Id="1",User_Id="3",User_Name="bkennedy",Http_Referer="http://www.google.com",Session_Id="abc_123",logger=Gallery.Web,log_message=Sample test message
            logger.Debug("Sample test message", new
            {
                message_type = "keep-alive",
                Receiver = new
                {
                    Name = "bkennedy",
                    User_Id = 2
                },
                Sender = new
                {
                    Name = "admin",
                    User_Id = 1
                }
            }, userInfo);


            // Log an error with an exception and extra info
            // Ouput: 2016-08-17 10:36:22,671 [9] Fatal Destination_Ip="127.0.0.1" Method="POST" Source_Ip="192.168.1.1" Gallery.Web Sample test message System.DivideByZeroException: Attempted to divide by zero.	   at Gallery.Logs.TestHarness.Program.Main(String[] args) in c:\projects\Gallery.Logs\Gallery.Logs.TestHarness\Program.cs:line 45
            try
            {
                var zero = 0;
                var divideBy = 1 / zero;

            }
            catch (Exception ex)
            {
                logger.Fatal("Sample test message", ex, new
                {
                    Source_Ip = "192.168.1.1",
                    Destination_Ip = "127.0.0.1",
                    Method = "POST"
                });

                var nestedOne = new ArgumentException("Bad arg", ex);
                var nestedTwo = new ArgumentException("Another level of nesting", nestedOne);

                logger.Fatal("Nested exception", nestedTwo, new
                {
                    Source_Ip = "192.168.1.1",
                    Destination_Ip = "127.0.0.1",
                    Method = "POST"
                });
            }

            // add a callback before each logging event to add additional data
            var logger_context = OKLogManager.GetLogger("Gallery.Web", "test")
                .WithContext(() => { return new { a = 1, b = 2 }; })
                .WithContext(() => { return new { c = 1, d = 2 }; });

            logger_context.Info("Log a thing");
            logger_context.Info("Log the thing");

            var logger_without_context = OKLogManager.GetLogger("Gallery.Web", "test");
            logger_without_context.Info("Log another thing");

            logger.Fatal("this is fatal");
            logger.Error("this is a error");
            logger.Warn("this is a warning");
            logger.Info("this is a info");

            for (var i = 0; i < 20; i++)
            {
                Thread.Sleep(5000);
            }



        }
    }
}