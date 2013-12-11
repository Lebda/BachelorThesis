using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Media;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_CommonLibrary.Utility;
using Microsoft.Practices.Unity;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_OneSectionDataXml : XEP_XmlWorkerImpl
    {
        readonly XEP_OneSectionData _data = null;
        public XEP_OneSectionDataXml(XEP_OneSectionData data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_OneSectionData";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents on section on member";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.InternalForces)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
            xmlElement.Add(_data.SectionShape.XmlWorker.GetXmlElement());
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
            var xmlForces = xmlElement.Elements(ns + UnityContainerExtensions.Resolve<XEP_InternalForceItem>(_data.Container).XmlWorker.GetXmlElementName());
            if (xmlForces != null && xmlForces.Count() > 0)
            {
                for (int counter = 0; counter < xmlForces.Count(); ++counter)
                {
                    XElement xmlForce = Exceptions.CheckNull<XElement>(xmlForces.ElementAt(counter), "Invalid XML file");
                    XEP_InternalForceItem item = UnityContainerExtensions.Resolve<XEP_InternalForceItem>(_data.Container);
                    item.XmlWorker.LoadFromXmlElement(xmlForce);
                    _data.InternalForces.Add(item);
                }
            }
            _data.SectionShape.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.SectionShape.XmlWorker.GetXmlElementName()));
        }
        protected override void LoadAtributes( XElement xmlElement )
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.Id = (Guid)xmlElement.Attribute(ns + "Guid");
        }
        #endregion
    }

    [Serializable]
    public class XEP_OneSectionData : XEP_IOneSectionData
    {
        readonly IUnityContainer _container = null;
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        Guid _guid = Guid.NewGuid();
        string _name = String.Empty;
        ObservableCollection<XEP_IInternalForceItem> _internalForces = new ObservableCollection<XEP_IInternalForceItem>();
        XEP_ISectionShape _sectionShape = null;
        public XEP_OneSectionData(IUnityContainer container)
        {
            _container = Exceptions.CheckNull(container);
            _manager = UnityContainerExtensions.Resolve<XEP_IQuantityManager>(_container);
            _xmlWorker = new XEP_OneSectionDataXml(this);
            _sectionShape = UnityContainerExtensions.Resolve<XEP_ISectionShape>(_container);
        }
        public IUnityContainer Container
        {
            get { return _container; }
        }
        #region XEP_IOneSectionData
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
        //
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
        {
            get { return _internalForces; }
            set { _internalForces = value; }
        }
        #endregion
    }
}
