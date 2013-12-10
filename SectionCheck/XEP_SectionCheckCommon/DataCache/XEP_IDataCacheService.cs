using System;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCacheService
    {
        eDataCacheServiceOperation Load(XEP_IDataCache dataCache);
        eDataCacheServiceOperation Save(XEP_IDataCache dataCache);
    }
}
