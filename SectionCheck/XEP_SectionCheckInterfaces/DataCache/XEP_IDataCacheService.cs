using System;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IDataCacheService
    {
        eDataCacheServiceOperation Load(XEP_IDataCache dataCache);
        eDataCacheServiceOperation Save(XEP_IDataCache dataCache);
        string AplicationFolderPathFullName { get; set; }
        string FolderName { get; set; }
        string FileName { get; set; }
        string MaterialLibraryName { get; set; }
    }
}
