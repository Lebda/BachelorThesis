using System;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IDataCache : XEP_IDataCacheObjectBase
    {
        XEP_IStructure Structure { get; set; }
        XEP_IMaterialLibrary MaterialLibrary { get; set; }
        XEP_ISetupParameters SetupParameters { get; set; }
        XEP_IEnum2StringManager Enum2StringManager { get; set; }
    }
}
