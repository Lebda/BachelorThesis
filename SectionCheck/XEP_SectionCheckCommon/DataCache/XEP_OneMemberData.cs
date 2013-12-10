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
using Microsoft.Practices.Unity;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_OneMemberDataXml : XEP_XmlWorkerImpl
    {
        readonly XEP_OneMemberData _data = null;
        public XEP_OneMemberDataXml(XEP_OneMemberData data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_OneMemberData";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents one member in construction";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.SectionsData.Values)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Guid", _data.Id));
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
        }
        protected override void LoadElements( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlItems = xmlElement.Elements(ns + UnityContainerExtensions.Resolve<XEP_IOneSectionData>(_data.Container).XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IOneSectionData item = UnityContainerExtensions.Resolve<XEP_IOneSectionData>(_data.Container);
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    _data.SaveOneSectionData(item);
                }
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.Id = (Guid)xmlElement.Attribute(ns + "Guid");
        }
        #endregion
    }

    [Serializable]
    public class XEP_OneMemberData : XEP_IOneMemberData
    {
        readonly IUnityContainer _container = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        Dictionary<Guid, XEP_IOneSectionData> _sectionsData = new Dictionary<Guid, XEP_IOneSectionData>();
        Guid _guid = Guid.NewGuid();
        string _name = String.Empty;

        public XEP_OneMemberData(IUnityContainer container)
        {
            _container = Exceptions.CheckNull(container);
            _manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            _xmlWorker = new XEP_OneMemberDataXml(this);
        }
        #region XEP_IOneMemberData Members
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
        public Guid Id
        {
            get { return _guid; }
            set { _guid = value; }
        }
        public XEP_IOneSectionData GetOneSectionData(Guid guid)
        {
            return Common.GetDataDictionary<Guid, XEP_IOneSectionData>(guid, _sectionsData);
        }
        public Dictionary<Guid, XEP_IOneSectionData> SectionsData
        {
            get { return _sectionsData; }
            set { _sectionsData = value; }
        }
        public eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionsData.ContainsKey(sectionData.Id))
            {
                _sectionsData.Remove(sectionData.Id);
            }
            _sectionsData.Add(sectionData.Id, sectionData);
            return eDataCacheServiceOperation.eSuccess;
        }
        public eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData)
        {
            Exceptions.CheckNull(sectionData);
            if (_sectionsData.ContainsKey(sectionData.Id))
            {
                _sectionsData.Remove(sectionData.Id);
                return eDataCacheServiceOperation.eSuccess;
            }
            return eDataCacheServiceOperation.eNotFound;
        }
        #endregion
    }
}
