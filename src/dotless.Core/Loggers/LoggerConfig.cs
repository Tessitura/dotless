namespace dotless.Core.Loggers
{
    using dotless.Core.configuration;

    public class LoggerConfig
    {
        public LogLevel LogLevel { get; set; }

        public LoggerConfig(DotlessConfiguration config)
        {
            LogLevel = config.LogLevel;
        }
    }
}