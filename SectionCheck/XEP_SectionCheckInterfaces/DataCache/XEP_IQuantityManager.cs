using System;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
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
