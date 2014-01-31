using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_IMaterialData : XEP_IDataCacheObjectBase
    {
        ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram { get; set; }
        eEP_MaterialDiagramType DiagramType { get; set; }
        bool MatFromLib { get; set; }
        void CreatePoints(XEP_ISetupParameters setup);
        void ResetMatFromLib();
        XEP_IMaterialData CopyInstance();
    }

    public interface XEP_IMaterialDataConcrete : XEP_IMaterialData
    {
        XEP_IQuantity Fck { get; set; }
        XEP_IQuantity FckCube { get; set; }
        XEP_IQuantity EpsC1 { get; set; }
        XEP_IQuantity EpsCu1 { get; set; }
        XEP_IQuantity EpsC2 { get; set; }
        XEP_IQuantity EpsCu2 { get; set; }
        XEP_IQuantity EpsC3 { get; set; }
        XEP_IQuantity EpsCu3 { get; set; }
        XEP_IQuantity N { get; set; }
    }
}
