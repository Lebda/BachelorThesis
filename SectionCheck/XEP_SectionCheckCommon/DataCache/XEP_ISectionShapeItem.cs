using System;
using System.Linq;
using System.Windows;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_ISectionShapeItem : XEP_IDataCacheObjectBase
    {
        Point Point { get; set; }
        eEP_CssShapePointType Type { get; set; }
    }
}
