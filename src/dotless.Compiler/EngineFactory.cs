namespace dotless.Compiler
{
    public class EngineFactory
    {
        public Core.configuration.DotlessConfiguration Configuration { get; set; }

        public EngineFactory(Core.configuration.DotlessConfiguration configuration)
        {
            Configuration = configuration;
        }

        public EngineFactory() : this(Core.configuration.DotlessConfiguration.GetDefault())
        {
        }

        public Core.ILessEngine GetEngine()
        {
            return GetEngine(new ContainerFactory());
        }

        public Core.ILessEngine GetEngine(ContainerFactory containerFactory)
        {
            var container = containerFactory.GetContainer(Configuration);

            Core.ICoreServiceLocator serviceLocator = null;

            return serviceLocator.GetInstance<Core.ILessEngine>();
        }
    }
}