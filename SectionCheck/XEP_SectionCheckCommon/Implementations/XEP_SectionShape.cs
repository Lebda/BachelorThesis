using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using System.ComponentModel;

namespace XEP_SectionCheckCommon.Implementations
{
    [Serializable]
    class XEP_SectionShapeXml : XEP_XmlWorkerImpl
    {
        readonly XEP_SectionShape _data = null;
        public XEP_SectionShapeXml(XEP_SectionShape data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SectionShape";
        }
        protected override string GetXmlElementComment()
        {
            return "Object represents section shape";
        }
        protected override void AddElements(XElement xmlElement)
        {
            foreach (var item in _data.ShapeOuter)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
            foreach (var item in _data.ShapeInner)
            {
                xmlElement.Add(item.XmlWorker.GetXmlElement());
            }
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_Constants.GuidPropertyName, _data.Id));
            foreach (var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            var xmlElements = xmlElement.Elements(ns + _data.Resolver.Resolve().XmlWorker.GetXmlElementName());
            if (xmlElements != null && xmlElements.Count() > 0)
            {
                for (int counter = 0; counter < xmlElements.Count(); ++counter)
                {
                    XElement xmlForce = Exceptions.CheckNull<XElement>(xmlElements.ElementAt(counter), "Invalid XML file");
                    XEP_ISectionShapeItem item = _data.Resolver.Resolve();
                    item.XmlWorker.LoadFromXmlElement(xmlForce);
                    if (item.Type == eEP_CssShapePointType.eOuter)
                    {
                        _data.ShapeOuter.Add(item);
                    }
                    else
                    {
                        _data.ShapeInner.Add(item);
                    }
                }
            }
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Id = (Guid)xmlElement.Attribute(ns + XEP_Constants.GuidPropertyName);
            foreach (var item in _data.Data)
            {
                item.Value = (double)xmlElement.Attribute(ns + item.Name);
            }
        }
        #endregion
    }

    [Serializable]
    public class XEP_SectionShape : XEP_ObservableObject, XEP_ISectionShape
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolver = null;
        public XEP_IResolver<XEP_ISectionShapeItem> Resolver
        {
            get { return _resolver; }
        }
        XEP_IXmlWorker _xmlWorker = null;
        XEP_IQuantityManager _manager = null;
        string _name = "Section shape";
        ObservableCollection<XEP_ISectionShapeItem> _shapeOuter = new ObservableCollection<XEP_ISectionShapeItem>();
        ObservableCollection<XEP_ISectionShapeItem> _shapeInner = new ObservableCollection<XEP_ISectionShapeItem>();

        public XEP_SectionShape(XEP_IResolver<XEP_ISectionShapeItem> sectionShapeItemResolver, XEP_IQuantityManager manager)
        {
            _resolver = sectionShapeItemResolver;
            _manager = manager;
            _xmlWorker = new XEP_SectionShapeXml(this);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eBool, PolygonModePropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, HPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, BPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eBool, HoleModePropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, BholePropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, HholePropertyName);
        }

        #region XEP_ISectionShape Members
        public static readonly string PolygonModePropertyName = "PolygonMode";
        public bool PolygonMode
        {
            get { return GetOneQuantityBool(PolygonModePropertyName); }
            set{ SetItemBoolWithActions(ref value, PolygonModePropertyName, () => !PolygonMode, Recalculate);}
        }
        public static readonly string HoleModePropertyName = "HoleMode";
        public bool HoleMode
        {
            get { return GetOneQuantityBool(HoleModePropertyName); }
            set { SetItemBoolWithActions(ref value, HoleModePropertyName, () => true, Recalculate); }
        }
        public static readonly string HholePropertyName = "Hhole";
        public XEP_IQuantity Hhole
        {
            get { return GetOneQuantity(HholePropertyName); }
            set { SetItemWithActions(ref value, HholePropertyName, () => !PolygonMode && HoleMode, Recalculate); }
        }
        public static readonly string BholePropertyName = "Bhole";
        public XEP_IQuantity Bhole
        {
            get { return GetOneQuantity(BholePropertyName); }
            set { SetItemWithActions(ref value, BholePropertyName, () => !PolygonMode && HoleMode, Recalculate); }
        }
        public static readonly string HPropertyName = "H";
        public XEP_IQuantity H
        {
            get { return GetOneQuantity(HPropertyName); }
            set { SetItemWithActions(ref value, HPropertyName, () => !PolygonMode, Recalculate); }
        }
        public static readonly string BPropertyName = "B";
        public XEP_IQuantity B
        {
            get { return GetOneQuantity(BPropertyName); }
            set { SetItemWithActions(ref value, BPropertyName, () => !PolygonMode, Recalculate); }
        }
        public static readonly string ShapeOuterPropertyName = "ShapeOuter";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeOuter
        {
            get { return _shapeOuter; }
            set { SetMemberWithAction<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _shapeOuter, () => _shapeOuter != value, Recalculate);}
        }
        public static readonly string ShapeInnerPropertyName = "ShapeInner";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeInner
        {
            get { return _shapeInner; }
            set { SetMemberWithAction<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _shapeInner, () => _shapeInner != value, Recalculate); }
        }
        #endregion

        #region METHODS
        public void Recalculate()
        {
            if (PolygonMode)
            {
                H.Value = 0.0; B.Value = 0.0; Hhole.Value = 0.0; Bhole.Value = 0.0;
            }
            else
            {
                _shapeOuter = XEP_ViewModelHelp.CreateRectShape(_resolver, B.Value / 2.0, H.Value / 2.0, true);
                _shapeInner = XEP_ViewModelHelp.CreateRectShape(_resolver, Bhole.Value / 2.0, Hhole.Value / 2.0, false);
            }
            if (!HoleMode)
            {
                Hhole.Value = 0.0; Bhole.Value = 0.0;
                _shapeInner = null;
            }
            RaisePropertyChanged(ShapeOuterPropertyName);
            RaisePropertyChanged(ShapeInnerPropertyName);
            foreach (var item in Data)
            {
                RaisePropertyChanged(item.Name);
            }

        }

        #endregion

        #region XEP_IDataCacheObjectBase Members
        public void Test(string name, double oldValue)
        {
            TestInternal(name, oldValue, (s) => { this.H = s; }, () => this.H);
            TestInternal(name, oldValue, (s) => { this.B = s; }, () => this.B);
            TestInternal(name, oldValue, (s) => { this.Bhole = s; }, () => this.Bhole);
            TestInternal(name, oldValue, (s) => { this.Hhole = s; }, () => this.Hhole);
        }
        void TestInternal(string name, double oldValue, Action<XEP_IQuantity> propertySetter, Func<XEP_IQuantity> propertyGetter)
        {
            if (propertyGetter().Name == name)
            {
                XEP_IQuantity copy = DeepCopy.Make<XEP_IQuantity>(propertyGetter());
                propertyGetter().ManagedValue = oldValue;
                propertySetter(copy);
            }
        }
        public void SetOneQuantity(string index, double oldManagedValue)
        {
            Action<XEP_IQuantity> test = (s) => { this.H = s; };
        }
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
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion


    }
}
