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
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
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
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
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
    public class XEP_MaterialDataConcrete : XEP_ObservableObject, XEP_IMaterialDataConcrete
    {
        readonly XEP_IResolver<XEP_IESDiagramItem> _resolverDiagramItem = null;
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolver = null;
        public XEP_IResolver<XEP_IESDiagramItem> ResolverDiagramItem
        {
            get { return _resolverDiagramItem; }
        }

        // ctors
        public XEP_MaterialDataConcrete(XEP_IQuantityManager manager,
            XEP_IResolver<XEP_IESDiagramItem> resolverDiagramItem,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolver)
        {
            _manager = manager;
            _xmlWorker = new XEP_MaterialDataConcreteXml(this);
            _resolverDiagramItem = resolverDiagramItem;
            _resolver = resolver;
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStress, FckPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStress, FckCubePropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsC1PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu1PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsC2PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu2PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsC3PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eStrain, EpsCu3PropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eNoUnit, NPropertyName);
        }

        #region XEP_IMaterialDataConcrete Members
        public void ResetMatFromLib()
        {
            _matFromLib = true;
            RaisePropertyChanged(MatFromLibPropertyName);
            foreach (var item in Data)
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
            XEP_MaterialDataConcrete copyCon = copy as XEP_MaterialDataConcrete;
            Exceptions.CheckNull(copyCon);
            for (int counter = 0; counter < Data.Count - 1; ++counter)
            {
                copyCon.Data[counter] = DeepCopy.Make<XEP_IQuantity>(Data[counter]);
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
            set { SetMember < ObservableCollection<XEP_IESDiagramItem>>(ref value, ref _stressStrainDiagram, (_stressStrainDiagram == value), StressStrainDiagramPropertyName); }
        }
        public static readonly string DiagramTypePropertyName = "DiagramType";
        private eEP_MaterialDiagramType _diagramType = eEP_MaterialDiagramType.eBiliUls;
        public eEP_MaterialDiagramType DiagramType
        {
            get { return _diagramType; }
            set { SetMember<eEP_MaterialDiagramType>(ref value, ref _diagramType, (_diagramType == value), DiagramTypePropertyName); }
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
                foreach (var item in Data)
                {
                    RaisePropertyChanged(item.Name);
                }
            }
        }
        //
        public static readonly string FckPropertyName = "Fck";
        public XEP_IQuantity Fck
        {
            get { return GetOneQuantity(FckPropertyName); }
            set { SetItem(ref value, FckPropertyName); }
        }
        public static readonly string FckCubePropertyName = "FckCube";
        public XEP_IQuantity FckCube
        {
            get { return GetOneQuantity(FckCubePropertyName); }
            set { SetItem(ref value, FckCubePropertyName); }
        }
        public static readonly string EpsC1PropertyName = "EpsC1";
        public XEP_IQuantity EpsC1
        {
            get { return GetOneQuantity(EpsC1PropertyName); }
            set { SetItem(ref value, EpsC1PropertyName); }
        }
        public static readonly string EpsCu1PropertyName = "EpsCu1";
        public XEP_IQuantity EpsCu1
        {
            get { return GetOneQuantity(EpsCu1PropertyName); }
            set { SetItem(ref value, EpsCu1PropertyName); }
        }
        public static readonly string EpsC2PropertyName = "EpsC2";
        public XEP_IQuantity EpsC2
        {
            get { return GetOneQuantity(EpsC2PropertyName); }
            set { SetItem(ref value, EpsC2PropertyName); }
        }
        public static readonly string EpsCu2PropertyName = "EpsCu2";
        public XEP_IQuantity EpsCu2
        {
            get { return GetOneQuantity(EpsCu2PropertyName); }
            set { SetItem(ref value, EpsCu2PropertyName); }
        }
        public static readonly string EpsC3PropertyName = "EpsC3";
        public XEP_IQuantity EpsC3
        {
            get { return GetOneQuantity(EpsC3PropertyName); }
            set { SetItem(ref value, EpsC3PropertyName); }
        }
        public static readonly string EpsCu3PropertyName = "EpsCu3";
        public XEP_IQuantity EpsCu3
        {
            get { return GetOneQuantity(EpsCu3PropertyName); }
            set { SetItem(ref value, EpsCu3PropertyName); }
        }
        public static readonly string NPropertyName = "N";
        public XEP_IQuantity N
        {
            get { return GetOneQuantity(NPropertyName); }
            set { SetItem(ref value, NPropertyName); }
        }
        #endregion
        #region XEP_IDataCacheObjectBase Members
        XEP_IQuantityManager _manager = null;
        XEP_IXmlWorker _xmlWorker = null;
        string _name = String.Empty;
        //
        public string Name
        {
            get { return _name; }
            set { SetMember<string>(ref value, ref _name, (_name == value), XEP_Constants.NamePropertyName); }
        }
        Guid _guid = Guid.NewGuid();
        public Guid Id
        {
            get { return _guid; }
            set { SetMember<Guid>(ref value, ref _guid, (_guid == value), XEP_Constants.GuidPropertyName); }
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
