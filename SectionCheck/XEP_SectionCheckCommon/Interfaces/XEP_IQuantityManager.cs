using System;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.DataCache;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IQuantityManager
    {
        double GetValue(XEP_IQuantity source);
        string GetName(XEP_IQuantity source);
        double GetValueManaged(double value, eEP_QuantityType type);
        string GetNameWithUnit(XEP_IQuantity source);
        void SetScale(eEP_QuantityType type, double scaleValue);
    }
}
