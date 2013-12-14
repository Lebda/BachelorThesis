using System;
using System.Collections.Generic;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IStructure : XEP_IDataCacheObjectBase
    {
        void Clear();
        XEP_IOneMemberData GetOneMemberData(Guid guid);
        Dictionary<Guid, XEP_IOneMemberData> MemberData{ get; set; }
        eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData);
        eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData);
    }
}
