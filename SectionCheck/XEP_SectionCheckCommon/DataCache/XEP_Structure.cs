using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_StructurXml : XEP_XmlWorkerImpl
    {
        XEP_Structure _data = null;
        public XEP_StructurXml(XEP_Structure data)
        {
            _data = data;
        }
        protected override string GetXmlElementName()
        {
            return "XEP_Structure";
        }
        protected override string GetXmlElementComment()
        {
            return "All data connected to structure are hold by this element";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.GetMemberData().Values)
            {
                xmlElement.Add(item.GetXmlElement());
            }
        }
    }

    public class XEP_Structure : XEP_IStructure
    {
        XEP_XmlWorkerImpl _xmlWorker = null;
        readonly Dictionary<Guid, XEP_IOneMemberData> _memberData = new Dictionary<Guid, XEP_IOneMemberData>();
        public XEP_Structure()
        {
            _xmlWorker = new XEP_StructurXml(this);
        }
        #region XEP_IStructure
        public void Clear()
        {
            _memberData.Clear();
        }
        public Dictionary<Guid, XEP_IOneMemberData> GetMemberData()
        {
            return _memberData;
        }
        public XEP_IOneMemberData GetOneMemberData(Guid guid)
        {
            return Common.GetDataDictionary<Guid, XEP_IOneMemberData>(guid, _memberData);
        }
        public eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData)
        {
            Exceptions.CheckNull(memberData);
            if (_memberData.ContainsKey(memberData.Id))
            {
                _memberData.Remove(memberData.Id);
            }
            _memberData.Add(memberData.Id, memberData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData)
        {
            Exceptions.CheckNull(memberData);
            if (_memberData.ContainsKey(memberData.Id))
            {
                _memberData.Remove(memberData.Id);
                return eDataCacheServiceOperation.eSuccess;
            }
            return eDataCacheServiceOperation.eNotFound;
        }
        #endregion

        #region XEP_IXmlWorker Members
        XElement XEP_IXmlWorker.GetXmlElement()
        {
            return _xmlWorker.GetXmlElement();
        }
        void XEP_IXmlWorker.LoadFromXmlElement(XElement xmlElement)
        {
            _xmlWorker.LoadFromXmlElement(xmlElement);
        }
        #endregion
    }
}
