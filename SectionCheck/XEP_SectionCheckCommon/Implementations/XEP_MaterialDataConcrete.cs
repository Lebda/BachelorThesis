using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Infrastructure;
using System.ComponentModel.DataAnnotations;
using XEP_SectionCheckCommon.ResTrans;

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
        protected override void AddElements(XElement xmlElement)
        {
            xmlElement.Add(_data.MaterialBase.XmlWorker.GetXmlElement());
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.MaterialBase.XmlWorker.LoadFromXmlElement(xmlElement.Element(ns + _data.MaterialBase.XmlWorker.GetXmlElementName()));
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
        #endregion
    }

    [Serializable]
    public class XEP_MaterialDataConcrete : ObservableObject, XEP_IMaterialDataConcrete
    {
        XEP_IMaterialData _materialBase = null;
        public XEP_IMaterialData MaterialBase
        {
            get { return _materialBase; }
            set { _materialBase = value; }
        }
        // ctors
        public XEP_MaterialDataConcrete(XEP_IQuantityManager manager, XEP_IResolver<XEP_IMaterialData> resolverBase)
        {
            _manager = manager;
            _materialBase = resolverBase.Resolve();
            _xmlWorker = new XEP_MaterialDataConcreteXml(this);
        }

        #region XEP_IMaterialDataConcrete Members
        public ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram
        {
            get { return _materialBase.StressStrainDiagram; }
            set { _materialBase.StressStrainDiagram = value; }
        }
        public eEP_MaterialDiagramType DiagramType
        {
            get { return _materialBase.DiagramType; }
            set { _materialBase.DiagramType = value; }
        }
        //
        public const string FckPropertyName = "Fck";
        double _fck = 0.0;
        public double Fck
        {
            get { return _fck; }
            set
            {
                if (_fck == value) { return; }
                _fck = value;
                RaisePropertyChanged(FckPropertyName);
            }
        }
        public const string FckCubePropertyName = "FckCube";
        double _fckCube = 0.0;
        public double FckCube
        {
            get { return _fckCube; }
            set
            {
                if (_fckCube == value) { return; }
                _fckCube = value;
                RaisePropertyChanged(FckCubePropertyName);
            }
        }
        public const string EpsC1PropertyName = "EpsC1";
        double _epsC1 = 0.0;
        public double EpsC1
        {
            get { return _epsC1; }
            set
            {
                if (_epsC1 == value) { return; }
                _epsC1 = value;
                RaisePropertyChanged(EpsC1PropertyName);
            }
        }
        public const string EpsCu1PropertyName = "EpsCu1";
        double _epsCu1 = 0.0;
        public double EpsCu1
        {
            get { return _epsCu1; }
            set
            {
                if (_epsCu1 == value) { return; }
                _epsCu1 = value;
                RaisePropertyChanged(EpsCu1PropertyName);
            }
        }
        public const string EpsC2PropertyName = "EpsC2";
        double _epsC2 = 0.0;
        public double EpsC2
        {
            get { return _epsC2; }
            set
            {
                if (_epsC2 == value) { return; }
                _epsC2 = value;
                RaisePropertyChanged(EpsC2PropertyName);
            }
        }
        public const string EpsCu2PropertyName = "EpsCu2";
        double _epsCu2 = 0.0;
        public double EpsCu2
        {
            get { return _epsCu2; }
            set
            {
                if (_epsCu2 == value) { return; }
                _epsCu2 = value;
                RaisePropertyChanged(EpsCu2PropertyName);
            }
        }
        public const string EpsC3PropertyName = "EpsC3";
        double _epsC3 = 0.0;
        public double EpsC3
        {
            get { return _epsC3; }
            set
            {
                if (_epsC3 == value) { return; }
                _epsC3 = value;
                RaisePropertyChanged(EpsC3PropertyName);
            }
        }
        public const string EpsCu3PropertyName = "EpsCu3";
        double _epsCu3 = 0.0;
        public double EpsCu3
        {
            get { return _epsCu3; }
            set
            {
                if (_epsCu3 == value) { return; }
                _epsCu3 = value;
                RaisePropertyChanged(EpsCu3PropertyName);
            }
        }
        public const string NPropertyName = "N";
        double _n = 0.0;
        public double N
        {
            get { return _n; }
            set
            {
                if (_n == value) { return; }
                _n = value;
                RaisePropertyChanged(NPropertyName);
            }
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
