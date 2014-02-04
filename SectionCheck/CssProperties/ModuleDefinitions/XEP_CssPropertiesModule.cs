using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckInterfaces.Infrastructure;

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
