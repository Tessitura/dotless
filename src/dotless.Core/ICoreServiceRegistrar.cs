using System;

namespace dotless.Core
{
    public interface ICoreServiceRegistrar
    {
        void RegisterInstance<T>(T instance) where T : class;

        void Register<TService>();

        void Register<TService, TImplementor>() where TImplementor : TService;

        void Register<TService>(Type implementorType);
    }
}