using System;
using System.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCache : XEP_IDataCacheObjectBase
    {
        XEP_IStructure Structure { get; set; }
    }
}
