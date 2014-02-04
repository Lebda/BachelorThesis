using System;
using System.Linq;
using System.Windows;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_ISectionShapeItem : XEP_IDataCacheObjectBase, ICloneable
    {
        XEP_IQuantity Y { get; set; }
        XEP_IQuantity Z { get; set; }
        XEP_IQuantity PointType { get; set; }
        XEP_IDataCacheNotificationData NotificationData { get; }
    }
}
