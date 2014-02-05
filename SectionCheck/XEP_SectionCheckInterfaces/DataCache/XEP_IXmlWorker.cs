using System;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IXmlWorker
    {
        XElement GetXmlElement();
        void LoadFromXmlElement(XElement xmlElement);
        string GetXmlElementName();
    }

    public abstract class XEP_XmlWorkerImpl : XEP_IXmlWorker
    {
        XEP_IDataCacheObjectBase _xmlCustomer = null;
        protected XEP_IDataCacheObjectBase XmlCustomer
        {
            get { return _xmlCustomer; }
            set { _xmlCustomer = value; }
        }
        protected T GetXmlCustomer<T>()
            where T : class
        {
            T customer = XmlCustomer as T;
            Exceptions.CheckNull(customer);
            return customer;
        }
        public XEP_XmlWorkerImpl(XEP_IDataCacheObjectBase xmlCustomer)
        {
            Exceptions.CheckNull(xmlCustomer);
            _xmlCustomer = xmlCustomer;
        }
        protected virtual string GetXmlElementComment()
        {
            return "";
        }
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
            if (_xmlCustomer != null)
            {
                _xmlCustomer.Intergrity(null);
            }
            AddAtributes(xmlElement);
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
            LoadAtributes(xmlElement);
            LoadElements(xmlElement);
            if (_xmlCustomer != null)
            {
                _xmlCustomer.Intergrity(null);
            }
        }
  
        protected virtual void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _xmlCustomer.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _xmlCustomer.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
            foreach (var item in _xmlCustomer.Data)
            {
                if (item.QuantityType == eEP_QuantityType.eString)
                {
                    item.ValueName = (string)xmlElement.Attribute(ns + item.Name);
                }
                else
                {
                    item.Value = (double)xmlElement.Attribute(ns + item.Name);
                }
            }
        }

        protected virtual void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _xmlCustomer.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _xmlCustomer.Id));
            foreach (var item in _xmlCustomer.Data)
            {
                if (item.QuantityType == eEP_QuantityType.eString)
                {
                    xmlElement.Add(new XAttribute(ns + item.Name, item.ValueName));
                }
                else
                {
                    xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
                }

            }
        }

        protected virtual void AddElements(XElement xmlElement)
        {
            return;
        }
    }
}