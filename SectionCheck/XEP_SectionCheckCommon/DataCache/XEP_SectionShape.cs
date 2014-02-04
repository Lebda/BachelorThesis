using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;
using XEP_SectionCheckInterfaces.SectionDrawer;

namespace XEP_SectionCheckCommon.DataCache
{
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
                    if (item.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eOuter)
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

    public class XEP_SectionShape : XEP_ObservableObject, XEP_ISectionShape
    {
        // Members
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolver = null;
        readonly XEP_IResolver<XEP_ICssDataShape> _resolverICssDataShape = null;
        public XEP_IResolver<XEP_ISectionShapeItem> Resolver
        {
            get { return _resolver; }
        }

        public XEP_SectionShape(XEP_IResolver<XEP_ISectionShapeItem> sectionShapeItemResolver, 
            XEP_IResolver<XEP_ICssDataShape> resolverICssDataShape,
            XEP_IQuantityManager manager)
        {
            _resolverICssDataShape = resolverICssDataShape;
            _cssShape = _resolverICssDataShape.Resolve();
            _resolver = sectionShapeItemResolver;
            _manager = manager;
            _xmlWorker = new XEP_SectionShapeXml(this);
            AddOneQuantity(0.0, eEP_QuantityType.eBool, PolygonModePropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, HPropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, BPropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eBool, HoleModePropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, HholePropertyName, this);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, BholePropertyName, this);
            Intergrity(null);
        }

