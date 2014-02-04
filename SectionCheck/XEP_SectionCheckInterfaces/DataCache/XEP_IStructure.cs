using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IStructure : XEP_IDataCacheObjectBase
    {
        void Clear();
        XEP_IOneMemberData GetOneMemberData(Guid guid);
        ObservableCollection<XEP_IOneMemberData> MemberData { get; set; }
        eDataCacheServiceOperation SaveOneMemberData(XEP_IOneMemberData memberData);
        eDataCacheServiceOperation RemoveOneMemberData(XEP_IOneMemberData memberData);
    }
}
