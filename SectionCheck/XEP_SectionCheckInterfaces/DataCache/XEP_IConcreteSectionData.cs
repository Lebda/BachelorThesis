using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IConcreteSectionData : XEP_IDataCacheObjectBase
    {
        XEP_ISectionShape SectionShape { get; set; }
        XEP_IMaterialDataConcrete MaterialData { get; set; }
    }
}
