using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_DataCacheXml : XEP_XmlWorkerImpl
    {
        public XEP_DataCacheXml(XEP_IDataCache data) : base(data)
        {
        }
        public override string GetXmlElementName()
        {
            return "XEP_DataCache";
        }
        protected override string GetXmlElementComment()
        {
            return "Data cache holds all data";
        }
        protected override void AddElements(XElement xmlElement)
        {
            XEP_IDataCache customer = GetXmlCustomer<XEP_IDataCache>();
            xmlElement.Add(customer.Structure.XmlWorker.GetXmlElement());
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_IDataCache customer = GetXmlCustomer<XEP_IDataCache>();
            customer.Structure.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + customer.Structure.XmlWorker.GetXmlElementName()));
        }
    }

    public class XEP_DataCache : XEP_ObservableObject, XEP_IDataCache
    {
        XEP_IStructure _structure = null;
        XEP_IMaterialLibrary _materialLibrary = null;
        XEP_ISetupParameters _setupParameters = null;

        public XEP_DataCache(XEP_IResolver<XEP_IStructure> resolver, XEP_IResolver<XEP_IMaterialLibrary> resolverMatLib,
            XEP_IResolver<XEP_ISetupParameters> resolverSetup)
        {
            _xmlWorker = new XEP_DataCacheXml(this);
            _structure = resolver.Resolve();
            _materialLibrary = resolverMatLib.Resolve();
            _setupParameters = resolverSetup.Resolve();
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
        string _name = "Data cache";
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

        public static readonly string StructurePropertyName = "Structure";
        public XEP_IStructure Structure
        {
            get { return _structure; }
            set { SetMember<XEP_IStructure>(ref value, ref _structure, (_structure == value), StructurePropertyName); }
        }
        public static readonly string MaterialLibraryPropertyName = "MaterialLibrary";
        public XEP_IMaterialLibrary MaterialLibrary
        {
            get { return _materialLibrary; }
            set { SetMember<XEP_IMaterialLibrary>(ref value, ref _materialLibrary, (_materialLibrary == value), MaterialLibraryPropertyName); }
        }
        public static readonly string SetupParametersPropertyName = "SetupParameters";
        public XEP_ISetupParameters SetupParameters
        {
            get { return _setupParameters; }
            set { SetMember<XEP_ISetupParameters>(ref value, ref _setupParameters, (_setupParameters == value), SetupParametersPropertyName); }
        }
    }
}
