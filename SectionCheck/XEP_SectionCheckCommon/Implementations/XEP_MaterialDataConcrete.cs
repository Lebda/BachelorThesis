using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
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
            foreach(var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + "Name");
            foreach (var item in _data.Data)
            {
                item.Value = (double)xmlElement.Attribute(ns + item.Name);
            }
        }
        #endregion
    }

    [Serializable]
    public class XEP_MaterialDataConcrete : ObservableObject, XEP_IMaterialDataConcrete
    {
        ObservableCollection<XEP_IQuantity> _data = new ObservableCollection<XEP_IQuantity>();
        public ObservableCollection<XEP_IQuantity> Data
        {
            get { return _data; }
            protected set { _data = value; }
        }
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
            _fck = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStress, FckPropertyName);
            _fckCube = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStress, FckCubePropertyName);
            _epsC1 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC1PropertyName);
            _epsCu1 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu1PropertyName);
            _epsC2 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC2PropertyName);
            _epsCu2 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu2PropertyName);
            _epsC3 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC3PropertyName);
            _epsCu3 = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu3PropertyName);
            _n = XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoType, NPropertyName);
            _data.Add(_fck);
            _data.Add(_fckCube);
            _data.Add(_epsC1);
            _data.Add(_epsCu1);
            _data.Add(_epsC2);
            _data.Add(_epsCu2);
            _data.Add(_epsC3);
            _data.Add(_epsCu3);
            _data.Add(_n);
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
            set 
            { 
                _materialBase.DiagramType = value; 
                
            }
        }
        //
        public static readonly string FckPropertyName = "Fck";
        XEP_IQuantity _fck = null;
        public XEP_IQuantity Fck
        {
            get { return _fck; }
            set
            {
                if (_fck == value) { return; }
                SetItem(ref value, ref _fck, FckPropertyName);
                RaisePropertyChanged(FckPropertyName);
            }
        }
        public const string FckCubePropertyName = "FckCube";
        XEP_IQuantity _fckCube = null;
        public XEP_IQuantity FckCube
        {
            get { return _fckCube; }
            set
            {
                if (_fckCube == value) { return; }
                SetItem(ref value, ref _fckCube, FckCubePropertyName);
                RaisePropertyChanged(FckCubePropertyName);
            }
        }
        public const string EpsC1PropertyName = "EpsC1";
        XEP_IQuantity _epsC1 = null;
        public XEP_IQuantity EpsC1
        {
            get { return _epsC1; }
            set
            {
                if (_epsC1 == value) { return; }
                SetItem(ref value, ref _epsC1, EpsC1PropertyName);
                RaisePropertyChanged(EpsC1PropertyName);
            }
        }
        public const string EpsCu1PropertyName = "EpsCu1";
        XEP_IQuantity _epsCu1 = null;
        public XEP_IQuantity EpsCu1
        {
            get { return _epsCu1; }
            set
            {
                if (_epsCu1 == value) { return; }
                SetItem(ref value, ref _epsCu1, EpsCu1PropertyName);
                RaisePropertyChanged(EpsCu1PropertyName);
            }
        }
        public const string EpsC2PropertyName = "EpsC2";
        XEP_IQuantity _epsC2 = null;
        public XEP_IQuantity EpsC2
        {
            get { return _epsC2; }
            set
            {
                if (_epsC2 == value) { return; }
                SetItem(ref value, ref _epsC2, EpsC2PropertyName);
                RaisePropertyChanged(EpsC2PropertyName);
            }
        }
        public const string EpsCu2PropertyName = "EpsCu2";
        XEP_IQuantity _epsCu2 = null;
        public XEP_IQuantity EpsCu2
        {
            get { return _epsCu2; }
            set
            {
                if (_epsCu2 == value) { return; }
                SetItem(ref value, ref _epsCu2, EpsCu2PropertyName);
                RaisePropertyChanged(EpsCu2PropertyName);
            }
        }
        public const string EpsC3PropertyName = "EpsC3";
        XEP_IQuantity _epsC3 = null;
        public XEP_IQuantity EpsC3
        {
            get { return _epsC3; }
            set
            {
                if (_epsC3 == value) { return; }
                SetItem(ref value, ref _epsC3, EpsC3PropertyName);
                RaisePropertyChanged(EpsC3PropertyName);
            }
        }
        public const string EpsCu3PropertyName = "EpsCu3";
        XEP_IQuantity _epsCu3 = null;
        public XEP_IQuantity EpsCu3
        {
            get { return _epsCu3; }
            set
            {
                if (_epsCu3 == value) { return; }
                SetItem(ref value, ref _epsCu3, EpsCu3PropertyName);
                RaisePropertyChanged(EpsCu3PropertyName);
            }
        }
        public const string NPropertyName = "N";
        XEP_IQuantity _n = null;
        public XEP_IQuantity N
        {
            get { return _n; }
            set
            {
                if (_n == value) { return; }
                SetItem(ref value, ref _n, NPropertyName);
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
        private void SetItem(ref XEP_IQuantity valueFromBinding, ref XEP_IQuantity property, params string[] names)
        {
            if (property == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, ref property))
            {
                return;
            }
            property = valueFromBinding;
            foreach (string item in names)
            {
                RaisePropertyChanged(item);
            }
        }
        private bool SetItemFromBinding(ref XEP_IQuantity valueFromBinding, ref XEP_IQuantity propertyItem)
        {
            if (valueFromBinding == null)
            {
                return false;
            }
            if (valueFromBinding.Manager == null && string.IsNullOrEmpty(valueFromBinding.Name) && valueFromBinding.QuantityType == eEP_QuantityType.eNoType)
            { // setting throw binding
                valueFromBinding.Manager = propertyItem.Manager;
                valueFromBinding.Name = propertyItem.Name;
                valueFromBinding.QuantityType = propertyItem.QuantityType;
                valueFromBinding.Value = Manager.GetValueManaged(valueFromBinding.Value, valueFromBinding.QuantityType);
                if (valueFromBinding.Value == propertyItem.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
