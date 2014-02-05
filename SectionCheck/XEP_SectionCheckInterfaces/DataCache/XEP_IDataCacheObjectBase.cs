using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IDataCacheObjectBase
    {
        // Properties
        string Name { get; set; }
        Guid Id { get; set; }
        ObservableCollection<XEP_IQuantity> Data { get; set; }
        XEP_IXmlWorker XmlWorker { get; set; }
        // Methods
        void Intergrity(string propertyCallerName);
        bool CallPropertySet4NewManagedValue(string propertyName, double newValue);
        void NotifyOwnerProperty(XEP_IDataCacheNotificationData notificationData);
        Action<XEP_IDataCacheNotificationData> GetNotifyOwnerAction();
    }
}
