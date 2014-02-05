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
    class XEP_MaterialLibraryXml : XEP_XmlWorkerImpl
    {
        public XEP_MaterialLibraryXml(XEP_IMaterialLibrary data) : base(data)
        {
        }
        public override string GetXmlElementName()
        {
            return "XEP_MaterialLibrary";
        }
        protected override string GetXmlElementComment()
        {
            return "Objects holds material database.";
        }
        protected override void AddElements(XElement xmlElement)
        {
            XEP_IMaterialLibrary customer = GetXmlCustomer<XEP_IMaterialLibrary>();
            foreach (var item in customer.MaterialDataConcrete)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_MaterialLibrary customer = GetXmlCustomer<XEP_MaterialLibrary>();
            var xmlItems = xmlElement.Elements(ns + customer.ResolverMatConcrete.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IMaterialDataConcrete item = customer.ResolverMatConcrete.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    customer.SaveOneMaterialDataConcrete(item);
                }
            }
        }
    }

    public class XEP_MaterialLibrary : XEP_ObservableObject, XEP_IMaterialLibrary
    {
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolverMatConcrete;
        public XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete
        {
            get { return _resolverMatConcrete; }
        }

        public XEP_MaterialLibrary(XEP_IResolver<XEP_IMaterialDataConcrete> resolverMatConcrete)
        {
            _xmlWorker = new XEP_MaterialLibraryXml(this);
            _resolverMatConcrete = resolverMatConcrete;
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
        string _name = "Material library";
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

        #region XEP_IMaterialLibrary Members
        public static readonly string MaterialDataConcretePropertyName = "MaterialDataConcrete";
        ObservableCollection<XEP_IMaterialDataConcrete> _materialDataConcrete = new ObservableCollection<XEP_IMaterialDataConcrete>();
        public ObservableCollection<XEP_IMaterialDataConcrete> MaterialDataConcrete
        {
            get { return _materialDataConcrete; }
            set { SetMember<ObservableCollection<XEP_IMaterialDataConcrete>>(ref value, ref _materialDataConcrete, (_materialDataConcrete == value), MaterialDataConcretePropertyName); }
        }
        public XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(string matName)
        {
            return GetOneData<XEP_IMaterialDataConcrete>(_materialDataConcrete, matName);
        }
        public XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(Guid id)
        {
            return GetOneData<XEP_IMaterialDataConcrete>(_materialDataConcrete, id);
        }
        public eDataCacheServiceOperation SaveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData)
        {
            return SaveOneData<XEP_IMaterialDataConcrete>(_materialDataConcrete, matData);
        }
        public eDataCacheServiceOperation RemoveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData)
        {
            return RemoveOneData<XEP_IMaterialDataConcrete>(_materialDataConcrete, matData);
        }
        #endregion
    }
}