using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IQuantityManager
    {
        double GetValue(XEP_Quantity source);
        string GetString(XEP_Quantity source);
        void SetScale(eEP_QuantityType type, double scaleValue);
    }
}
