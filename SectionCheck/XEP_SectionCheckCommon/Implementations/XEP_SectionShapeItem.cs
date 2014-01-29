using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
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
            xmlElement.Add(new XAttribute(ns + "Type", (int)_data.Type));
            xmlElement.Add(new XAttribute(ns + "Y", _data.Point.X));
            xmlElement.Add(new XAttribute(ns + "Z", _data.Point.Y));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Type = (eEP_CssShapePointType)(int)xmlElement.Attribute(ns + "Type");
            double posX = (double)xmlElement.Attribute(ns + "Y");
            double posY = (double)xmlElement.Attribute(ns + "Z");
            _data.Point = new Point(posX, posY);
        }
        #endregion
    }

    [Serializable]
    public class XEP_SectionShapeItem : ObservableObject, XEP_ISectionShapeItem
    {
        Point _point = new Point(0.0, 0.0);
        eEP_CssShapePointType _type = eEP_CssShapePointType.eOuter;
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        string _name = "ShapePoint";

        public XEP_SectionShapeItem(XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_SectionShapeItemXml(this);
        }
        #region XEP_ISectionShapeItem Members
        public const string PointPropertyName = "Point";
        public Point Point
        {
            get
            {
                return _point;
            }
            set
            {
                if (_point == value)
                {
                    return;
                }
                _point = value;
                RaisePropertyChanged(PointPropertyName);
            }
        }
        public eEP_CssShapePointType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
