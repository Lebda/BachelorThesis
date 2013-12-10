using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.DataCache
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
            var xmlItems = xmlElement.Elements(ns + UnityContainerExtensions.Resolve<XEP_IOneMemberData>(_data.Container).XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IOneMemberData item = UnityContainerExtensions.Resolve<XEP_IOneMemberData>(_data.Container);
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    _data.SaveOneMemberData(item);
                }
            }
        }
    }

    public class XEP_Structure : XEP_IStructure
    {
        readonly IUnityContainer _container = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        Dictionary<Guid, XEP_IOneMemberData> _memberData = new Dictionary<Guid, XEP_IOneMemberData>();
        string _name = "";
        public XEP_Structure(IUnityContainer container)
        {
            _container = Exceptions.CheckNull(container);
            _manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            _xmlWorker = new XEP_StructurXml(this);
        }
        #region XEP_IStructure
        public Dictionary<Guid, XEP_IOneMemberData> MemberData
        {
            get { return _memberData; }
            set { _memberData = value; }
        }
        public IUnityContainer Container
        {
            get { return _container; }
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
