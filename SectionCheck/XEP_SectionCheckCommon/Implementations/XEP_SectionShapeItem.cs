using System;
using System.Linq;
using System.Xml.Linq;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Infrastucture;
using XEP_SectionCheckCommon.Interfaces;
using XEP_Prism.Infrastructure;

namespace XEP_SectionCheckCommon.Implementations
{
    class XEP_SectionShapeItemXml : XEP_XmlWorkerImpl
    {
        readonly XEP_SectionShapeItem _data = null;
        public XEP_SectionShapeItemXml(XEP_SectionShapeItem data)
        {
            _data = data;
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SectionShapeItem";
        }
        protected override void AddAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            xmlElement.Add(new XAttribute(ns + XEP_Constants.NamePropertyName, _data.Name));
            xmlElement.Add(new XAttribute(ns + XEP_SectionShapeItem.TypePropertyName, (int)_data.Type));
            foreach (var item in _data.Data)
            {
                xmlElement.Add(new XAttribute(ns + item.Name, item.Value));
            }
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        protected override void LoadAtributes(XElement xmlElement)
        {
            XNamespace ns = XEP_Constants.XEP_SectionCheckNs;
            _data.Name = (string)xmlElement.Attribute(ns + XEP_Constants.NamePropertyName);
            _data.Type = (eEP_CssShapePointType)(int)xmlElement.Attribute(ns + XEP_SectionShapeItem.TypePropertyName);
            foreach (var item in _data.Data)
            {
                item.Value = (double)xmlElement.Attribute(ns + item.Name);
            }
        }
        #endregion
    }

    public class XEP_SectionShapeItem : XEP_ObservableObject, XEP_ISectionShapeItem
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolver = null;

        public XEP_SectionShapeItem(XEP_IQuantityManager manager, XEP_IResolver<XEP_ISectionShapeItem> resolver, XEP_IResolver<XEP_IDataCacheNotificationData> notificationDataRes)
        {
            _notificationData = notificationDataRes.Resolve();
            _resolver = resolver;
            _manager = manager;
            _xmlWorker = new XEP_SectionShapeItemXml(this);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, YPropertyName);
            AddOneQuantity(_manager, 0.0, eEP_QuantityType.eCssLength, ZPropertyName);

        }
        #region ICloneable Members
        public object Clone()
        {  // do not copy owner has to be set from outside !
            XEP_ISectionShapeItem copy = _resolver.Resolve();
            copy.Name = _name;
            XEP_SectionShapeItem copyDescendant = copy as XEP_SectionShapeItem;
            copyDescendant.CopyAllQuanties(this, copy);
            copy.Type = _type;
            return copy;
        }
        #endregion

        #region XEP_ISectionShapeItem Members
        readonly XEP_IDataCacheNotificationData _notificationData = null;
        public XEP_IDataCacheNotificationData NotificationData
        {
            get { return _notificationData; }
        }
        public static readonly string YPropertyName = "Y";
        public XEP_IQuantity Y
        {
            get { return GetOneQuantity(YPropertyName); }
            set { SetItemWithActions(ref value, YPropertyName, null, Intergrity); }
        }
        public static readonly string ZPropertyName = "Z";
        public XEP_IQuantity Z
        {
            get { return GetOneQuantity(ZPropertyName); }
            set { SetItemWithActions(ref value, ZPropertyName, null, Intergrity); }
        }
        eEP_CssShapePointType _type = eEP_CssShapePointType.eOuter;
        public static readonly string TypePropertyName = "Type";
        public eEP_CssShapePointType Type
        {
            get { return _type; }
            set { SetMember<eEP_CssShapePointType>(ref value, ref _type, (_type == value), TypePropertyName); }
        }
        #endregion

        #region METHODS
        void Intergrity(string propertyName)
        {
            // Check object integrity

            // Notify owner
            if (_notificationData != null)
            {
                _notificationData.Notifier = this;
                _notificationData.PropertyNotifier = propertyName;
                NotifyOwnerProperty(_notificationData);
            }
        }
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction()
        {
            return null;
        }
        string _name = "ShapePoint";
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
