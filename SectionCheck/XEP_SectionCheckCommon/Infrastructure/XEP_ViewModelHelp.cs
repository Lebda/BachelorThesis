using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Infrastructure
{
    static public class XEP_ViewModelHelp
    {
        public static XEP_IQuantity SetWithDeepCopy(XEP_IQuantity target, double value)
        {
            XEP_IQuantity copy = DeepCopy.Make<XEP_IQuantity>(target);
            copy.Value = value;
            return copy;
        }
    }
}
