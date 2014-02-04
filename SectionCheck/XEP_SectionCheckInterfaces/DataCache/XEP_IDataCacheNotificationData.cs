using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IDataCacheNotificationData
    {
        XEP_IDataCacheObjectBase Owner { get; set; }
        XEP_IDataCacheObjectBase Notifier { get; set; }
        string PropertyNotifier { get; set; }
    }

}
