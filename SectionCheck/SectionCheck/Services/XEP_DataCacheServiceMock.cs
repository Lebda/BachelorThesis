using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using SectionCheck.Services;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheck.Services
{
    public class XEP_DataCacheServiceMock : XEP_DataCacheService
    {
        readonly XEP_IResolver<XEP_IOneSectionData> _resolverXEP_IOneSectionData = null;
        readonly XEP_IResolver<XEP_IOneMemberData> _resolverXEP_IOneMemberData = null;
        readonly XEP_IResolver<XEP_IStructure> _resolverXEP_IStructure = null;
        readonly XEP_IResolver<XEP_IConcreteSectionData> _resolverXEP_IConcreteSectionData = null;
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolverXEP_ISectionShapeItem = null;
        readonly XEP_IResolver<XEP_ISectionShape> _resolverXEP_ISectionShape = null;
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverXEP_IInternalForceItem = null;
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolverXEP_IMaterialDataConcrete = null;
        readonly XEP_IResolver<XEP_IESDiagramItem> _resolverXEP_IESDiagramItem = null;
        readonly XEP_IResolver<XEP_IMaterialLibrary> _resolverMatLib = null;
        readonly XEP_IResolver<XEP_ISetupParameters> _resolverSetup = null;
        public XEP_DataCacheServiceMock(
            XEP_IResolver<XEP_IOneSectionData> resolverXEP_IOneSectionData,
            XEP_IResolver<XEP_IOneMemberData> resolverXEP_IOneMemberData,
            XEP_IResolver<XEP_IStructure> resolverXEP_IStructure,
            XEP_IResolver<XEP_IConcreteSectionData> resolverXEP_IConcreteSectionData,
            XEP_IResolver<XEP_ISectionShapeItem> resolverXEP_ISectionShapeItem,
            XEP_IResolver<XEP_ISectionShape> resolverXEP_ISectionShape,
            XEP_IResolver<XEP_IInternalForceItem> resolverXEP_IInternalForceItem,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolverXEP_IMaterialDataConcrete,
            XEP_IResolver<XEP_IESDiagramItem> resolverXEP_IESDiagramItem,
            XEP_IResolver<XEP_IMaterialLibrary> resolverMatLib,
            XEP_IResolver<XEP_ISetupParameters> resolverSetup)
        {
            _resolverXEP_IOneSectionData = resolverXEP_IOneSectionData;
            _resolverXEP_IOneMemberData = resolverXEP_IOneMemberData;
            _resolverXEP_IStructure = resolverXEP_IStructure;
            _resolverXEP_IConcreteSectionData = resolverXEP_IConcreteSectionData;
            _resolverXEP_ISectionShapeItem = resolverXEP_ISectionShapeItem;
            _resolverXEP_ISectionShape = resolverXEP_ISectionShape;
            _resolverXEP_IInternalForceItem = resolverXEP_IInternalForceItem;
            _resolverXEP_IMaterialDataConcrete = resolverXEP_IMaterialDataConcrete;
            _resolverXEP_IESDiagramItem = resolverXEP_IESDiagramItem;
            _resolverMatLib = resolverMatLib;
            _resolverSetup = resolverSetup;
        }

        #region XEP_IDataCacheService Members
        public override eDataCacheServiceOperation Load(XEP_IDataCache dataCache)
        {
            dataCache.SetupParameters = GetSetup();
            dataCache.MaterialLibrary = GetMaterialLibrary(dataCache.SetupParameters);
            XEP_IOneSectionData newSectionData = _resolverXEP_IOneSectionData.Resolve();
            newSectionData.InternalForces = GetInternalForces();
            newSectionData.Name = "Section 1";
            newSectionData.ConcreteSectionData = GetConcreteSectionData();
            XEP_IOneMemberData newMemberData = _resolverXEP_IOneMemberData.Resolve();
            newMemberData.Name = "Member 1";
            newMemberData.SaveOneSectionData(newSectionData);
            XEP_IStructure newStructure = _resolverXEP_IStructure.Resolve();
            newStructure.SaveOneMemberData(newMemberData);
            dataCache.Structure = newStructure;
            return eDataCacheServiceOperation.eSuccess;
        }
        #endregion

        #region METHODS 
        private XEP_ISetupParameters GetSetup()
        {
            XEP_ISetupParameters data = _resolverSetup.Resolve();
            data.GammaC.Value = 1.5;
            data.GammaS.Value = 1.15;
            data.AlphaCc.Value = 1.0;
            data.AlphaCt.Value = 1.0;
            data.Fi.Value = 2.48;
            data.FiEff.Value = 2.48;
            data.Name = "Setup parameters";
            return data;
        }

        private XEP_IMaterialLibrary GetMaterialLibrary(XEP_ISetupParameters setup)
        {
            XEP_IMaterialLibrary data = _resolverMatLib.Resolve();
            data.SaveOneMaterialDataConcrete(GetMaterialDataConcrete(setup));
            data.SaveOneMaterialDataConcrete(GetMaterialDataConcrete2(setup));
            data.Name = "Material Library";
            return data;
        }

        private XEP_IConcreteSectionData GetConcreteSectionData()
        {
            XEP_IConcreteSectionData data = _resolverXEP_IConcreteSectionData.Resolve();
            data.Name = "Concrete section parts";
            data.SectionShape = GetSectionShape();
            data.MaterialData = GetMaterialDataConcrete(_resolverSetup.Resolve());
            return data;
        }

        private XEP_IMaterialDataConcrete GetMaterialDataConcrete(XEP_ISetupParameters setup)
        {
            XEP_IMaterialDataConcrete item = _resolverXEP_IMaterialDataConcrete.Resolve();
            item.Name = "C25/30";
            item.Fck.Value = 25.0*1e6;
            item.FckCube.Value = 30.0*1e6;
            item.EpsC1.Value = 2.1 * 1e-3;
            item.EpsCu1.Value = 3.5 * 1e-3;
            item.EpsC2.Value = 2.0 * 1e-3;
            item.EpsCu2.Value = 3.5 * 1e-3;
            item.EpsC3.Value = 1.75 * 1e-3;
            item.EpsCu3.Value = 3.5 * 1e-3;
            item.N.Value = 2.0;
            item.DiagramType = eEP_MaterialDiagramType.eBiliUls;
            item.MatFromLib = true;
            item.CreatePoints(setup);
            return item;
        }
        private XEP_IMaterialDataConcrete GetMaterialDataConcrete2(XEP_ISetupParameters setup)
        {
            XEP_IMaterialDataConcrete item = _resolverXEP_IMaterialDataConcrete.Resolve();
            item.Name = "C30/37";
            item.Fck.Value = 30.0 * 1e6;
            item.FckCube.Value = 37.0 * 1e6;
            item.EpsC1.Value = 2.2 * 1e-3;
            item.EpsCu1.Value = 3.5 * 1e-3;
            item.EpsC2.Value = 2.0 * 1e-3;
            item.EpsCu2.Value = 3.5 * 1e-3;
            item.EpsC3.Value = 1.75 * 1e-3;
            item.EpsCu3.Value = 3.5 * 1e-3;
            item.N.Value = 2.0;
            item.MatFromLib = true;
            item.DiagramType = eEP_MaterialDiagramType.eBiliUls;
            item.CreatePoints(setup);
            return item;
        }

        private XEP_ISectionShape GetSectionShape()
        {
            XEP_ISectionShape data = _resolverXEP_ISectionShape.Resolve();
            data.B.Value = 0.3;
            data.H.Value = 0.5;
            data.Hhole.Value = 0.05;
            data.Bhole.Value = 0.05;
            data.PolygonMode = false;
            data.HoleMode = true;
            data.Name = "Concrete part 1";
            return data;
        }

        XEP_IInternalForceItem CreateForce()
        {
            XEP_IInternalForceItem item = _resolverXEP_IInternalForceItem.Resolve();
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = false;
            item.Name = "New force";
            return item;
        }

        ObservableCollection<XEP_IInternalForceItem> GetInternalForces()
        {
            ObservableCollection<XEP_IInternalForceItem> collection = new ObservableCollection<XEP_IInternalForceItem>();
            XEP_IInternalForceItem item = _resolverXEP_IInternalForceItem.Resolve();
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = true;
            item.Name = "RS2-C01.1-1";
            item.N.Value = 150000.0;
            item.Vy.Value = 65000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 113000;
            item.My.Value = 32000;
            item.Mz.Value = 68000;
            collection.Add(item);
            item = _resolverXEP_IInternalForceItem.Resolve();
            item.Name = "RS2-C02.2-4";
            item.Type = eEP_ForceItemType.eULS;
            item.UsedInCheck = false;
            item.N.Value = 178000.0;
            item.Vy.Value = 52000.0;
            item.Vz.Value = 0.0;
            item.Mx.Value = 98000;
            item.My.Value = 42000;
            item.Mz.Value = 75000;
            collection.Add(item);
            return collection;
        }
        #endregion
    }
}
