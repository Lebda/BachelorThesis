using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_StructurXml : XEP_XmlWorkerImpl
    {
        readonly XEP_Structure _data = null;
        public XEP_StructurXml(XEP_Structure data)
        {
            _data = data;
        }
        public override string GetXmlElementName()
        {
            return "XEP_Structure";
        }
        protected override string GetXmlElementComment()
        {
            return "Structure holds information about geometry";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.GetMemberData().Values)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlItems = xmlElement.Elements(ns + _data.Resolver.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IOneMemberData item = _data.Resolver.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    _data.SaveOneMemberData(item);
                }
            }
        }
    }

    public class XEP_Structure : XEP_IStructure
    {
        readonly XEP_IResolver<XEP_IOneMemberData> _resolver = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        Dictionary<Guid, XEP_IOneMemberData> _memberData = new Dictionary<Guid, XEP_IOneMemberData>();
        string _name = "";
        public XEP_Structure(XEP_IResolver<XEP_IOneMemberData> resolver, XEP_IQuantityManager manager)
        {
            _resolver = resolver;
            _manager = manager;
            _xmlWorker = new XEP_StructurXml(this);
        }
        #region XEP_IStructure
        public XEP_IResolver<XEP_IOneMemberData> Resolver
        {
            get
            {
                return this._resolver;
            }
        }
        public Dictionary<Guid, XEP_IOneMemberData> MemberData
        {
            get { return _memberData; }
            set { _memberData = value; }
        }

        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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

    }
}
