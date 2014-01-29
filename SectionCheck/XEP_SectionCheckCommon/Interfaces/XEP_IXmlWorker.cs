using System;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IXmlWorker
    {
        XElement GetXmlElement();
        void LoadFromXmlElement(XElement xmlElement);
        string GetXmlElementName();
    }

    [Serializable]
    public abstract class XEP_XmlWorkerImpl : XEP_IXmlWorker
    {
        public abstract string GetXmlElementName();
        protected abstract void LoadElements(XElement xmlElement);
        public XElement GetXmlElement()
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XElement xmlElement = new XElement(ns + GetXmlElementName());
            if (!String.IsNullOrEmpty(GetXmlElementComment()))
            {
                XComment xmlComment = new XComment(GetXmlElementComment());
                xmlElement.Add(xmlComment);
            }
            // Attributes
            AddAtributes(xmlElement);
            // Elements
            AddElements(xmlElement);
            return xmlElement;
        }

        public void LoadFromXmlElement(XElement xmlElement)
        {
            if (xmlElement == null)
            {
                throw new ArgumentException(String.Format("{0} can not be created from XElement == null ! :", GetXmlElementName()));
            }
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            if (xmlElement.Name != (ns + GetXmlElementName()))
            {
                throw new ArgumentException(String.Format("{0} can not be created from XElement that is not {0} ", GetXmlElementName()));
            }
            LoadElements(xmlElement);
            LoadAtributes(xmlElement);
        }
  
        protected virtual void LoadAtributes(XElement xmlElement)
        {
            return;
        }

        protected virtual string GetXmlElementComment()
        {
            return "";
        }

        protected virtual void AddAtributes(XElement xmlElement)
        {
            return;
        }

        protected virtual void AddElements(XElement xmlElement)
        {
            return;
        }
    }
}