using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IStructure : XEP_IXmlWorker
    {
        void Clear();
        XEP_IOneMemberData GetOneMemberData(Guid guid);
        Dictionary<Guid, XEP_IOneMemberData> GetMemberData();
        eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData);
        eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData);
    }
}
