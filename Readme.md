[![Build status](https://ci.appveyor.com/api/projects/status/c3ngkopnxb4stwe6/branch/master?svg=true)](https://ci.appveyor.com/project/tparnell8/oklogger/branch/master)

## Ok Logger

OK Logger is designed as a logging wrapper to enforce a consistent key-value based logging format that is easy to query against. Current it is designed to wrap around a log4net logger and uses lognet configuration to route log messages.

## Usage

The log manager is used to create individual loggers. Loggers must created with an identifying string and an environment.

```csharp

    OKLogManager.Configure(new System.IO.FileInfo("logging.config.xml"));

    // get a basic logger
    var logger = OKLogManager.GetLogger("MyApp.Web", "dev");


```

A basic log message uses the appropriate method based on severity, passes a fixed message string and an optional object containing context.

```csharp

logger.Error("Error accessing database", new
{
    SessionId = 4,
    Sender = new
    {
        Name = "admin",
        User_Id = 1
    }
});

```

Additional context shared between logging calls can be added by providing a context object udring logger creation...


```csharp
// get a logger with context. context properties are added to each logging message
var logger = OKLogManager.GetLogger("Gallery.Web", "dev", new
{
    Session_Id = "abc_123",
    Http_Referer = "http://www.google.com"
});

```

or by a callback method that will be called before each logging statement

```csharp
// add a callback before each logging event to add additional data
var logger_context = OKLogManager.GetLogger("MyApp.Web", "test")
    .WithContext(() => { return new { a = 1, b = 2 }; })
    .WithContext(() => { return new { c = 1, d = 2 }; });
```