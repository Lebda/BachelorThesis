using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_CommonLibrary.Infrastructure;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IQuantity : XEP_IQuantityManagerHolder
    {
        double Value { get; set;}
        eEP_QuantityType QuantityType { get; set;}
        string Name { get; set; }
    }
}
