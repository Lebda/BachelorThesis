using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using SectionCheckInterfaces.Infrastucture;
using XEP_CommonLibrary.Services;
using Microsoft.Practices.Prism.Modularity;
using SectionCheck.Services;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;

namespace SectionCheck.ModuleDefinitions
{
    [Module(ModuleName = "MainModule")]
    public class MainModule : MyModuleBase
    {
        public MainModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        public override void Initialize()
        {
            //load container
            _container.RegisterType<IDialogService, ModalDialogService>(new TransientLifetimeManager());
            _container.RegisterType<XEP_IQuantityManager, XEP_QuantityManager>(new ContainerControlledLifetimeManager()); // singleton
            XEP_IQuantityManager test = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            test.SetScale(eEP_QuantityType.eForce, 1000.0); // just for test
            test.SetScale(eEP_QuantityType.eMoment, 1000.0); // just for test
            //load MainRegion using Prism View Discovery
            _regionManager.RegisterViewWithRegion(Constants.MainContentRegionName, () => _container.Resolve<MainView>());
        }
    }
}
