using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using XEP_SectionCheckCommon.Interfaces;
using SectionDrawUI.Services;
using SectionDrawUI.Models;
using XEP_SectionDrawer.Interfaces;
using XEP_SectionCheckCommon.Infrastucture;

namespace SectionDrawUI.ModuleDefinitions
{
    [Module(ModuleName = "SectionDrawModule")]
    public class SectionDrawModule : MyModuleBase
    {
        public SectionDrawModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            // Services
            _container.RegisterType<ICssDataService, CssDataService>();

            // Regions
           _regionManager.RegisterViewWithRegion(XEP_Constants.MiddleContentRegionName, () => _container.Resolve<DrawSectionView>());
        }
    }
}
