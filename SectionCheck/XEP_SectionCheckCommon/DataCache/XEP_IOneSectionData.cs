using System;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Interfaces;
using System.Windows.Media;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneSectionData : XEP_IDataCacheObjectBase, XEP_IContainerHolder
    {
        Guid Id { get; set; }
        ObservableCollection<XEP_IInternalForceItem> InternalForces { get; set; }
        XEP_ISectionShape SectionShape { get; set; }
    }
}
