using System;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IInternalForceItem : XEP_IDataCacheObjectBase
    {
        XEP_IInternalForceItem CopyInstance();
        XEP_IQuantity GetItem(eEP_ForceType type);
        XEP_IQuantity GetMax();
        XEP_IQuantity GetMin();
        string GetString();
        double MaxValue {get; set;}
        double MinValue {get; set;}
        string ShortExplanation {get; set;}
        bool UsedInCheck {get; set;}
        eEP_ForceItemType Type { get; set; }
        XEP_IQuantity N { get; set; }
        XEP_IQuantity Vy { get; set; }
        XEP_IQuantity Vz { get; set; }
        XEP_IQuantity Mx { get; set; }
        XEP_IQuantity My { get; set; }
        XEP_IQuantity Mz { get; set; }
    }
}
