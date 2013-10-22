using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using SectionCheckInterfaces.Infrastucture;
using Microsoft.Practices.Prism.Modularity;

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
            _regionManager.RegisterViewWithRegion(Constants.MiddleContentRegionName, () => _container.Resolve<DrawSectionView>());
        }
    }
}
