using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.Interfaces
{
    public interface XEP_ICssPropertiesService
    {
        ObservableCollection<XEP_InternalForceItem> GetInternalForces();
    }
}
