using System;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    public class XEP_DataCacheMaker
    {
        readonly XEP_IResolver<XEP_IDataCache> _resolverDataCache = null;
        readonly XEP_IResolver<XEP_IStructure> _resolverStructure = null;
        readonly XEP_IResolver<XEP_IOneMemberData> _resolverMemberData = null;
        readonly XEP_IResolver<XEP_IOneSectionData> _resolverSectionData = null;
        readonly XEP_IResolver<XEP_ISectionShape> _resolverShape = null;
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolverShapeItem = null;
        readonly XEP_IResolver<XEP_InternalForceItem> _resolverForce = null;
        XEP_IQuantityManager _manager = null;

        public XEP_DataCacheMaker(
            XEP_IQuantityManager manager,
            XEP_IResolver<XEP_IDataCache> resolverDataCache,
            XEP_IResolver<XEP_IStructure> resolverStructure,
            XEP_IResolver<XEP_IOneMemberData> resolverMemberData,
            XEP_IResolver<XEP_IOneSectionData> resolverSectionData,
            XEP_IResolver<XEP_ISectionShape> resolverShape,
            XEP_IResolver<XEP_ISectionShapeItem> resolverShapeItem,
            XEP_IResolver<XEP_InternalForceItem> resolverForce
            )
        {
            _manager = manager;
            _resolverDataCache = resolverDataCache;
            _resolverStructure = resolverStructure;
            _resolverMemberData = resolverMemberData;
            _resolverSectionData = resolverSectionData;
            _resolverShape =resolverShape;
            _resolverShapeItem = resolverShapeItem;
            _resolverForce = resolverForce;
        }

        XEP_IDataCache Create()
        {
            return new XEP_DataCache(_resolverStructure, _manager);
        }

    }
}
