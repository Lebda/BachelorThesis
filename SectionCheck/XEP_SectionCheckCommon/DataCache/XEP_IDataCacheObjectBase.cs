using System;
using System.Linq;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IDataCacheObjectBase
    {
        string Name { get; set; }
        XEP_IXmlWorker XmlWorker { get; set; }
        XEP_IQuantityManager Manager { get; set; }
    }
}
