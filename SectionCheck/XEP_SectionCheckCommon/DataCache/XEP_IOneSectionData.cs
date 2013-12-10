using System;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneSectionData : XEP_IDataCacheObjectBase, XEP_IContainerHolder
    {
        Guid Id { get; set; }
        ObservableCollection<XEP_IInternalForceItem> InternalForces { get; set; }
    }
}
