﻿using System;
using System.Collections.Generic;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;
using System.Collections.ObjectModel;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneMemberData : XEP_IDataCacheObjectBase
    {
        XEP_IOneSectionData GetOneSectionData(Guid guid);
        ObservableCollection<XEP_IOneSectionData> SectionsData { get; set; }
        eDataCacheServiceOperation SaveOneSectionData(XEP_IOneSectionData sectionData);
        eDataCacheServiceOperation RemoveOneSectionData(XEP_IOneSectionData sectionData);
    }
}
