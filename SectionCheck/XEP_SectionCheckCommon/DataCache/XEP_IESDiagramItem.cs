using System;
using System.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IESDiagramItem : XEP_IDataCacheObjectBase
    {
        double Strain { get; set; }
        double Stress { get; set; }
    }
}
