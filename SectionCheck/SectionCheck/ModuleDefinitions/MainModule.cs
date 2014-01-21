using System;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SectionCheck.Services;
using XEP_CommonLibrary.Services;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Implementations;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheck.Services;

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
            RegisterTypes(_container);
            // _container.RegisterType<XEP_IDataCacheService, XEP_DataCacheServiceMock>(new TransientLifetimeManager());
            XEP_IQuantityManager manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            manager.SetScale(eEP_QuantityType.eForce, 1000.0); // just for test
            manager.SetScale(eEP_QuantityType.eMoment, 1000.0); // just for test
            // LOAD DATA from XML
            XEP_IDataCacheService dataCacheService = UnityContainerExtensions.Resolve<XEP_IDataCacheService>(_container);
            dataCacheService.Load(UnityContainerExtensions.Resolve<XEP_IDataCache>(_container));

            //dataCacheService.Save(UnityContainerExtensions.Resolve<XEP_IDataCache>(_container)); // todo_move that

            //load MainRegion using Prism View Discovery
            _regionManager.RegisterViewWithRegion(XEP_Constants.MainContentRegionName, () => _container.Resolve<XEP_MainView>());
        }
  
        static public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<XEP_IQuantityManager, XEP_QuantityManager>(new ContainerControlledLifetimeManager()); // singleton
            // data cache object registration
            container.RegisterType<XEP_ISectionShapeItem, XEP_SectionShapeItem>(new TransientLifetimeManager());
            container.RegisterType<XEP_ISectionShape, XEP_SectionShape>(new TransientLifetimeManager());
            container.RegisterType<XEP_IInternalForceItem, XEP_InternalForceItem>(new TransientLifetimeManager());
            container.RegisterType<XEP_IMaterialDataConcrete, XEP_MaterialDataConcrete>(new TransientLifetimeManager());
            container.RegisterType<XEP_IConcreteSectionData, XEP_ConcreteSectionData>(new TransientLifetimeManager());
            container.RegisterType<XEP_IOneSectionData, XEP_OneSectionData>(new TransientLifetimeManager());
            container.RegisterType<XEP_IOneMemberData, XEP_OneMemberData>(new TransientLifetimeManager());
            container.RegisterType<XEP_IStructure, XEP_Structure>(new TransientLifetimeManager());
            container.RegisterType<XEP_IDataCache, XEP_DataCache>(new ContainerControlledLifetimeManager()); // singleton for data cache
            container.RegisterType<XEP_IDataCacheService, /*XEP_DataCacheServiceMock*/XEP_DataCacheService>(new TransientLifetimeManager());
            // Resolvers
            container.RegisterType<XEP_IResolver<XEP_IInternalForceItem>, XEP_UnityResolver<XEP_IInternalForceItem>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_IStructure>, XEP_UnityResolver<XEP_IStructure>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_IOneMemberData>, XEP_UnityResolver<XEP_IOneMemberData>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_IOneSectionData>, XEP_UnityResolver<XEP_IOneSectionData>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_IConcreteSectionData>, XEP_UnityResolver<XEP_IConcreteSectionData>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_IMaterialDataConcrete>, XEP_UnityResolver<XEP_IMaterialDataConcrete>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_InternalForceItem>, XEP_UnityResolver<XEP_InternalForceItem>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_ISectionShape>, XEP_UnityResolver<XEP_ISectionShape>>(new TransientLifetimeManager());
            container.RegisterType<XEP_IResolver<XEP_ISectionShapeItem>, XEP_UnityResolver<XEP_ISectionShapeItem>>(new TransientLifetimeManager());
            //load container
            container.RegisterType<IDialogService, ModalDialogService>(new TransientLifetimeManager());
        }
    }
}
