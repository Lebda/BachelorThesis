using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IQuantityManagerHolder
    {
        XEP_IQuantityManager Manager { get; set; }
    }
}
