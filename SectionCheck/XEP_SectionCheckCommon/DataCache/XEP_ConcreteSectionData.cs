using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_ConcreteSectionDataXml : XEP_XmlWorkerImpl
    {
        public XEP_ConcreteSectionDataXml(XEP_IConcreteSectionData data) : base(data)
        {
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_ConcreteSectionData";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents concrete data for one concrete shape.";
        }
        protected override void AddElements(XElement xmlElement)
        {
            XEP_IConcreteSectionData customer = GetXmlCustomer<XEP_IConcreteSectionData>();
            xmlElement.Add(customer.SectionShape.XmlWorker.GetXmlElement());
            xmlElement.Add(customer.MaterialData.XmlWorker.GetXmlElement());
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_IConcreteSectionData customer = GetXmlCustomer<XEP_IConcreteSectionData>();
            customer.SectionShape.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + customer.SectionShape.XmlWorker.GetXmlElementName()));
            customer.MaterialData.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + customer.MaterialData.XmlWorker.GetXmlElementName()));
        }
        #endregion
    }

    public class XEP_ConcreteSectionData : XEP_ObservableObject, XEP_IConcreteSectionData
    {
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Concrete section data";
        XEP_ISectionShape _sectionShape = null;
        XEP_IMaterialDataConcrete _materialData = null;

        // ctors
        public XEP_ConcreteSectionData(XEP_IResolver<XEP_ISectionShape> resolverShape,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolverMaterialData)
        {
            _xmlWorker = new XEP_ConcreteSectionDataXml(this);
            _sectionShape = resolverShape.Resolve();
            _materialData = resolverMaterialData.Resolve();
            Intergrity(null);

        }
        // Properties
        public static readonly string MaterialDataPropertyName = "MaterialData";
        public XEP_IMaterialDataConcrete MaterialData
        {
            get { return _materialData; }
            set { SetMember<XEP_IMaterialDataConcrete>(ref value, ref _materialData, (_materialData == value), MaterialDataPropertyName); }
        }
        public static readonly string SectionShapePropertyName = "SectionShape";
        public XEP_ISectionShape SectionShape
        {
            get { return _sectionShape; }
            set { SetMember<XEP_ISectionShape>(ref value, ref _sectionShape, (_sectionShape == value), SectionShapePropertyName); }
        }
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
