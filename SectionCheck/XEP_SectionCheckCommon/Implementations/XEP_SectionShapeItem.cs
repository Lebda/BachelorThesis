using System;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
    class XEP_SectionShapeItemXml : XEP_XmlWorkerImpl
    {
        readonly XEP_SectionShapeItem _data = null;
        public XEP_SectionShapeItemXml(XEP_SectionShapeItem data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SectionShapeItem";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_SectionShapeItem.TypePropertyName, (int)_data.Type));
            xmlElement.Add(new XAttribute(ns + XEP_SectionShapeItem.YPropertyName, _data.Y.Value));
            xmlElement.Add(new XAttribute(ns + XEP_SectionShapeItem.ZPropertyName, _data.Z.Value));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Type = (eEP_CssShapePointType)(int)xmlElement.Attribute(ns + XEP_SectionShapeItem.TypePropertyName);
            _data.Y.Value = (double)xmlElement.Attribute(ns + XEP_SectionShapeItem.YPropertyName);
            _data.Z.Value = (double)xmlElement.Attribute(ns + XEP_SectionShapeItem.ZPropertyName);
        }
        #endregion
    }

    [Serializable]
    public class XEP_SectionShapeItem : XEP_ObservableObject, XEP_ISectionShapeItem
    {
        eEP_CssShapePointType _type = eEP_CssShapePointType.eOuter;
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        string _name = "ShapePoint";

        public XEP_SectionShapeItem(XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_SectionShapeItemXml(this);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, YPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, ZPropertyName);

        }
        #region XEP_ISectionShapeItem Members
        public static readonly string YPropertyName = "Y";
        public XEP_IQuantity Y
        {
            get { return GetOneQuantity(YPropertyName); }
            set { SetItem(ref value, YPropertyName); }
        }
        public static readonly string ZPropertyName = "Z";
        public XEP_IQuantity Z
        {
            get { return GetOneQuantity(ZPropertyName); }
            set { SetItem(ref value, ZPropertyName); }
        }
        public static readonly string TypePropertyName = "Type";
        public eEP_CssShapePointType Type
        {
            get { return _type; }
            set { SetMember<eEP_CssShapePointType>(ref value, ref _type, (_type == value), TypePropertyName); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
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
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion
    }
}
