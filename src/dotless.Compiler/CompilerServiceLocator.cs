using Autofac;

namespace dotless.Compiler
{
    public class CompilerServiceLocator : Core.ICoreServiceLocator
    {
        private IContainer _container;

        public CompilerServiceLocator(IContainer container)
        {
            _container = container;
        }

        public TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }
    }
}