        #region XEP_ISectionShape Members
        string _description = String.Empty;
        public static readonly string DescriptionPropertyName = "Description";
        public string Description
        {
            get { return _description; }
            set { SetMemberWithAction<string>(ref value, ref _description, () => _description != value, Intergrity); }
        }
        XEP_ICssDataShape _cssShape = null;
        public static readonly string CssShapePropertyName = "CssShape";
        public XEP_ICssDataShape CssShape
        {
            get { return _cssShape; }
            set { SetMemberWithAction<XEP_ICssDataShape>(ref value, ref _cssShape, () => _cssShape != value, Intergrity); }
        }
        public static readonly string PolygonModePropertyName = "PolygonMode";
        public XEP_IQuantity PolygonMode
        {
            get { return GetOneQuantity(PolygonModePropertyName); }
            set { SetItemWithActions(ref value, PolygonModePropertyName, null, Intergrity); }
        }
        public static readonly string HoleModePropertyName = "HoleMode";
        public XEP_IQuantity HoleMode
        {
            get { return GetOneQuantity(HoleModePropertyName); }
            set { SetItemWithActions(ref value, HoleModePropertyName, null, Intergrity); }
        }
        public static readonly string HholePropertyName = "Hhole";
        public XEP_IQuantity Hhole
        {
            get { return GetOneQuantity(HholePropertyName); }
            set { SetItemWithActions(ref value, HholePropertyName, () => !PolygonMode.IsTrue() && HoleMode.IsTrue(), Intergrity); }
        }
        public static readonly string BholePropertyName = "Bhole";
        public XEP_IQuantity Bhole
        {
            get { return GetOneQuantity(BholePropertyName); }
            set { SetItemWithActions(ref value, BholePropertyName, () => !PolygonMode.IsTrue() && HoleMode.IsTrue(), Intergrity); }
        }
        public static readonly string HPropertyName = "H";
        public XEP_IQuantity H
        {
            get { return GetOneQuantity(HPropertyName); }
            set { SetItemWithActions(ref value, HPropertyName, () => !PolygonMode.IsTrue(), Intergrity); }
        }
        public static readonly string BPropertyName = "B";
        public XEP_IQuantity B
        {
            get { return GetOneQuantity(BPropertyName); }
            set { SetItemWithActions(ref value, BPropertyName, () => !PolygonMode.IsTrue(), Intergrity); }
        }
        ObservableCollection<XEP_ISectionShapeItem> _shapeOuter = new ObservableCollection<XEP_ISectionShapeItem>();
        public static readonly string ShapeOuterPropertyName = "ShapeOuter";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeOuter
        {
            get { return _shapeOuter; }
            set { SetMemberWithAction<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _shapeOuter, () => _shapeOuter != value, Intergrity, ShapeOuterPropertyName); }
        }
        ObservableCollection<XEP_ISectionShapeItem> _shapeInner = new ObservableCollection<XEP_ISectionShapeItem>();
        public static readonly string ShapeInnerPropertyName = "ShapeInner";
        public ObservableCollection<XEP_ISectionShapeItem> ShapeInner
        {
            get { return _shapeInner; }
            set { SetMemberWithAction<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _shapeInner, () => _shapeInner != value, Intergrity, ShapeInnerPropertyName); }
        }

        public void Intergrity(string propertyCallerName)
        {
            foreach (var item in Data)
            {
                item.Value = MathUtils.CorrectOnRange(item.Value, 0.0, double.MaxValue);
            }
            PolygonMode.VisibleState = Visibility.Visible;
            HoleMode.VisibleState = Visibility.Visible;
            HoleMode.IsReadOnly = false;
            PolygonMode.IsReadOnly = false;
            if (PolygonMode.IsTrue())
            {
                H.Value = 0.0; B.Value = 0.0; Hhole.Value = 0.0; Bhole.Value = 0.0;
                H.VisibleState = Visibility.Collapsed;
                B.VisibleState = Visibility.Collapsed;
                Hhole.VisibleState = Visibility.Collapsed;
                Bhole.VisibleState = Visibility.Collapsed;
                H.IsReadOnly = true;
                B.IsReadOnly = true;
                Hhole.IsReadOnly = true;
                Bhole.IsReadOnly = true;
            }
            else
            {
                PolygonMode.VisibleState = Visibility.Collapsed;
                if (H.Value <= 0)
                {
                    H.Value = 0.5;
                }
                if (B.Value <= 0)
                {
                    B.Value = 0.3;
                }
                if (Bhole.Value <= 0)
                {
                    Bhole.Value = 0.0;
                }
                if (Hhole.Value <= 0)
                {
                    Hhole.Value = 0.0;
                }
                H.VisibleState = Visibility.Visible;
                B.VisibleState = Visibility.Visible;
                Hhole.VisibleState = Visibility.Visible;
                Bhole.VisibleState = Visibility.Visible;
                H.IsReadOnly = false;
                B.IsReadOnly = false;
                Hhole.IsReadOnly = false;
                Bhole.IsReadOnly = false;
                _shapeOuter = XEP_ViewModelHelp.CreateRectShape(_resolver, B.Value / 2.0, H.Value / 2.0, true);
                _shapeInner = XEP_ViewModelHelp.CreateRectShape(_resolver, Bhole.Value / 2.0, Hhole.Value / 2.0, false);
            }
            if (!HoleMode.IsTrue())
            {
                Hhole.VisibleState = Visibility.Collapsed;
                Bhole.VisibleState = Visibility.Collapsed;
                HoleMode.VisibleState = Visibility.Collapsed;
                Hhole.IsReadOnly = true;
                Bhole.IsReadOnly = true;
                Hhole.Value = 0.0; Bhole.Value = 0.0;
                _shapeInner.Clear();
            }
            _cssShape.RecreateShape(_shapeOuter, _shapeInner);
            _cssShape = CssShape.Clone() as XEP_ICssDataShape;
            foreach (var item in _shapeOuter)
            {
                item.NotificationData.Owner = this;
                foreach (var oneQunatity in item.Data)
                {
                    oneQunatity.IsReadOnly = !PolygonMode.IsTrue();
                }
            }
            foreach (var item in _shapeInner)
            {
                item.NotificationData.Owner = this;
                foreach (var oneQunatity in item.Data)
                {
                    oneQunatity.IsReadOnly = !PolygonMode.IsTrue();
                }
            }
            _description = CreateDescription();
            // Raise all
            foreach (var item in Data)
            {
                RaisePropertyChanged(item.Name);
            }
            RaisePropertyChanged(CssShapePropertyName);
            RaisePropertyChanged(ShapeOuterPropertyName);
            RaisePropertyChanged(ShapeInnerPropertyName);
            RaisePropertyChanged(DescriptionPropertyName);
        }
        #endregion

        #region METHODS
        string CreateDescription()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append((PolygonMode.IsTrue()) ? ("Polygon") : ("Rectangle: "));
            if (!PolygonMode.IsTrue())
            {
                builder.Append(BPropertyName + "= ");
                builder.Append(B.ManagedValue.ToString());
                builder.Append(", ");
                //
                builder.Append(HPropertyName + "= ");
                builder.Append(H.ManagedValue.ToString());
            }
            return builder.ToString();
            
        }
        #endregion
	
        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return (notificationData) =>
                {
                    if (notificationData != null && notificationData.Owner != this)
                    {
                        Exceptions.CheckNull(null);
                    }
                    _cssShape.RecreateShape(_shapeOuter, _shapeInner);
                    _cssShape = CssShape.Clone() as XEP_ICssDataShape;
                    RaisePropertyChanged(CssShapePropertyName);
                    if (notificationData.Notifier != null)
                    {
                        XEP_ISectionShapeItem notifier = notificationData.Notifier as XEP_ISectionShapeItem;
                        if (notifier !=null)
                        {
                            if (notifier.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eOuter)
                            {
                                RaisePropertyChanged(ShapeOuterPropertyName);
                            }
                            else if (notifier.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eInner)
                            {
                                RaisePropertyChanged(ShapeInnerPropertyName);
                            }
                        }
                    }
                };
        }
        string _name = "Section shape";
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
        XEP_IXmlWorker _xmlWorker = null;
        public XEP_IXmlWorker XmlWorker
        {
            get { return _xmlWorker; }
            set { _xmlWorker = value; }
        }
        XEP_IQuantityManager _manager = null;
        public XEP_IQuantityManager Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        #endregion


    }
}
