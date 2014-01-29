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
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
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
            foreach (var item in _data.StressStrainDiagram)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlElemColection = xmlElement.Elements(ns + _data.ResolverDiagramItem.Resolve().XmlWorker.GetXmlElementName());
            if (xmlElemColection != null && xmlElemColection.Count() > 0)
            {
                for (int counter = 0; counter < xmlElemColection.Count(); ++counter)
                {
                    XElement xmlElemItem = Exceptions.CheckNull<XElement>(xmlElemColection.ElementAt(counter), "Invalid XML file");
                    XEP_IESDiagramItem item = _data.ResolverDiagramItem.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlElemItem);
                    _data.StressStrainDiagram.Add(item);
                }
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_MaterialDataConcrete.DiagramTypePropertyName, XEP_Conventors.ConvertDiagramType(_data.DiagramType, false)));
            xmlElement.Add(new XAttribute(ns + XEP_MaterialDataConcrete.MatFromLibPropertyName, _data.MatFromLib));
            foreach(var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.DiagramType = XEP_Conventors.ConvertDiagramType((string)xmlElement.Attribute(ns + XEP_MaterialDataConcrete.DiagramTypePropertyName), false);
            _data.MatFromLib = (bool)xmlElement.Attribute(ns + XEP_MaterialDataConcrete.MatFromLibPropertyName);
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
        }
        readonly XEP_IResolver<XEP_IESDiagramItem> _resolverDiagramItem = null;
        public XEP_IResolver<XEP_IESDiagramItem> ResolverDiagramItem
        {
            get { return _resolverDiagramItem; }
        }
        XEP_IResolver<XEP_IMaterialDataConcrete> _resolver = null;
        // ctors
        public XEP_MaterialDataConcrete(XEP_IQuantityManager manager,
            XEP_IResolver<XEP_IESDiagramItem> resolverDiagramItem,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolver)
        {
            _manager = manager;
            _xmlWorker = new XEP_MaterialDataConcreteXml(this);
            _resolverDiagramItem = resolverDiagramItem;
            _resolver = resolver;
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStress, FckPropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStress, FckCubePropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC1PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu1PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC2PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu2PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsC3PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu3PropertyName));
            _data.Add(XEP_QuantityFactory.Instance().Create(_manager, 0.0, eEP_QuantityType.eNoUnit, NPropertyName));
        }

        #region XEP_IMaterialDataConcrete Members
        public void ResetMatFromLib()
        {
            _matFromLib = true;
            RaisePropertyChanged(MatFromLibPropertyName);
            foreach (var item in _data)
            {
                RaisePropertyChanged(item.Name);
            }
        }
        public XEP_IMaterialData CopyInstance()
        {
            XEP_IMaterialDataConcrete copy = _resolver.Resolve();
            copy.Name = _name;
            copy.StressStrainDiagram = DeepCopy.Make<ObservableCollection<XEP_IESDiagramItem>>(_stressStrainDiagram);
            copy.DiagramType = _diagramType;
            copy.MatFromLib = _matFromLib;
            for (int counter = 0; counter < _data.Count - 1; ++counter)
            {
                copy.Data[counter] = DeepCopy.Make<XEP_IQuantity>(_data[counter]);
            }
            return copy;
        }
        public void CreatePoints(XEP_ISetupParameters setup)
        {
            switch(DiagramType)
            {
                case eEP_MaterialDiagramType.eBiliUls:
                default:
                    {
                        ObservableCollection<XEP_IESDiagramItem> diagram = new ObservableCollection<XEP_IESDiagramItem>();
                        XEP_IESDiagramItem diagItem = _resolverDiagramItem.Resolve();
                        diagItem.Strain.Value = 0.0;
                        diagItem.Stress.Value = 0.0;
                        diagram.Add(diagItem);
                        diagItem = _resolverDiagramItem.Resolve();
                        diagItem.Strain.Value = -EpsC3.Value;
                        double helpVal = -(setup.AlphaCc.Value * Fck.Value) / (setup.GammaC.Value);
                        diagItem.Stress.Value = helpVal;
                        diagram.Add(diagItem);
                        diagItem = _resolverDiagramItem.Resolve();
                        diagItem.Strain.Value = -EpsCu3.Value;
                        diagItem.Stress.Value = helpVal;
                        diagram.Add(diagItem);
                        StressStrainDiagram = diagram;
                    }
                    break;
            }
        }

        public static readonly string StressStrainDiagramPropertyName = "StressStrainDiagram";
        private ObservableCollection<XEP_IESDiagramItem> _stressStrainDiagram = new ObservableCollection<XEP_IESDiagramItem>();
        public ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram
        {
            get { return _stressStrainDiagram; }
            set
            {
                if (_stressStrainDiagram == value) { return; }
                _stressStrainDiagram = value;
                RaisePropertyChanged(StressStrainDiagramPropertyName);
            }
        }
        public static readonly string DiagramTypePropertyName = "DiagramType";
        private eEP_MaterialDiagramType _diagramType = eEP_MaterialDiagramType.eBiliUls;
        public eEP_MaterialDiagramType DiagramType
        {
            get { return _diagramType; }
            set
            {
                if (_diagramType == value) { return; }
                _diagramType = value;
                RaisePropertyChanged(DiagramTypePropertyName);
            }
        }
        public static readonly string MatFromLibPropertyName = "MatFromLib";
        private bool _matFromLib = true;
        public bool MatFromLib
        {
            get { return _matFromLib; }
            set
            {
                if (_matFromLib == false)
                {// already dirty no change
                    return;
                }
                if (_matFromLib == value) { return; }
                _matFromLib = value;
                RaisePropertyChanged(MatFromLibPropertyName);
                foreach (var item in _data)
                {
                    RaisePropertyChanged(item.Name);
                }
            }
        }
        //
        public static readonly string FckPropertyName = "Fck";
        public XEP_IQuantity Fck
        {
            get { return _data[0]; }
            set
            {
                if (_data[0] == value) { return; }
                SetItem(ref value, ref _data, 0, _data[0].Name);
            }
        }
        public const string FckCubePropertyName = "FckCube";
        public XEP_IQuantity FckCube
        {
            get { return _data[1]; }
            set
            {
                if (_data[1] == value) { return; }
                SetItem(ref value, ref _data, 1, _data[1].Name);
            }
        }
        public const string EpsC1PropertyName = "EpsC1";
        public XEP_IQuantity EpsC1
        {
            get { return _data[2]; }
            set
            {
                if (_data[2] == value) { return; }
                SetItem(ref value, ref _data, 2, _data[2].Name);
            }
        }
        public const string EpsCu1PropertyName = "EpsCu1";
        public XEP_IQuantity EpsCu1
        {
            get { return _data[3]; }
            set
            {
                if (_data[3] == value) { return; }
                SetItem(ref value, ref _data, 3, _data[3].Name);
            }
        }
        public const string EpsC2PropertyName = "EpsC2";
        public XEP_IQuantity EpsC2
        {
            get { return _data[4]; }
            set
            {
                if (_data[4] == value) { return; }
                SetItem(ref value, ref _data, 4, _data[4].Name);
            }
        }
        public const string EpsCu2PropertyName = "EpsCu2";
        public XEP_IQuantity EpsCu2
        {
            get { return _data[5]; }
            set
            {
                if (_data[5] == value) { return; }
                SetItem(ref value, ref _data, 5, _data[5].Name);
            }
        }
        public const string EpsC3PropertyName = "EpsC3";
        public XEP_IQuantity EpsC3
        {
            get { return _data[6]; }
            set
            {
                if (_data[6] == value) { return; }
                SetItem(ref value, ref _data, 6, _data[6].Name);
            }
        }
        public const string EpsCu3PropertyName = "EpsCu3";
        public XEP_IQuantity EpsCu3
        {
            get { return _data[7]; }
            set
            {
                if (_data[7] == value) { return; }
                SetItem(ref value, ref _data, 7, _data[7].Name);
            }
        }
        public const string NPropertyName = "N";
        public XEP_IQuantity N
        {
            get { return _data[8]; }
            set
            {
                if (_data[8] == value) { return; }
                SetItem(ref value, ref _data, 8, _data[8].Name);
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
            set
            {
                if (_name == value) { return; }
                _name = value;
                RaisePropertyChanged(XEP_Constants.NamePropertyName);
            }
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
        private void SetItem(ref XEP_IQuantity valueFromBinding, ref ObservableCollection<XEP_IQuantity> data, int index, params string[] names)
        {
            if (data[index] == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, data[index]))
            {
                return;
            }
            data[index] = valueFromBinding;
            foreach (string item in names)
            {
                RaisePropertyChanged(item);
            }
        }
        private bool SetItemFromBinding(ref XEP_IQuantity valueFromBinding, XEP_IQuantity propertyItem)
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
