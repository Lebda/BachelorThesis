using System;
using System.Linq;
using XEP_SectionCheckCommon.Interfaces;
using System.Collections.ObjectModel;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCacheObjectBase
    {
        string Name { get; set; }
        Guid Id { get; set; }
        ObservableCollection<XEP_IQuantity> Data { get; set; }
        bool CallPropertySet4NewManagedValue(string propertyName, double newValue);
        void NotifyOwnerProperty(XEP_IDataCacheNotificationData notificationData);
        Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction();
        XEP_IXmlWorker XmlWorker { get; set; }
        XEP_IQuantityManager Manager { get; set; }
    }
}
