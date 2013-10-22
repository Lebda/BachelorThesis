using Microsoft.Practices.Unity;

namespace MyPrism.Infrastructure
{
    public class UnityResolver<T> : IResolver<T>
    {
        readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve()
        {
            return _container.Resolve<T>();
        }
    }
}
