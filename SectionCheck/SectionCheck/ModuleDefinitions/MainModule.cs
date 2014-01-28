﻿using System;
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
        static bool _isMock = true;
        public MainModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }
        // http://msdn.microsoft.com/en-us/library/ff660872%28v=pandp.20%29.aspx // lifetime managers
        public override void Initialize()
        {
            RegisterTypes(_container);
            XEP_IQuantityManager manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            PrepareQuantityManager(ref manager);
            // LOAD DATA from XML
            XEP_IDataCacheService dataCacheService = UnityContainerExtensions.Resolve<XEP_IDataCacheService>(_container);
            dataCacheService.Load(UnityContainerExtensions.Resolve<XEP_IDataCache>(_container));
            if (_isMock)
            {
                dataCacheService.Save(UnityContainerExtensions.Resolve<XEP_IDataCache>(_container)); // todo_move that
            }
            //load MainRegion using Prism View Discovery
            _regionManager.RegisterViewWithRegion(XEP_Constants.MainContentRegionName, () => _container.Resolve<XEP_MainView>());
        }

        static private void PrepareQuantityManager(ref XEP_IQuantityManager manager)
        { // just for test, move it elsewhere
            manager.SetScale(eEP_QuantityType.eForce, 1000.0);
            manager.SetScale(eEP_QuantityType.eMoment, 1000.0);
            manager.SetScale(eEP_QuantityType.eStress, 1e6);
            manager.SetScale(eEP_QuantityType.eStrain, 1e-3);
        }
        static public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<XEP_IQuantityManager, XEP_QuantityManager>(new ContainerControlledLifetimeManager()); // singleton
            // data cache object registration
            MyModuleBase.RegisterWithResolver<XEP_ISectionShapeItem, TransientLifetimeManager, XEP_SectionShapeItem, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IESDiagramItem, TransientLifetimeManager, XEP_ESDiagramItem, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IInternalForceItem, TransientLifetimeManager, XEP_InternalForceItem, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_ISectionShape, TransientLifetimeManager, XEP_SectionShape, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IMaterialData, TransientLifetimeManager, XEP_MaterialDataImpl, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IMaterialDataConcrete, TransientLifetimeManager, XEP_MaterialDataConcrete, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IConcreteSectionData, TransientLifetimeManager, XEP_ConcreteSectionData, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IOneSectionData, TransientLifetimeManager, XEP_OneSectionData, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IOneMemberData, TransientLifetimeManager, XEP_OneMemberData, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IStructure, TransientLifetimeManager, XEP_Structure, TransientLifetimeManager>(container);
            MyModuleBase.RegisterWithResolver<XEP_IDataCache, ContainerControlledLifetimeManager, XEP_DataCache, TransientLifetimeManager>(container); // singleton
            MyModuleBase.RegisterWithResolver<XEP_IMaterialLibrary, ContainerControlledLifetimeManager, XEP_MaterialLibrary, TransientLifetimeManager>(container); // singleton
            if (_isMock)
            {
                MyModuleBase.RegisterWithResolver<XEP_IDataCacheService, TransientLifetimeManager, XEP_DataCacheServiceMock, TransientLifetimeManager>(container);
            }
            else
            {
                MyModuleBase.RegisterWithResolver<XEP_IDataCacheService, TransientLifetimeManager, XEP_DataCacheService, TransientLifetimeManager>(container);

            }
            //load container
            container.RegisterType<IDialogService, ModalDialogService>(new TransientLifetimeManager());
        }
    }
}
