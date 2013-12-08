using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Utility;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_OneMemberDataXml : XEP_XmlWorkerImpl
    {
        XEP_OneMemberData _data = null;
        public XEP_OneMemberDataXml(XEP_OneMemberData data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        protected override string GetXmlElementName()
        {
            return "XEP_OneMemberData";
        }
        protected override string GetXmlElementComment()
        {
            return "All data connected to one member are hold by this element";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.GetSectionsData().Values)
            {
                xmlElement.Add(item.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Guid", _data.Id));
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
        }
        #endregion
    }

    [Serializable]
    public class XEP_OneMemberData : XEP_IOneMemberData
    {
        XEP_XmlWorkerImpl _xmlWorker = null;
        Dictionary<Guid, XEP_IOneSectionData> _sectionData = new Dictionary<Guid, XEP_IOneSectionData>();
        readonly Guid _guid = Guid.NewGuid();
        string _name = String.Empty;
        public XEP_OneMemberData()
        {
            _xmlWorker = new XEP_OneMemberDataXml(this);
        }
        #region XEP_IOneMemberData Members
        public Guid Id
        {
            get { return _guid; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public XEP_IOneSectionData GetOneSectionData(Guid guid)
        {
            return Common.GetDataDictionary<Guid, XEP_IOneSectionData>(guid, _sectionData);
        }
        public Dictionary<Guid, XEP_IOneSectionData> GetSectionsData()
        {
            return _sectionData;
        }
        public eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionData.ContainsKey(sectionData.Id))
            {
                _sectionData.Remove(sectionData.Id);
            }
            _sectionData.Add(sectionData.Id, sectionData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionData.ContainsKey(sectionData.Id))
            {
                _sectionData.Remove(sectionData.Id);
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
