using Microsoft.Practices.Unity;
using System;

namespace XEP_Prism.Infrastructure
{
    /// <summary>
    /// Thanks to Glenn Block for showing this pattern to me.
    /// 
    /// This resolver allows a type to be passed to an object, without actually allowing the 
    /// container to leak into the object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XEP_UnityResolver<T> : XEP_IResolver<T>
    {
        readonly IUnityContainer _container;

        public XEP_UnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public T Resolve()
        {
            if (_container == null)
            {
                throw new ArgumentException("Unity container is null object can not bee resolved !");
            }
            return _container.Resolve<T>();
        }
    }
}
