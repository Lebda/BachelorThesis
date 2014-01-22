using System;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IMaterialData : XEP_IDataCacheObjectBase
    {
        ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram { get; set; }
        eEP_MaterialDiagramType DiagramType { get; set; }
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
