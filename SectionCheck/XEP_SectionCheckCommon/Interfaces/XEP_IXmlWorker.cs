using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IXmlWorker
    {
        XElement GetXmlElement();
        void LoadFromXmlElement(XElement xmlElement);
    }

    public abstract class XEP_XmlWorkerImpl : XEP_IXmlWorker
    {
        protected abstract string GetXmlElementName();
        public XElement GetXmlElement()
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XElement xmlElement = new XElement(ns + GetXmlElementName());
            XComment xmlComment = new XComment(GetXmlElementComment());
            xmlElement.Add(xmlComment);
            // Attributes
            AddAtributes(xmlElement);
            // Elements
            AddElements(xmlElement);
            return xmlElement;
        }
        public void LoadFromXmlElement(XElement xmlElement)
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
