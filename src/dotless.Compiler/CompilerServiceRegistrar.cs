using System;

using Autofac;

using dotless.Core;

namespace dotless.Compiler
{

    public class CompilerServiceRegistrar : ICoreServiceRegistrar
    {
        private ContainerBuilder _builder;

        public CompilerServiceRegistrar(ContainerBuilder builder)
        {
            _builder = builder;
        }

        public void RegisterInstance<T>(T instance)
            where T : class
        {
            _builder.RegisterInstance(instance);
        }
        

        public void Register<TService, TImplementor>() where TImplementor : TService
        {
            
            _builder.RegisterType<TImplementor>().As<TService>();
        }

        public void Register<TService>()
        {
            _builder.RegisterType<TService>();
        }

        public void Register<TService>(Type implementorType)
        {
            _builder.RegisterType<TService>().As(implementorType);
        }
    }
}