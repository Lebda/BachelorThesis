﻿using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_DataCacheXml : XEP_XmlWorkerImpl
    {
        readonly XEP_IDataCache _data = null;
        public XEP_DataCacheXml(XEP_IDataCache data)
        {
            _data = data;
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
            xmlElement.Add(_data.Structure.XmlWorker.GetXmlElement());
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
            _data.Structure.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.Structure.XmlWorker.GetXmlElementName()));
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
        }
    }

    public class XEP_DataCache : XEP_ObservableObject, XEP_IDataCache
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Data cache";
        XEP_IStructure _structure = null;
        XEP_IMaterialLibrary _materialLibrary = null;
        XEP_ISetupParameters _setupParameters = null;

        public XEP_DataCache(XEP_IResolver<XEP_IStructure> resolver, XEP_IResolver<XEP_IMaterialLibrary> resolverMatLib,
            XEP_IResolver<XEP_ISetupParameters> resolverSetup, XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_DataCacheXml(this);
            _structure = resolver.Resolve();
            _materialLibrary = resolverMatLib.Resolve();
            _setupParameters = resolverSetup.Resolve();
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
