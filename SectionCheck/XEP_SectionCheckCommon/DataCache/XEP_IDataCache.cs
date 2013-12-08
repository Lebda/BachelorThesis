using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_IDataCache
    {
        void Clear();
        XEP_IOneMemberData GetOneMemberData(Guid guid);
        Dictionary<Guid, XEP_IOneMemberData> GetMemberData();
        eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData);
        eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData);
    }
}
