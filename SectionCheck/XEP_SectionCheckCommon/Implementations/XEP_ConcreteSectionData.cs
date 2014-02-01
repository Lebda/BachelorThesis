using System;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
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
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
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
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
        }
        #endregion
    }

    [Serializable]
    public class XEP_ConcreteSectionData : XEP_ObservableObject, XEP_IConcreteSectionData
    {
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = "Concrete section data";
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
    }
}
