using System;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using System.Xml.Linq;
using XEP_SectionCheckCommon.Infrastucture;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_MaterialDataConcreteXml : XEP_XmlWorkerImpl
    {
        readonly XEP_MaterialDataConcrete _data = null;
        public XEP_MaterialDataConcreteXml(XEP_MaterialDataConcrete data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_MaterialDataConcrete";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents concrete material data.";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + "Name", _data.Name));
            xmlElement.Add(new XAttribute(ns + "Fck", _data.Fck));
            xmlElement.Add(new XAttribute(ns + "FckCube", _data.FckCube));
            xmlElement.Add(new XAttribute(ns + "EpsC1", _data.EpsC1));
            xmlElement.Add(new XAttribute(ns + "EpsCu1", _data.EpsCu1));
            xmlElement.Add(new XAttribute(ns + "EpsC2", _data.EpsC2));
            xmlElement.Add(new XAttribute(ns + "EpsCu2", _data.EpsCu2));
            xmlElement.Add(new XAttribute(ns + "EpsC3", _data.EpsC3));
            xmlElement.Add(new XAttribute(ns + "EpsCu3", _data.EpsCu3));
            xmlElement.Add(new XAttribute(ns + "N", _data.N));

        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            _data.Fck = (double)xmlElement.Attribute(ns + "Fck");
            _data.FckCube = (double)xmlElement.Attribute(ns + "FckCube");
            _data.EpsC1 = (double)xmlElement.Attribute(ns + "EpsC1");
            _data.EpsCu1 = (double)xmlElement.Attribute(ns + "EpsCu1");
            _data.EpsC2 = (double)xmlElement.Attribute(ns + "EpsC2");
            _data.EpsCu2 = (double)xmlElement.Attribute(ns + "EpsCu2");
            _data.EpsC3 = (double)xmlElement.Attribute(ns + "EpsC3");
            _data.EpsCu3 = (double)xmlElement.Attribute(ns + "EpsCu3");
            _data.N = (double)xmlElement.Attribute(ns + "N");
        }
        protected override void LoadElements(XElement xmlElement)
        {
        }

        #endregion
    }

    [Serializable]
    public class XEP_MaterialDataConcrete : XEP_IMaterialDataConcrete
    {
        // ctors
        public XEP_MaterialDataConcrete(XEP_IQuantityManager manager)
        {
            _manager = manager;
            _xmlWorker = new XEP_MaterialDataConcreteXml(this);
        }

        #region XEP_IMaterialDataConcrete Members
        double _fck = 0.0;
        public double Fck
        {
            get { return _fck; }
            set { _fck = value; }
        }
        double _fckCube = 0.0;
        public double FckCube
        {
            get { return _fckCube; }
            set { _fckCube = value; }
        }
        double _epsC1 = 0.0;
        public double EpsC1
        {
            get { return _epsC1; }
            set { _epsC1 = value; }
        }
        double _epsCu1 = 0.0;
        public double EpsCu1
        {
            get { return _epsCu1; }
            set { _epsCu1 = value; }
        }
        double _epsC2 = 0.0;
        public double EpsC2
        {
            get { return _epsC2; }
            set { _epsC2 = value; }
        }
        double _epsCu2 = 0.0;
        public double EpsCu2
        {
            get { return _epsCu2; }
            set { _epsCu2 = value; }
        }
        double _epsC3 = 0.0;
        public double EpsC3
        {
            get { return _epsC3; }
            set { _epsC3 = value; }
        }
        double _epsCu3 = 0.0;
        public double EpsCu3
        {
            get { return _epsCu3; }
            set { _epsCu3 = value; }
        }
        double _n = 0.0;
        public double N
        {
            get { return _n; }
            set { _n = value; }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = String.Empty;
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
        #endregion
    }
}
