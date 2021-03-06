﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_MaterialDataConcreteXml : XEP_XmlWorkerImpl
    {
        public XEP_MaterialDataConcreteXml(XEP_IMaterialDataConcrete data)
            : base(data)
        {
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
            XEP_IMaterialDataConcrete customer = GetXmlCustomer<XEP_IMaterialDataConcrete>();
            foreach (var item in customer.StressStrainDiagram)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            XEP_MaterialDataConcrete customer = GetXmlCustomer<XEP_MaterialDataConcrete>();
            var xmlElemColection = xmlElement.Elements(ns + customer.ResolverDiagramItem.Resolve().XmlWorker.GetXmlElementName());
            if (xmlElemColection != null && xmlElemColection.Count() > 0)
            {
                for (int counter = 0; counter < xmlElemColection.Count(); ++counter)
                {
                    XElement xmlElemItem = Exceptions.CheckNull<XElement>(xmlElemColection.ElementAt(counter), "Invalid XML file");
                    XEP_IESDiagramItem item = customer.ResolverDiagramItem.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlElemItem);
                    customer.StressStrainDiagram.Add(item);
                }
            }
        }
        #endregion
    }

    public class XEP_MaterialDataConcrete : XEP_ObservableObject, XEP_IMaterialDataConcrete
    {
        readonly XEP_IResolver<XEP_IESDiagramItem> _resolverDiagramItem = null;
        readonly XEP_IResolver<XEP_IMaterialDataConcrete> _resolver = null;
        public XEP_IResolver<XEP_IESDiagramItem> ResolverDiagramItem
        {
            get { return _resolverDiagramItem; }
        }

        // ctors
        public XEP_MaterialDataConcrete(
            XEP_IResolver<XEP_IESDiagramItem> resolverDiagramItem,
            XEP_IResolver<XEP_IMaterialDataConcrete> resolver)
        {
            XmlWorker = new XEP_MaterialDataConcreteXml(this);
            _resolverDiagramItem = resolverDiagramItem;
            _resolver = resolver;
            AddOneQuantity(0.0, eEP_QuantityType.eString, MaterialNamePropertyName, this, null, MaterialNamePropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eBool, MatFromLibPropertyName, this);
            AddOneQuantity((double)XEP_eMaterialDiagramType.BiliUls, eEP_QuantityType.eEnum, DiagramTypePropertyName, this, typeof(XEP_eMaterialDiagramType).Name);
            AddOneQuantity( 0.0, eEP_QuantityType.eStress, FckPropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStress, FckCubePropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, EpsC1PropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, EpsCu1PropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, EpsC2PropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, EpsCu2PropertyName, this);
            AddOneQuantity( 0.0, eEP_QuantityType.eStrain, EpsC3PropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eStrain, EpsCu3PropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eNoUnit, NPropertyName, this);
            Intergrity(null);
        }

        #region ICloneable Members
        public object Clone()
        {
            XEP_IMaterialDataConcrete copy = _resolver.Resolve();
            copy.Name = _name;
            foreach (var item in _stressStrainDiagram)
            {
                copy.StressStrainDiagram.Add(item.CopyInstance());
            }
            XEP_MaterialDataConcrete copyCon = copy as XEP_MaterialDataConcrete;
            copyCon.CopyAllQuanties(this, copy);
            return copy;
        }
        #endregion

        #region XEP_IMaterialDataConcrete Members
        public void ResetMatFromLib()
        {
            MatFromLibMode.SetBool(true);
            Intergrity(null);
        }
        public void CreatePoints(XEP_ISetupParameters setup)
        {
            switch (DiagramType.GetEnumValue<XEP_eMaterialDiagramType>())
            {
                case XEP_eMaterialDiagramType.BiliUls:
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
        public void Intergrity(string propertyCallerName)
        {
            if (propertyCallerName == MaterialNamePropertyName)
            {
                return;
            }
            foreach (var item in Data)
            {
                item.Value = MathUtils.CorrectOnRange(item.Value, 0.0, double.MaxValue);
                item.VisibleState = Visibility.Visible;
                if (item.Name == MatFromLibPropertyName)
                {
                    item.IsReadOnly = false;
                }
                else
                {
                    item.IsReadOnly = true;
                }
            }
            if (!MatFromLibMode.IsTrue())
            {
                foreach (var item in Data)
                {
                    if (item.Name == MatFromLibPropertyName)
                    {
                        item.IsReadOnly = true;
                    }
                    else
                    {
                        item.IsReadOnly = false;
                    }
                }
            }
            Name = MaterialName.ValueName;
            DiagramType.IsReadOnly = false;
            // Raise all
            foreach (var item in Data)
            {
                RaisePropertyChanged(item.Name);
            }
            RaisePropertyChanged(StressStrainDiagramPropertyName);
        }
        // PROPERTIES
        private ObservableCollection<XEP_IESDiagramItem> _stressStrainDiagram = new ObservableCollection<XEP_IESDiagramItem>();
        public static readonly string StressStrainDiagramPropertyName = "StressStrainDiagram";
        public ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram
        {
            get { return _stressStrainDiagram; }
            set { SetMemberWithAction<ObservableCollection<XEP_IESDiagramItem>>(ref value, ref _stressStrainDiagram, () => _stressStrainDiagram != value, Intergrity, StressStrainDiagramPropertyName); }
        }
        public static readonly string MaterialNamePropertyName = "MaterialName";
        public XEP_IQuantity MaterialName
        {
            get { return GetOneQuantity(MaterialNamePropertyName); }
            set { SetItemWithActions(ref value, MaterialNamePropertyName, () => !MaterialName.Equals(value), Intergrity); }
        }
        public static readonly string DiagramTypePropertyName = "DiagramType";
        public XEP_IQuantity DiagramType
        {
            get { return GetOneQuantity(DiagramTypePropertyName); }
            set { SetItemWithActions(ref value, DiagramTypePropertyName, () => !GetOneQuantity(DiagramTypePropertyName).Equals(value), Intergrity); }
        }
        public static readonly string MatFromLibPropertyName = "MatFromLibMode";
        public XEP_IQuantity MatFromLibMode
        {
            get { return GetOneQuantity(MatFromLibPropertyName); }
            set { SetItemWithActions(ref value, MatFromLibPropertyName, () => MatFromLibMode.IsTrue(), Intergrity); }
        }
        //
        public static readonly string FckPropertyName = "Fck";
        public XEP_IQuantity Fck
        {
            get { return GetOneQuantity(FckPropertyName); }
            set { SetItemWithActions(ref value, FckPropertyName, () => !Fck.Equals(value), Intergrity); }
        }
        public static readonly string FckCubePropertyName = "FckCube";
        public XEP_IQuantity FckCube
        {
            get { return GetOneQuantity(FckCubePropertyName); }
            set { SetItemWithActions(ref value, FckCubePropertyName, () => !FckCube.Equals(value), Intergrity); }
        }
        public static readonly string EpsC1PropertyName = "EpsC1";
        public XEP_IQuantity EpsC1
        {
            get { return GetOneQuantity(EpsC1PropertyName); }
            set { SetItemWithActions(ref value, EpsC1PropertyName, () => !GetOneQuantity(EpsC1PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string EpsCu1PropertyName = "EpsCu1";
        public XEP_IQuantity EpsCu1
        {
            get { return GetOneQuantity(EpsCu1PropertyName); }
            set { SetItemWithActions(ref value, EpsCu1PropertyName, () => !GetOneQuantity(EpsCu1PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string EpsC2PropertyName = "EpsC2";
        public XEP_IQuantity EpsC2
        {
            get { return GetOneQuantity(EpsC2PropertyName); }
            set { SetItemWithActions(ref value, EpsC2PropertyName, () => !GetOneQuantity(EpsC2PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string EpsCu2PropertyName = "EpsCu2";
        public XEP_IQuantity EpsCu2
        {
            get { return GetOneQuantity(EpsCu2PropertyName); }
            set { SetItemWithActions(ref value, EpsCu2PropertyName, () => !GetOneQuantity(EpsCu2PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string EpsC3PropertyName = "EpsC3";
        public XEP_IQuantity EpsC3
        {
            get { return GetOneQuantity(EpsC3PropertyName); }
            set { SetItemWithActions(ref value, EpsC3PropertyName, () => !GetOneQuantity(EpsC3PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string EpsCu3PropertyName = "EpsCu3";
        public XEP_IQuantity EpsCu3
        {
            get { return GetOneQuantity(EpsCu3PropertyName); }
            set { SetItemWithActions(ref value, EpsCu3PropertyName, () => !GetOneQuantity(EpsCu3PropertyName).Equals(value), Intergrity); }
        }
        public static readonly string NPropertyName = "N";
        public XEP_IQuantity N
        {
            get { return GetOneQuantity(NPropertyName); }
            set { SetItemWithActions(ref value, NPropertyName, () => !GetOneQuantity(NPropertyName).Equals(value), Intergrity); }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        string _name = String.Empty;
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
        public XEP_IXmlWorker XmlWorker {get; set;}
        #endregion
    }
}
