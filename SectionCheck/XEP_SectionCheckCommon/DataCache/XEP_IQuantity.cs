using System;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IQuantity : XEP_IDataCacheObjectBase
    {
        XEP_IDataCacheObjectBase Owner { get; set; }
        double Value { get; set; }
        double ManagedValue { get; set; }
        eEP_QuantityType QuantityType { get; set; }
        XEP_IQuantity CopyInstance();
        bool IsTrue();
        void SetBool(bool value);
    }
}
