using System;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;
using Microsoft.Practices.Unity;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_SectionShapeXml : XEP_XmlWorkerImpl
    {
        readonly XEP_SectionShape _data = null;
        public XEP_SectionShapeXml(XEP_SectionShape data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SectionShape";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents section shape";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.ShapeOuter)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
            foreach (var item in _data.ShapeInner)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlElements = xmlElement.Elements(ns + UnityContainerExtensions.Resolve<XEP_ISectionShapeItem>(_data.Container).XmlWorker.GetXmlElementName());
            if (xmlElements != null && xmlElements.Count() > 0)
            {
                for (int counter = 0; counter < xmlElements.Count(); ++counter)
                {
                    XElement xmlForce = Exceptions.CheckNull<XElement>(xmlElements.ElementAt(counter), "Invalid XML file");
                    XEP_ISectionShapeItem item = UnityContainerExtensions.Resolve<XEP_ISectionShapeItem>(_data.Container);
                    item.XmlWorker.LoadFromXmlElement(xmlForce);
                    if (item.Type == eEP_CssShapePointType.eOuter)
                    {
                        _data.ShapeOuter.Add(item);
                    }
                    else
                    {
                        _data.ShapeInner.Add(item);
                    }
                }
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
        }
        #endregion
    }

    [Serializable]
    public class XEP_SectionShape : ObservableObject, XEP_ISectionShape
    {
        readonly IUnityContainer _container = null;
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        string _name = "Section shape";
        ObservableCollection<XEP_ISectionShapeItem> _shapeOuter = new ObservableCollection<XEP_ISectionShapeItem>();
        ObservableCollection<XEP_ISectionShapeItem> _shapeInner = new ObservableCollection<XEP_ISectionShapeItem>();

        public XEP_SectionShape(IUnityContainer container)
        {
            _container = Exceptions.CheckNull(container);
            _manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            _xmlWorker = new XEP_SectionShapeXml(this);
        }

        #region XEP_ISectionShape Members
        public const string ShapeOuterPropertyName = "ShapeOuter";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeOuter
        {
            get { return _shapeOuter; }
            set
            {
                if (_shapeOuter == value)
                {
                    return;
                }
                _shapeOuter = value;
                RaisePropertyChanged(ShapeOuterPropertyName);
            }
        }
        public const string ShapeInnerPropertyName = "ShapeInner";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeInner
        {
            get { return _shapeInner; }
            set
            {
                if (_shapeInner == value)
                {
                    return;
                }
                _shapeInner = value;
                RaisePropertyChanged(ShapeOuterPropertyName);
            }
        }
        public IUnityContainer Container
        {
            get { return _container; }
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
