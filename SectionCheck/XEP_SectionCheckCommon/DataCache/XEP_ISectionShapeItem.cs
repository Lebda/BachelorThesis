using System;
using System.Linq;
using System.Windows;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_ISectionShapeItem : XEP_IDataCacheObjectBase, ICloneable
    {
        XEP_IQuantity Y { get; set; }
        XEP_IQuantity Z { get; set; }
        eEP_CssShapePointType Type { get; set; }
        XEP_IDataCacheNotificationData NotificationData { get; }
    }
}
