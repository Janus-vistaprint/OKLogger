using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using OKLogger.Parsing;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using OKLogger.Parsing.JObjects;

namespace OKLogger.Tests
{

    public class JTokenFormatterTests
    {
        public IValueEscaper Scrubber = new CharEscaper('"');

        public JValue SampleStringJToken = new JValue("This is a test string");

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_StringFormatter_Formatting()
        {

            var testString = "This is a test string";
            var testval = new JValue(testString);

            var formatter = new StringJTokenFormatter(Scrubber);

            var result = formatter.Format(testval, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal(testString, result[string.Empty]);
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_StringFormatter_Handles()
        {
            var formatter = new StringJTokenFormatter(Scrubber);


            var result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.True(result, "JToken string formatter should handle token string");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken string formatter should not handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken string formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.TrueBool.Token.Type);
            Assert.False(result, "JToken string formatter should not handle boolean types");

            result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken string formatter should not handle float types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken string formatter should not handle object types");
        }


        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_BooleanFormatter_Formatting()
        {


            var formatter = new BooleanJTokenFormatter();

            var result = formatter.Format(SampleTokens.TrueBool.Token, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("true", result[string.Empty]);

            result = formatter.Format(SampleTokens.FalseBool.Token, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("false", result[string.Empty]);
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_BooleanFormatter_Handles()
        {
            var formatter = new BooleanJTokenFormatter();

            var result = formatter.Handles(SampleTokens.TrueBool.Token.Type);
            Assert.True(result, "JToken boolean formatter should handle token bool");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.True(result, "JToken boolean formatter should handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken boolean formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken boolean formatter should not handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken boolean formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken boolean formatter should not handle float types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken boolean formatter should not handle object types");
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_FloatFormatter_Formatting()
        {


            var formatter = new FloatJTokenFormatter();

            var result = formatter.Format(SampleTokens.Double.Token, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal("3.14", result[string.Empty]);

        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_FloatFormatter_Handles()
        {
            var formatter = new FloatJTokenFormatter();

            var result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.True(result, "JToken float formatter should handle token double");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.False(result, "JToken float formatter should not handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken float formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken float formatter should not handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken float formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken float formatter should not handle object types");
        }


        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_IntegerFormatter_Formatting()
        {


            var formatter = new IntegerJTokenFormatter();

            var result = formatter.Format(SampleTokens.Int.Token, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal(SampleTokens.Int.Value.ToString(), result[string.Empty]);

        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_IntegerFormatter_Handles()
        {
            var formatter = new IntegerJTokenFormatter();

            var result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken int formatter should not handle token double");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.False(result, "JToken int formatter should not handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken int formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken int formatter should not handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.True(result, "JToken int formatter not handle integer types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken int formatter should not handle object types");
        }


        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_GuidFormatter_Formatting()
        {


            var formatter = new GuidFormatter();

            var result = formatter.Format(SampleTokens.Guid.Token, 0);

            Assert.Single(result);
            Assert.True(result.ContainsKey(string.Empty), "Should have a single, empty key");
            Assert.Equal(SampleTokens.Guid.Value.ToString(), result[string.Empty]);

        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_GuidFormatter_Handles()
        {
            var formatter = new GuidJTokenFormatter();

            var result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken float formatter should not handle token double");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.False(result, "JToken float formatter should not handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken float formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.True(result, "JToken float formatter should  handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken float formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken float formatter should not handle float types");
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JObjectFormatter_Handles()
        {
            var formatter = new JObjectFormatter(new DefaultJTokenFormatters(), ",", 3);

            var result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken float formatter should not handle token double");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.False(result, "JToken float formatter should not handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken float formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken float formatter should  handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken float formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.True(result, "JToken float formatter should handle object types");
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JObjectFormatter_NestedObject()
        {
            var testValue = JObject.FromObject(new
            {
                Alpha = "a",
                Beta = 1.0,
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

            });

            var formatter = new JObjectFormatter(new DefaultJTokenFormatters(), ",", 3);
            var result = formatter.Format(testValue, 0);

            Assert.Equal(6, result.Count);
            Assert.Equal("a", result["Alpha"]);
            Assert.Equal("1", result["Beta"]);
            Assert.Equal("bkennedy", result["Receiver_Name"]);
            Assert.Equal("2", result["Receiver_User_Id"]);
            Assert.Equal("admin", result["Sender_Name"]);
            Assert.Equal("1", result["Sender_User_Id"]);

        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JObjectFormatter_Formatting()
        {
            var testValue = JObject.FromObject(new
            {
                Alpha = "a",
                Beta = 1.0,
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

            });

            var formatter = new JObjectFormatter(new DefaultJTokenFormatters(), ",", 3);
            var result = formatter.Format(testValue, 0);

            Assert.Equal(6, result.Count);
            Assert.Equal("a", result["Alpha"]);
            Assert.Equal("1", result["Beta"]);
            Assert.Equal("bkennedy", result["Receiver_Name"]);
            Assert.Equal("2", result["Receiver_User_Id"]);
            Assert.Equal("admin", result["Sender_Name"]);
            Assert.Equal("1", result["Sender_User_Id"]);

        }


        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JArrayFormatter_Handles()
        {
            var formatter = new ArrayJTokenFormatter(new DefaultJTokenFormatters(), ",", 3);

            var result = formatter.Handles(SampleTokens.Double.Token.Type);
            Assert.False(result, "JToken array formatter should not handle token double");

            result = formatter.Handles(SampleTokens.FalseBool.Token.Type);
            Assert.False(result, "JToken array formatter should not handle token bool");

            result = formatter.Handles(SampleTokens.String.Token.Type);
            Assert.False(result, "JToken array formatter should not handle string types");

            result = formatter.Handles(SampleTokens.Guid.Token.Type);
            Assert.False(result, "JToken array formatter should  handle guid types");

            result = formatter.Handles(SampleTokens.Int.Token.Type);
            Assert.False(result, "JToken array formatter should not handle integer types");

            result = formatter.Handles(SampleTokens.TestObject.Token.Type);
            Assert.False(result, "JToken array formatter should not handle object types");

            var testArray = JArray.FromObject(new SampleObject[] {
                new SampleObject { Alpha = "first", Beta = 1 },
                new SampleObject { Alpha = "second", Beta = 2 }
                });

            result = formatter.Handles(testArray.Type);
            Assert.True(result, "JToken array formatter should handle array  types");
        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JArrayFormatter_Formatting_ObjectArray()
        {
            var formatter = new ArrayJTokenFormatter(new DefaultJTokenFormatters(), ",", 3);

            var testArray =  JArray.FromObject( new SampleObject[] {
                new SampleObject { Alpha = "first", Beta = 1 },
                new SampleObject { Alpha = "second", Beta = 2 }
                });

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var result = formatter.Format(testArray, 0);
            stopwatch.Stop();


            Assert.Equal(4, result.Count);
            Assert.Equal("first", result["_0_Alpha"]);
            Assert.Equal("second", result["_1_Alpha"]);
            Assert.Equal("1", result["_0_Beta"]);
            Assert.Equal("2", result["_1_Beta"]);

        }

        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JArrayFormatter_Formatting_ValueArray()
        {
            var formatter = new ArrayJTokenFormatter(new DefaultJTokenFormatters(), ",", 3);

            var testArray = JArray.FromObject(new int[] { 1, 1, 2, 3, 5, 8, 13});

            var result = formatter.Format(testArray, 0);

            Assert.Single(result);
            Assert.Equal("1,1,2,3,5,8,13", result[string.Empty]);


        }
        /// <summary>
        /// This tests the entity formatter that forwards to the jtoken forwarder 
        /// </summary>
        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JTokenFormatter_Formatting_Object()
        {
            var testValue = JObject.FromObject(new
            {
                Alpha = "a",
                Beta = 1.0,
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

            });

            var formatter = new JTokenFormatter(new DefaultJTokenFormatters(), ",", 3);
            var result = formatter.Format(testValue, 0);

            Assert.Equal(6, result.Count);
            Assert.Equal("a", result["Alpha"]);
            Assert.Equal("1", result["Beta"]);
            Assert.Equal("bkennedy", result["Receiver_Name"]);
            Assert.Equal("2", result["Receiver_User_Id"]);
            Assert.Equal("admin", result["Sender_Name"]);
            Assert.Equal("1", result["Sender_User_Id"]);

        }

        /// <summary>
        /// This tests the entity formatter that forwards to the jtoken forwarder 
        /// </summary>
        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JTokenFormatter_Formatting()
        {
            var testArray = JArray.FromObject(new int[] { 1, 1, 2, 3, 5, 8, 13 });


            var formatter = new JTokenFormatter(new DefaultJTokenFormatters(), ",", 3);
            var result = formatter.Format(testArray, 0);

            Assert.Single(result);
            Assert.Equal("1,1,2,3,5,8,13", result[string.Empty]);

        }


        [Fact]
        [Trait("Category", "JToken")]
        public void JToken_JTokenFormatter_ComplexObject()
        {
            var testVal = JObject.Parse(@"{
	            ""msg"": ""JS error message"",

                ""logLevel"": ""error"",
	            ""data"": {
                    ""link"": ""https://test.com"",
		            ""description"": ""error description"",
		            ""values"": [
			            ""value1"",
			            ""value2"",
			            ""value3""
		            ]
                }
            }");

            var formatter = new JTokenFormatter(new DefaultJTokenFormatters(), ",", 3);
            var result = formatter.Format(testVal, 0);

            var propParser = new PropertyParser(new DefaultFormatters(), ",");
            var result2 = propParser.Parse(testVal);


            Assert.Equal(5, result.Count);
            Assert.Equal("JS error message", result["msg"]);
            Assert.Equal("error", result["logLevel"]);
            Assert.Equal("https://test.com", result["data_link"]);
            Assert.Equal("error description", result["data_description"]);
            Assert.Equal("value1,value2,value3", result["data_values"]);
        }
    }
}

