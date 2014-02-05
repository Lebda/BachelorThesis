using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IMaterialLibrary : XEP_IDataCacheObjectBase
    {
        // Properties
        ObservableCollection<XEP_IMaterialDataConcrete> MaterialDataConcrete { get; set; }
        XEP_IResolver<XEP_IMaterialDataConcrete> ResolverMatConcrete { get; }
        // Methods
        XEP_IMaterialDataConcrete GetOneMaterialDataConcrete(string matName);
        eDataCacheServiceOperation SaveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
        eDataCacheServiceOperation RemoveOneMaterialDataConcrete(XEP_IMaterialDataConcrete matData);
    }
}
