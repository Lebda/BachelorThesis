using System;
using System.Linq;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IMaterialData : XEP_IDataCacheObjectBase
    {

    }

    public interface XEP_IMaterialDataConcrete : XEP_IMaterialData
    {
        double Fck { get; set; }
        double FckCube { get; set; }
        double EpsC1 { get; set; }
        double EpsCu1 { get; set; }
        double EpsC2 { get; set; }
        double EpsCu2 { get; set; }
        double EpsC3 { get; set; }
        double EpsCu3 { get; set; }
        double N { get; set; }
    }
}
