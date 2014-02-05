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
    class XEP_StructurXml : XEP_XmlWorkerImpl
    {
        public XEP_StructurXml(XEP_IStructure data) : base(data)
        {
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
            XEP_IStructure customer = GetXmlCustomer<XEP_IStructure>();
            foreach (var item in customer.MemberData)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_Structure customer = GetXmlCustomer<XEP_Structure>();
            var xmlItems = xmlElement.Elements(ns + customer.Resolver.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IOneMemberData item = customer.Resolver.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    customer.SaveOneMemberData(item);
                }
            }
        }
    }

    public class XEP_Structure : XEP_ObservableObject, XEP_IStructure
    {
        readonly XEP_IResolver<XEP_IOneMemberData> _resolver = null;
        ObservableCollection<XEP_IOneMemberData> _memberData = new ObservableCollection<XEP_IOneMemberData>();
        string _name = "Structure";
        public XEP_Structure(XEP_IResolver<XEP_IOneMemberData> resolver, XEP_IQuantityManager manager)
        {
            _resolver = resolver;
            _xmlWorker = new XEP_StructurXml(this);
            Intergrity(null);
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

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        public void Intergrity(string propertyCallerName)
        {

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
    }
}
