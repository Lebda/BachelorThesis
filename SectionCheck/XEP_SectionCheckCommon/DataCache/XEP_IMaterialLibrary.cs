using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IMaterialLibrary : XEP_IDataCacheObjectBase
    {
        ObservableCollection<XEP_IMaterialDataConcrete> MaterialDataConcrete { get; set; }
        XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete { get; }
        XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(string matName);
        eDataCacheServiceOperation SaveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
        eDataCacheServiceOperation RemoveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
    }
}
