using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace XEP_Prism.Infrastructure
{
    public abstract class MyModuleBase : IModule
    {
        protected readonly IUnityContainer _container;
        protected readonly IRegionManager _regionManager;

        public MyModuleBase(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
        #region IModule Members

        public abstract void Initialize();

        #endregion
        static public void RegisterWithResolver<Tinterface, Tmanager, Uimpl, ResolverManager>(IUnityContainer container)
            where Tmanager : LifetimeManager, new()
            where ResolverManager : LifetimeManager, new()
            where Uimpl : class, Tinterface
        {
            container.RegisterType<Tinterface, Uimpl>(new Tmanager());
            container.RegisterType<XEP_IResolver<Tinterface>, XEP_UnityResolver<Tinterface>>(new ResolverManager());
        }
    }
}
