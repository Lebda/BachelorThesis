using System;
using System.Linq;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCache : XEP_IDataCacheObjectBase, XEP_IContainerHolder
    {
        XEP_IStructure Structure { get; set; }
    }
}
