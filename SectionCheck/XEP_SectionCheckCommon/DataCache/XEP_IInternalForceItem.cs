using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IInternalForceItem : XEP_IDataCacheObjectBase
    {
        XEP_InternalForceItem CopyInstance();
        XEP_IQuantity GetItem(eEP_ForceType type);
        XEP_IQuantity GetMax();
        XEP_IQuantity GetMin();
        string GetString();
        double MaxValue {get; set;}
        double MinValue {get; set;}
        string ShortExplanation {get; set;}
        bool UsedInCheck {get; set;}
        [Required]
        eEP_ForceItemType Type { get; set; }
        [Required]
        XEP_IQuantity N { get; set; }
        XEP_IQuantity Vy { get; set; }
        XEP_IQuantity Vz { get; set; }
        XEP_IQuantity Mx { get; set; }
        XEP_IQuantity My { get; set; }
        XEP_IQuantity Mz { get; set; }
    }
}
