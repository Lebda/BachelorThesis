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
using XEP_CommonLibrary.Infrastructure;
using System.Collections.ObjectModel;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
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
            foreach (var item in _data.MemberData)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
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
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
        }
    }

    [Serializable]
    public class XEP_Structure : XEP_ObservableObject, XEP_IStructure
    {
        readonly XEP_IResolver<XEP_IOneMemberData> _resolver = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        ObservableCollection<XEP_IOneMemberData> _memberData = new ObservableCollection<XEP_IOneMemberData>();
        string _name = "Structure";
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
        public static readonly string MemberDataPropertyName = "MemberData";
        public ObservableCollection<XEP_IOneMemberData> MemberData
        {
            get { return _memberData; }
            set { SetMember<ObservableCollection<XEP_IOneMemberData>>(ref value, ref _memberData, (_memberData == value), MemberDataPropertyName); }
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
            set { SetMember<string>(ref value, ref _name, (_name == value), XEP_Constants.NamePropertyName); }
        }
        Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
            set { SetMember<Guid>(ref value, ref _guid, (_guid == value), XEP_Constants.GuidPropertyName); }
        }
        public void Clear()
        {
            _memberData.Clear();
        }
        public XEP_IOneMemberData GetOneMemberData(Guid guid)
        {
            return GetOneData<XEP_IOneMemberData>(_memberData, guid);
        }
        public eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData)
        {
            return SaveOneData<XEP_IOneMemberData>(_memberData, memberData);
        }
        public eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData)
        {
            return RemoveOneData<XEP_IOneMemberData>(_memberData, memberData);
        }
        #endregion
    }
}
