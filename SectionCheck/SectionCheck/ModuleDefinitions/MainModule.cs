using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using SectionCheckInterfaces.Infrastucture;
using CommonLibrary.Services;
using Microsoft.Practices.Prism.Modularity;

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
            //_container.RegisterType(typeof(IEventAggregatorResolver<>), typeof(EventAggregatorResolver<>));

            //load MainRegion using Prism View Discovery
            _regionManager.RegisterViewWithRegion(Constants.MainContentRegionName, () => _container.Resolve<MainView>());
        }
    }
}
