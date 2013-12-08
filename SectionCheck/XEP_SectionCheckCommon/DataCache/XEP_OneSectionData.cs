using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_OneSectionDataXml : XEP_XmlWorkerImpl
    {
        XEP_OneSectionData _data = null;
        public XEP_OneSectionDataXml(XEP_OneSectionData data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        protected override string GetXmlElementName()
        {
            return "XEP_OneSectionData";
        }
        protected override string GetXmlElementComment()
        {
            return "All data connected to one section are hold by this element";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.InternalForces)
            {
                xmlElement.Add(item.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Guid", _data.Id));
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
        }
        #endregion
    }

    [Serializable]
    public class XEP_OneSectionData : XEP_IOneSectionData
    {
        XEP_XmlWorkerImpl _xmlWorker = null;
        public XEP_OneSectionData()
        {
            _xmlWorker = new XEP_OneSectionDataXml(this);
        }
        readonly Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
        }

        #region XEP_IOneSectionData
        private string _name = String.Empty;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        //
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
        {
            get { return _internalForces; }
            set { _internalForces = value; }
        }
        #endregion

        #region XEP_IXmlWorker Members
        XElement XEP_IXmlWorker.GetXmlElement()
        {
            return _xmlWorker.GetXmlElement();
        }
        void XEP_IXmlWorker.LoadFromXmlElement(XElement xmlElement)
        {
            _xmlWorker.LoadFromXmlElement(xmlElement);
        }
        #endregion

    }
}
