using dotless.Core.configuration;
using dotless.Core;

namespace dotless.Compiler
{
    public class ContainerFactory
    {
        public ICoreServiceLocator GetContainer(DotlessConfiguration configuration)
        {
            var builder = new Autofac.ContainerBuilder();

            var registrar = new ServiceRegistrator();

            registrar.RegisterServices(new CompilerServiceRegistrar(builder), configuration);

            var container = builder.Build();

            return new CompilerServiceLocator(container);
        }
    }
}