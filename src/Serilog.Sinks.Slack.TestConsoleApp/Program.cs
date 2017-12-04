using System;

namespace Serilog.Sinks.Slack.TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
            Serilog.Sinks.Slack.SlackSink.ProxyUri = "url:8080";
            Serilog.Sinks.Slack.SlackSink.ProxyAuthString = "Negotiate  base64encoded user:pass";
            var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                //.WriteTo.Slack("C0W6DD7LZ", "xoxp-13773446566-13774642576-72300718082-1d35369b22", LevelAlias.Maximum)
                .WriteTo.Slack("Slack URL"
                //,(LogEvent l) => l.RenderMessage()
                )
                .CreateLogger();

            try
            {
                log.Verbose("This is an verbose message!");
                log.Debug("This is an debug message!");
                log.Information("This is an information message!");
                log.Warning("This is an warning message!");
                log.Error("This is an error message!");
                throw new Exception("This is an exception!");
            }
            catch (Exception exception)
            {
                log.Fatal(exception, "Exception catched at Main.");
            }
            Console.ReadLine();
        }
    }
}