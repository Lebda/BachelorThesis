using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckInterfaces.Infrastructure;
using XEP_SectionCheckInterfaces.SectionDrawer;
using XEP_SectionDrawer.Infrastructure;

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
            RegisterTypes(_container);

            // Regions
           _regionManager.RegisterViewWithRegion(XEP_Constants.MiddleContentRegionName, () => _container.Resolve<XEP_DrawSectionView>());
        }

        static public void RegisterTypes(IUnityContainer container)
        {
            MyModuleBase.RegisterWithResolver<XEP_ICssDataShape, TransientLifetimeManager, XEP_CssDataShape, TransientLifetimeManager>(container);
        }
    }
}
