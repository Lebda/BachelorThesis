using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using SectionCheckInterfaces.Infrastucture;
using Microsoft.Practices.Prism.Modularity;
using SectionCheckInterfaces.Interfaces;
using SectionDrawUI.Services;
using SectionDrawUI.Models;

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
            _container.RegisterType<ISectionShapeService, SectionShapeService>();
            _container.RegisterType<ISectionShape, SectionShapeModel>();

            // Regions
           _regionManager.RegisterViewWithRegion(Constants.MiddleContentRegionName, () => _container.Resolve<DrawSectionView>());
        }
    }
}
