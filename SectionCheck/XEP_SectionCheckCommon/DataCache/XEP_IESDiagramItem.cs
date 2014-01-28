using System;
using System.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IESDiagramItem : XEP_IDataCacheObjectBase
    {
        XEP_IQuantity Strain { get; set; }
        XEP_IQuantity Stress { get; set; }
    }
}
