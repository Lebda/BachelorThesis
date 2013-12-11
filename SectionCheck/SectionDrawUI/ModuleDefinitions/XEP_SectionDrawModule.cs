using System;
using System.Linq;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using SectionDrawUI.Services;
using XEP_SectionDrawer.Interfaces;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionDrawUI.ModuleDefinitions
{
    [Module(ModuleName = "SectionDrawModule")]
    public class XEP_SectionDrawModule : MyModuleBase
    {
        public XEP_SectionDrawModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            // Services
            _container.RegisterType<ICssDataService, CssDataService>();

            // Regions
           _regionManager.RegisterViewWithRegion(XEP_Constants.MiddleContentRegionName, () => _container.Resolve<XEP_DrawSectionView>());
        }
    }
}
