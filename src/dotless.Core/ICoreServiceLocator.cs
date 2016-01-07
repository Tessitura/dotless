namespace dotless.Core
{
    public interface ICoreServiceLocator
    {
        TService GetInstance<TService>();
    }
}