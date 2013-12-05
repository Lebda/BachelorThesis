using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using SectionCheckInterfaces.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CssProperties.Services;

namespace XEP_CssProperties.ModuleDefinitions
{
    [Module(ModuleName = "CssPropertiesModule")]
    public class XEP_CssPropertiesModule : MyModuleBase
    {
        public XEP_CssPropertiesModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            XEP_ICssPropertiesService propertiesService = new XEP_CssPropertiesService(UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container));
            _container.RegisterInstance<XEP_ICssPropertiesService>("CssPropertiesModule", propertiesService);
            // Regions
            _regionManager.RegisterViewWithRegion(Constants.CssPropertiesRegionName, () => _container.Resolve<XEP_CssPropertiesView>());
        }
    }
}
