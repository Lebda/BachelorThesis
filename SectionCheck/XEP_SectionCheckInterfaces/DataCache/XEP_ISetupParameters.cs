using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_ISetupParameters : XEP_IDataCacheObjectBase
    {
        XEP_IQuantity GammaC { get; set; }
        XEP_IQuantity GammaS { get; set; }
        XEP_IQuantity AlphaCc { get; set; }
        XEP_IQuantity AlphaCt { get; set; }
        XEP_IQuantity Fi { get; set; }
        XEP_IQuantity FiEff { get; set; } 
    }
}
