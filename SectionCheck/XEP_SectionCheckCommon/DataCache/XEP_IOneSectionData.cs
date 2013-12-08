using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IOneSectionData
    {
        Guid Id { get; }
        string SectionName { get; set; }
        ObservableCollection<XEP_IInternalForceItem> InternalForces { get; set; }
    }
}
