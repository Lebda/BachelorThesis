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
using System.Collections.ObjectModel;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_MaterialLibraryXml : XEP_XmlWorkerImpl
    {
        readonly XEP_IMaterialLibrary _data = null;
        public XEP_MaterialLibraryXml(XEP_IMaterialLibrary data)
        {
            _data = data;
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
            foreach (var item in _data.MaterialDataConcrete)
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
            var xmlItems = xmlElement.Elements(ns + _data.ResolverMatConcrete.Resolve().XmlWorker.GetXmlElementName());
            if (xmlItems != null && xmlItems.Count() > 0)
            {
                for (int counter = 0; counter < xmlItems.Count(); ++counter)
                {
                    XElement xmlItem = Exceptions.CheckNull<XElement>(xmlItems.ElementAt(counter), "Invalid XML file");
                    XEP_IMaterialDataConcrete item = _data.ResolverMatConcrete.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlItem);
                    _data.SaveOneMaterialDataConcrete(item);
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

    public class XEP_MaterialLibrary : XEP_ObservableObject, XEP_IMaterialLibrary
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Material library";
        ObservableCollection<XEP_IMaterialDataConcrete> _materialDataConcrete = new ObservableCollection<XEP_IMaterialDataConcrete>();
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolverMatConcrete;

        public XEP_MaterialLibrary(XEP_IQuantityManager manager, XEP_IResolver<XEP_IMaterialDataConcrete> resolverMatConcrete)
        {
            _manager = manager;
            _xmlWorker = new XEP_MaterialLibraryXml(this);
            _resolverMatConcrete = resolverMatConcrete;
        }

        public XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete
        {
            get { return _resolverMatConcrete; }
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
        #region XEP_IMaterialLibrary Members
        public static readonly string MaterialDataConcretePropertyName = "MaterialDataConcrete";
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