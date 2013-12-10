using System;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IQuantity : XEP_IDataCacheObjectBase
    {
        double Value { get; set;}
        eEP_QuantityType QuantityType { get; set;}
    }
}
