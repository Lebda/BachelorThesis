using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPrism.Infrastructure;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Regions;
using XEP_CommonLibrary.Services;
using Microsoft.Practices.Prism.Modularity;
using SectionCheck.Services;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Implementations;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheck.ModuleDefinitions
{
    [Module(ModuleName = "MainModule")]
    public class MainModule : MyModuleBase
    {
        public MainModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }
        // http://msdn.microsoft.com/en-us/library/ff660872%28v=pandp.20%29.aspx // lifetime managers
        public override void Initialize()
        { 
            //
            _container.RegisterType<XEP_IQuantityManager, XEP_QuantityManager>(new ContainerControlledLifetimeManager()); // singleton
            XEP_IQuantityManager test = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            test.SetScale(eEP_QuantityType.eForce, 1000.0); // just for test
            test.SetScale(eEP_QuantityType.eMoment, 1000.0); // just for test
            // data cache object registration
            _container.RegisterType<XEP_IOneSectionData, XEP_OneSectionData>(new TransientLifetimeManager());
            _container.RegisterType<XEP_IOneMemberData, XEP_OneMemberData>(new TransientLifetimeManager());
            _container.RegisterType<XEP_IStructure, XEP_Structure>(new TransientLifetimeManager());
            _container.RegisterType<XEP_IDataCache, XEP_DataCache>(new ContainerControlledLifetimeManager()); // singleton for data cache
            _container.RegisterType<XEP_IInternalForceItem, XEP_InternalForceItem>(new TransientLifetimeManager());
            _container.RegisterType<XEP_IDataCacheService, XEP_DataCacheService>(new TransientLifetimeManager());

            // Set dump data just for now
            XEP_IDataCacheService dataCacheService = UnityContainerExtensions.Resolve<XEP_IDataCacheService>(_container);
            dataCacheService.Load(UnityContainerExtensions.Resolve<XEP_IDataCache>(_container));

            dataCacheService.Save((XEP_IXmlWorker)UnityContainerExtensions.Resolve<XEP_IDataCache>(_container));

            //load container
            _container.RegisterType<IDialogService, ModalDialogService>(new TransientLifetimeManager());
            //load MainRegion using Prism View Discovery
            _regionManager.RegisterViewWithRegion(XEP_Constants.MainContentRegionName, () => _container.Resolve<XEP_MainView>());
        }
    }
}
