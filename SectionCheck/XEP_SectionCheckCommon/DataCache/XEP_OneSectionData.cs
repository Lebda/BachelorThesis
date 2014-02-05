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
    class XEP_OneSectionDataXml : XEP_XmlWorkerImpl
    {
        public XEP_OneSectionDataXml(XEP_IOneSectionData data) : base(data)
        {
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_OneSectionData";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents on section on member";
        }
        protected override void AddElements(XElement xmlElement)
        {
            XEP_IOneSectionData customer = GetXmlCustomer<XEP_IOneSectionData>();
            foreach (var item in customer.InternalForces)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
            xmlElement.Add(customer.ConcreteSectionData.XmlWorker.GetXmlElement());
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_OneSectionData customer = GetXmlCustomer<XEP_OneSectionData>();
            var xmlForces = xmlElement.Elements(ns + customer.ResolverForce.Resolve().XmlWorker.GetXmlElementName());
            if (xmlForces != null && xmlForces.Count() > 0)
            {
                for (int counter = 0; counter < xmlForces.Count(); ++counter)
                {
                    XElement xmlForce = Exceptions.CheckNull<XElement>(xmlForces.ElementAt(counter), "Invalid XML file");
                    XEP_IInternalForceItem item = customer.ResolverForce.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlForce);
                    customer.InternalForces.Add(item);
                }
            }
            customer.ConcreteSectionData.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + customer.ConcreteSectionData.XmlWorker.GetXmlElementName()));
        }
        #endregion
    }

    public class XEP_OneSectionData : XEP_ObservableObject, XEP_IOneSectionData
    {
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Section data";
        ObservableCollection<XEP_IInternalForceItem> _internalForces = new ObservableCollection<XEP_IInternalForceItem>();
        XEP_IConcreteSectionData _concreteSectionData = null;
        // ctors
        public XEP_OneSectionData(XEP_IResolver<XEP_IInternalForceItem> resolverForce, XEP_IResolver<XEP_IConcreteSectionData> resolverConcrete)
        {
            _resolverForce = resolverForce;
            _xmlWorker = new XEP_OneSectionDataXml(this);
            _concreteSectionData = resolverConcrete.Resolve();
            Intergrity(null);
        }
        public XEP_IResolver<XEP_IInternalForceItem> ResolverForce
        {
            get
            {
                return this._resolverForce;
            }
        }
        #region XEP_IOneSectionData
        public static readonly string ConcreteSectionDataPropertyName = "ConcreteSectionData";
        public XEP_IConcreteSectionData ConcreteSectionData
        {
            get { return _concreteSectionData; }
            set { SetMember<XEP_IConcreteSectionData>(ref value, ref _concreteSectionData, (_concreteSectionData == value), ConcreteSectionDataPropertyName); }
        }
        public static readonly string InternalForcesPropertyName = "InternalForces";
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
        {
            get { return _internalForces; }
            set { SetMember<ObservableCollection<XEP_IInternalForceItem>>(ref value, ref _internalForces, (_internalForces == value), InternalForcesPropertyName); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public void Intergrity(string propertyCallerName)
        {

        }
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
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
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        #endregion
    }
}
