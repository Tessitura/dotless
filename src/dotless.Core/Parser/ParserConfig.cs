namespace dotless.Core.Parser
{
    using dotless.Core.configuration;

    public class ParserConfig
    {
        public bool Debug { get; set; }

        public int Optimization { get; set; }

        public ParserConfig(DotlessConfiguration config)
        {
            if (config != null)
            {
                Debug = config.Debug;

                Optimization = config.Optimization;
            }
        }
    }
}