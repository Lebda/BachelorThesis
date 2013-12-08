using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCacheService
    {
        eDataCacheServiceOperation Load(XEP_IDataCache dataCache);
        eDataCacheServiceOperation Save(XEP_IXmlWorker dataCache);
    }
}
