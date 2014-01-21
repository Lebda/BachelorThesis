using System;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_ConcreteSectionDataXml : XEP_XmlWorkerImpl
    {
        readonly XEP_ConcreteSectionData _data = null;
        public XEP_ConcreteSectionDataXml(XEP_ConcreteSectionData data)
        {
            _data = data;
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
            xmlElement.Add(_data.SectionShape.XmlWorker.GetXmlElement());
            xmlElement.Add(_data.MaterialData.XmlWorker.GetXmlElement());
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Guid", _data.Id));
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.SectionShape.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.SectionShape.XmlWorker.GetXmlElementName()));
            _data.MaterialData.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.MaterialData.XmlWorker.GetXmlElementName()));
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.Id = (Guid)xmlElement.Attribute(ns + "Guid");
        }
        #endregion
    }

    [Serializable]
    public class XEP_ConcreteSectionData : XEP_IConcreteSectionData
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        Guid _guid = Guid.NewGuid();
        string _name = String.Empty;
        XEP_ISectionShape _sectionShape = null;
        XEP_IMaterialDataConcrete _materialData = null;

        // ctors
        public XEP_ConcreteSectionData(XEP_IResolver<XEP_ISectionShape> resolverShape,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolverMaterialData,
            XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_ConcreteSectionDataXml(this);
            _sectionShape = resolverShape.Resolve();
            _materialData = resolverMaterialData.Resolve();
        }
        // Properties
        public XEP_IMaterialDataConcrete MaterialData
        {
            get { return _materialData; }
            set { _materialData = value; }
        }
        public XEP_ISectionShape SectionShape
        {
            get { return _sectionShape; }
            set { _sectionShape = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public Guid Id
        {
            get { return _guid; }
            set { _guid = value; }
        }
    }
}
