using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using OKLogger.Parsing;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace OKLogger.Tests
{
    public class FormattingTests
    {

        public IValueEscaper Scrubber = new CharEscaper('"');


        [Fact]
        public void ObjectFormatter_Simple()
        {
            var objectToFormat = new
            {
                Alpha = "a",
                Beta = 1.0,
                EnumProp = SampleEnum.SecondValue

            };

            var formatters = new DefaultFormatters();
            var objectFormatter = new ObjectFormatter(formatters, ",", 5);
            var formattedObject = objectFormatter.Format(objectToFormat, 0);

        }

        [Fact]
        public void ObjectFormatter_SimpleList()
        {
            var objectToFormat = new
            {
                Alpha = new List<int> { 1, 2, 3, 4, 5 }
            };

            var formatters = new DefaultFormatters();
            var objectFormatter = new ObjectFormatter(formatters, ",", 5);
            var result = objectFormatter.Format(objectToFormat, 0);

            Assert.Single(result);
            Assert.Equal("1,2,3,4,5", result["Alpha"]);


        }

        [Fact]
        public void DictionaryFormatter()
        {
            var objectToFormat = new Dictionary<string, int>()
            {
                { "A" , 1 },
                { "B", 2 },
                { "C", 3 }

            };

            var formatters = new DefaultFormatters();
            var objectFormatter = new DictionaryFormatter(",", Scrubber);
            var result = objectFormatter.Format(objectToFormat, 0);

            Assert.Equal(3, result.Count);
            Assert.Equal("1", result["A"]);
            Assert.Equal("2", result["B"]);
            Assert.Equal("3", result["C"]);


        }


        [Fact]
        public void ObjectFormatter_Nested()
        {
            var stopwatch = Stopwatch.StartNew();

            var objectToFormat = new
            {
                Alpha = "a",
                Beta = 1.0,
                EnumProp = SampleEnum.SecondValue,
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

            };

            var formatters = new DefaultFormatters();
            var objectFormatter = new ObjectFormatter(formatters, ",", 5);
            var result = objectFormatter.Format(objectToFormat, 0);
            stopwatch.Stop();
            var duration = stopwatch.ElapsedMilliseconds;

            Assert.Equal(7, result.Count);
            Assert.Equal("a",result["Alpha"]);
            Assert.Equal("1",result["Beta"]);
            Assert.Equal("SecondValue",result["EnumProp"]);
            Assert.Equal("bkennedy",result["Receiver_Name"]);
            Assert.Equal("2",result["Receiver_User_Id"]);
            Assert.Equal("admin", result["Sender_Name"]);
            Assert.Equal("1",result["Sender_User_Id"]);


        }


        [Fact]
        public void ArrayFormatter_Int()
        {
            var testArray = new int[] { 1, 2, 3, 4 };

            var arrayFormatter = new ArrayFormatter(",", Scrubber);
            var result = arrayFormatter.Format(testArray, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("1,2,3,4", result[string.Empty]);



        }

        [Fact]
        public void ArrayFormatter_Double()
        {
            var testArray = new double[] { 1.1, 2.2, 3.3, 4.4 };

            var arrayFormatter = new ArrayFormatter(",", Scrubber);
            var result = arrayFormatter.Format(testArray, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("1.1,2.2,3.3,4.4", result[string.Empty]);

        }

        [Fact]
        public void ArrayFormatter_String()
        {
            var testArray = new string[] { "alpha", "beta", "gamma" };

            var arrayFormatter = new ArrayFormatter(",", Scrubber);
            var result = arrayFormatter.Format(testArray, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("alpha,beta,gamma", result[string.Empty]);
        }

        [Fact]
        public void GuidFormatter()
        {
            var testGuid = new Guid("DF0187E1-E1EB-4A9F-A528-4AFEBFECF4A5");

            var formatter = new GuidFormatter();
            var result = formatter.Format(testGuid, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("df0187e1-e1eb-4a9f-a528-4afebfecf4a5",result[string.Empty]);
        }

        [Fact]
        public void EnumFormatter()
        {
            var testval = SampleEnum.SecondValue;

            var formatter = new EnumFormatter();
            var result = formatter.Format(testval, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("SecondValue", result[string.Empty]);
        }


        [Fact]
        public void EnumFormatter_Nullable_WithValue()
        {
            SampleEnum? testval = SampleEnum.SecondValue;

            var formatter = new EnumFormatter();
            var result = formatter.Format(testval, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("SecondValue", result[string.Empty]);
        }


        [Fact]
        public void EnumFormatter_Nullable_NoValue()
        {
            SampleEnum? testval = null;

            var formatter = new EnumFormatter();
            var result = formatter.Format(testval, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal(result[string.Empty], string.Empty);
        }

        [Fact]
        public void Nullable_Integer_With_Value()
        {
            int? testVal = 3;

            var formatter = new NumericFormatter();
            var result = formatter.Format(testVal, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("3", result[string.Empty]);
        }

        [Fact]
        public void Nullable_Integer_No_Value()
        {
            int? testVal = null;

            var formatter = new NumericFormatter();
            var result = formatter.Format(testVal, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal(result[string.Empty], string.Empty);
        }

        [Fact]
        public void PropParser_JsonObject()
        {
            var testVal = new
            {
                A = "test_String",
                B = JObject.Parse("{\"propA\" : [1,2,3,4], \"propB\" : \"some_val\"}")
            };

            var propParser = new PropertyParser(new DefaultFormatters(), ",");
            var result = propParser.Parse(testVal);

        }



        [Fact]
        public void PropParser_Escape()
        {
            var testVal = new
            {
                A = "test_S\"tring"
            };

            var propParser = new PropertyParser(new DefaultFormatters(), ",");
            var result = propParser.Parse(testVal);

            Assert.Equal(1, result.Count);
            Assert.True(result.ContainsKey("A"), "Should have a single, empty key");
            Assert.Equal("test_S_tring", result["A"]);
        }

        [Fact]
        public void DateTimeFormatter()
        {
            var testVal = new DateTime(1979, 3, 1, 5, 4, 3, 555);


            var formatter = new DateTimeFormatter(Scrubber);

            var result = formatter.Format(testVal, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("1979-03-01 05:04:03Z", result[string.Empty]);
        }

        [Fact]
        public void BooleanFormatter()
        {
            var formatter = new BooleanFormatter();
            var result = formatter.Format(false, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("false", result[string.Empty]);

            result = formatter.Format(true, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("true", result[string.Empty]);

            Assert.True(formatter.Handles(typeof(bool)), "Should handle boolean");
            Assert.True(formatter.Handles(typeof(Boolean)), "Should handle boolean");

            Assert.False(formatter.Handles(typeof(string)), "Should not handle non-boolean types");
            Assert.False(formatter.Handles(typeof(object)), "Should not handle non-boolean types");

        }


        public enum SampleEnum
        {
            FirstValue = 1,
            SecondValue = 2
        }
    }
}
