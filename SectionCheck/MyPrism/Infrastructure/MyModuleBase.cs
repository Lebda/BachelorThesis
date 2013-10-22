using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MyPrism.Infrastructure
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
    }
}
