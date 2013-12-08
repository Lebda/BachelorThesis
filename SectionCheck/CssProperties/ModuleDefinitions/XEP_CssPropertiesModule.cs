using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastucture;

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
            // Regions
            _regionManager.RegisterViewWithRegion(XEP_Constants.CssPropertiesRegionName, () => _container.Resolve<XEP_CssPropertiesView>());
        }
    }
}
