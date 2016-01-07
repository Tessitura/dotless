using dotless.Core.configuration;
using dotless.Core.Cache;
using dotless.Core.Importers;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Parameters;
using dotless.Core.Stylizers;

namespace dotless.Core
{
    using dotless.Core.Engine;

    public class ServiceRegistrator
    {
        public void RegisterServices(ICoreServiceRegistrar serviceLocator, DotlessConfiguration configuration)
        {
            OverrideServices(serviceLocator, configuration);

            RegisterLocalServices(serviceLocator);

            RegisterCoreServices(serviceLocator, configuration);
        }

        private void OverrideServices(ICoreServiceRegistrar serviceLocator, DotlessConfiguration configuration)
        {
            if (configuration.Logger != null)
            {
                serviceLocator.Register<ILogger>(configuration.Logger);
            }
        }

        private void RegisterLocalServices(ICoreServiceRegistrar serviceLocator)
        {
            serviceLocator.Register<ICache, InMemoryCache>();
            serviceLocator.Register<IParameterSource, ConsoleArgumentParameterSource>();
            serviceLocator.Register<IPathResolver, RelativePathResolver>();
            serviceLocator.Register<ILogger, ConsoleLogger>();
        }

        private void RegisterCoreServices(ICoreServiceRegistrar serviceLocator, DotlessConfiguration configuration)
        {
            serviceLocator.Register<LoggerConfig>();

            serviceLocator.Register<IStylizer, PlainStylizer>();
            serviceLocator.RegisterInstance(configuration);
            serviceLocator.Register<ImporterConfig>();
            serviceLocator.Register<IImporter, Importer>();
            serviceLocator.Register<Parser.ParserConfig>();
            serviceLocator.Register<Parser.Parser>();

            if (!configuration.DisableParameters)
            {
                serviceLocator.Register<ILessEngine, ParameterDecorator>();
            }

            if (configuration.CacheEnabled)
            {
                serviceLocator.Register<ILessEngine, CacheDecorator>();
            }

            serviceLocator.Register<LessEngineConfig>();
            serviceLocator.Register<ILessEngine, LessEngine>();
            serviceLocator.Register<IFileReader>(configuration.LessSource);
        }
    }
}