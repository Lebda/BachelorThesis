using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_DataCacheXml : XEP_XmlWorkerImpl
    {
        readonly XEP_DataCache _data = null;
        public XEP_DataCacheXml(XEP_DataCache data)
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
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Structure.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.Structure.XmlWorker.GetXmlElementName()));
        }
    }
    public class XEP_DataCache : XEP_IDataCache
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = String.Empty;
        XEP_IStructure _structure = null;

        public XEP_DataCache(XEP_IResolver<XEP_IStructure> resolver, XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_DataCacheXml(this);
            _structure = resolver.Resolve();
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
        public XEP_IStructure Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }
    }
}
