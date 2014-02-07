using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace XEP_SectionCheckInterfaces.DataCache
{
    public interface XEP_IMaterialData : XEP_IDataCacheObjectBase, ICloneable
    {
        ObservableCollection<XEP_IESDiagramItem> StressStrainDiagram { get; set; }
        XEP_IQuantity DiagramType { get; set; }
        XEP_IQuantity MatFromLibMode { get; set; }
        XEP_IQuantity MaterialName { get; set; }
        // Methods
        void CreatePoints(XEP_ISetupParameters setup);
        void ResetMatFromLib();
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
