using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.DataCache;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Infrastructure;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_DataCacheXml : XEP_XmlWorkerImpl
    {
        XEP_DataCache _data = null;
        public XEP_DataCacheXml(XEP_DataCache data)
        {
            _data = data;
        }
        protected override string GetXmlElementName()
        {
            return "XEP_DataCache";
        }
        protected override string GetXmlElementComment()
        {
            return "Data cache holds all data";
        }
        protected override void AddElements(XElement xmlElement)
        {
            xmlElement.Add(_data.Structure.GetXmlElement());
        }
    }
    public class XEP_DataCache : XEP_IDataCache
    {
        XEP_XmlWorkerImpl _xmlWorker = null;
        XEP_IStructure _structure = null;
        public XEP_DataCache()
        {
            _xmlWorker = new XEP_DataCacheXml(this);
        }
        public XEP_IStructure Structure
        {
            get { return _structure; }
            set { _structure = value; }
        }

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
