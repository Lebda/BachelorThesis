using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneMemberData
    {
        XEP_IOneSectionData GetOneSectionData(Guid guid);
        Dictionary<Guid, XEP_IOneSectionData> GetSectionsData();
        eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData);
        eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData);
        Guid Id { get; }
    }
}
