using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IDataCache : XEP_IXmlWorker
    {
        XEP_IStructure Structure { get; set; }
    }
}
