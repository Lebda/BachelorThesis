﻿using System;
using System.Linq;
using XEP_SectionCheckCommon.DataCache;
using Microsoft.Practices.Unity;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows;
using XEP_Prism.Infrastructure;
using SectionCheck.Services;

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

        public XEP_DataCacheServiceMock(
            XEP_IResolver<XEP_IOneSectionData> resolverXEP_IOneSectionData,
            XEP_IResolver<XEP_IOneMemberData> resolverXEP_IOneMemberData,
            XEP_IResolver<XEP_IStructure> resolverXEP_IStructure,
            XEP_IResolver<XEP_IConcreteSectionData> resolverXEP_IConcreteSectionData,
            XEP_IResolver<XEP_ISectionShapeItem> resolverXEP_ISectionShapeItem,
            XEP_IResolver<XEP_ISectionShape> resolverXEP_ISectionShape,
            XEP_IResolver<XEP_IInternalForceItem> resolverXEP_IInternalForceItem,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolverXEP_IMaterialDataConcrete)
        {
            _resolverXEP_IOneSectionData = resolverXEP_IOneSectionData;
            _resolverXEP_IOneMemberData = resolverXEP_IOneMemberData;
            _resolverXEP_IStructure = resolverXEP_IStructure;
            _resolverXEP_IConcreteSectionData = resolverXEP_IConcreteSectionData;
            _resolverXEP_ISectionShapeItem = resolverXEP_ISectionShapeItem;
            _resolverXEP_ISectionShape = resolverXEP_ISectionShape;
            _resolverXEP_IInternalForceItem = resolverXEP_IInternalForceItem;
            _resolverXEP_IMaterialDataConcrete = resolverXEP_IMaterialDataConcrete;
        }

        #region XEP_IDataCacheService Members
        public override eDataCacheServiceOperation Load(XEP_IDataCache dataCache)
        {
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
        private XEP_IConcreteSectionData GetConcreteSectionData()
        {
            XEP_IConcreteSectionData data = _resolverXEP_IConcreteSectionData.Resolve();
            data.Name = "Concrete section data in section 1";
            data.SectionShape = GetSectionShape();
            data.MaterialData = GetMaterialDataConcrete();
            return data;
        }

        private XEP_IMaterialDataConcrete GetMaterialDataConcrete()
        {
            XEP_IMaterialDataConcrete item = _resolverXEP_IMaterialDataConcrete.Resolve();
            item.Name = "C25/30";
            item.Fck = 25.0;
            item.FckCube = 30.0;
            item.EpsC1 = 2.1 * 1e-3;
            item.EpsCu1 = 3.5 * 1e-3;
            item.EpsC2 = 2.0 * 1e-3;
            item.EpsCu2 = 3.5 * 1e-3;
            item.EpsC3 = 1.75 * 1e-3;
            item.EpsCu3 = 3.5 * 1e-3;
            item.N = 2.0;
            return item;
        }

        private XEP_ISectionShape GetSectionShape()
        {
            ObservableCollection<XEP_ISectionShapeItem> shape = new ObservableCollection<XEP_ISectionShapeItem>();
            XEP_ISectionShapeItem item = _resolverXEP_ISectionShapeItem.Resolve();
            item.Point = new Point(0.15, -0.25);
            shape.Add(item);
            item = _resolverXEP_ISectionShapeItem.Resolve();
            item.Point = new Point(0.15, 0.25);
            shape.Add(item);
            item = _resolverXEP_ISectionShapeItem.Resolve();
            item.Point = new Point(-0.15, 0.25);
            shape.Add(item);
            item = _resolverXEP_ISectionShapeItem.Resolve();
            item.Point = new Point(-0.15, -0.25);
            shape.Add(item);
            item = _resolverXEP_ISectionShapeItem.Resolve();
            item.Point = new Point(0.15, -0.25);
            shape.Add(item);
            XEP_ISectionShape newSectionShape = _resolverXEP_ISectionShape.Resolve();
            newSectionShape.ShapeOuter = shape;
            return newSectionShape;
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
