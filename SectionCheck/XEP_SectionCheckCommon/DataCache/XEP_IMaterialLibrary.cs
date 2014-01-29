using System;
using System.Collections.Generic;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IMaterialLibrary : XEP_IDataCacheObjectBase
    {
        Dictionary<string, XEP_IMaterialDataConcrete> MaterialDataConcrete { get; set; }
        List<XEP_IMaterialDataConcrete> GetMaterialConcreteNames { get; }
        XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete { get; }
        XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(string matName);
        eDataCacheServiceOperation SaveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
        eDataCacheServiceOperation RemoveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
    }
}
