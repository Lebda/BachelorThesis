using System;
using System.Linq;
using System.Xml.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    class XEP_SectionShapeItemXml : XEP_XmlWorkerImpl
    {
        public XEP_SectionShapeItemXml(XEP_ISectionShapeItem data)
            : base(data)
        {
        }
        #region XEP_XmlWorkerImpl Members
        public override string GetXmlElementName()
        {
            return "XEP_SectionShapeItem";
        }
        protected override void LoadElements(XElement xmlElement)
        {
            return;
        }
        #endregion
    }

    public class XEP_SectionShapeItem : XEP_ObservableObject, XEP_ISectionShapeItem
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolver = null;

        public XEP_SectionShapeItem(XEP_IResolver<XEP_ISectionShapeItem> resolver, XEP_IResolver<XEP_IDataCacheNotificationData> notificationDataRes)
        {
            _notificationData = notificationDataRes.Resolve();
            _resolver = resolver;
            _xmlWorker = new XEP_SectionShapeItemXml(this);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, YPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eCssLength, ZPropertyName);
            AddOneQuantity(0.0, eEP_QuantityType.eEnum, TypePropertyName);
            Intergrity(null);

        }
        #region ICloneable Members
        public object Clone()
        {  // do not copy owner has to be set from outside !
            XEP_ISectionShapeItem copy = _resolver.Resolve();
            copy.Name = _name;
            XEP_SectionShapeItem copyDescendant = copy as XEP_SectionShapeItem;
            copyDescendant.CopyAllQuanties(this, copy);
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
            set { SetItemWithActions(ref value, YPropertyName, () => !Y.Equals(value), Intergrity); }
        }
        public static readonly string ZPropertyName = "Z";
        public XEP_IQuantity Z
        {
            get { return GetOneQuantity(ZPropertyName); }
            set { SetItemWithActions(ref value, ZPropertyName, () => !Z.Equals(value), Intergrity); }
        }
        public static readonly string TypePropertyName = "Type";
        public XEP_IQuantity PointType
        {
            get { return GetOneQuantity(TypePropertyName); }
            set { SetItemWithActions(ref value, TypePropertyName, () => !PointType.Equals(value), Intergrity); }
        }
        #endregion

        #region METHODS
        #endregion

        #region XEP_IDataCacheObjectBase Members
        public void Intergrity(string propertyCallerName)
        {
            // Check object integrity

            // Notify owner
            if (_notificationData != null)
            {
                _notificationData.Notifier = this;
                _notificationData.PropertyNotifier = propertyCallerName;
                NotifyOwnerProperty(_notificationData);
            }
        }
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
        #endregion
    }
}
