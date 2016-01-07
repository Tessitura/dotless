namespace dotless.Core.Loggers
{
    using System;

    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(LoggerConfig config) : base(config.LogLevel) {}

        protected override void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}