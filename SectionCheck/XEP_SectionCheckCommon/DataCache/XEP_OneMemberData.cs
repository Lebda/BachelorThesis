using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_OneMemberDataXml : XEP_XmlWorkerImpl
    {
        public XEP_OneMemberDataXml(XEP_IOneMemberData data) : base (data)
        {
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
            XEP_IOneMemberData customer = GetXmlCustomer<XEP_IOneMemberData>();
            foreach (var item in customer.SectionsData)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_OneMemberData customer = GetXmlCustomer<XEP_OneMemberData>();
            var xmlItems = xmlElement.Elements(ns + customer.Resolver.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IOneSectionData item = customer.Resolver.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    customer.SaveOneSectionData(item);
                }
            }
        }
        #endregion
    }

    public class XEP_OneMemberData : XEP_ObservableObject, XEP_IOneMemberData
    {
        readonly XEP_IResolver<XEP_IOneSectionData> _resolver = null;
        ObservableCollection<XEP_IOneSectionData> _sectionsData = new ObservableCollection<XEP_IOneSectionData>();
        string _name = "Member data";

        public XEP_OneMemberData(XEP_IResolver<XEP_IOneSectionData> resolver)
        {
            _resolver = resolver;
            _xmlWorker = new XEP_OneMemberDataXml(this);
            Intergrity(null);
        }

        #region XEP_IDataCacheObjectBase Members
        public void Intergrity(string propertyCallerName)
        {

        }
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        XEP_IXmlWorker _xmlWorker = null;
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
        #endregion

        #region XEP_IOneMemberData Members
        public XEP_IResolver<XEP_IOneSectionData> Resolver
        {
            get { return _resolver; }
        }
        public static readonly string SectionsDataPropertyName = "SectionsData";
        public ObservableCollection<XEP_IOneSectionData> SectionsData
        {
            get { return _sectionsData; }
            set { SetMember<ObservableCollection<XEP_IOneSectionData>>(ref value, ref _sectionsData, (_sectionsData == value), SectionsDataPropertyName); }
        }
        public XEP_IOneSectionData GetOneSectionData(Guid guid)
        {
            return GetOneData<XEP_IOneSectionData>(_sectionsData, guid);
        }
        public eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData)
        {
            return SaveOneData<XEP_IOneSectionData>(_sectionsData, sectionData);
        }
        public eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData)
        {
            return RemoveOneData<XEP_IOneSectionData>(_sectionsData, sectionData);
        }
        #endregion
    }
}
