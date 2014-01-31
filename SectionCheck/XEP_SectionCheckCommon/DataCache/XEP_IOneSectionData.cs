using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneSectionData : XEP_IDataCacheObjectBase
    {
        ObservableCollection<XEP_IInternalForceItem> InternalForces { get; set; }
        XEP_IConcreteSectionData ConcreteSectionData { get; set; }
    }
 
}
