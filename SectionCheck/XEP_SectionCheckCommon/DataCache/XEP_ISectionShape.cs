using System;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionCheckCommon.DataCache
{
    public interface XEP_ISectionShape : XEP_IDataCacheObjectBase
    {
        XEP_ICssDataShape CssShape { get; set; }
        ObservableCollection<XEP_ISectionShapeItem> ShapeOuter { get; set; }
        ObservableCollection<XEP_ISectionShapeItem> ShapeInner { get; set; }
        XEP_IQuantity PolygonMode { get; set; }
        XEP_IQuantity H { get; set; }
        XEP_IQuantity B { get; set; }
        XEP_IQuantity HoleMode { get; set; }
        XEP_IQuantity Hhole { get; set; }
        XEP_IQuantity Bhole { get; set; }
        string Description { get; set; }
        void Intergrity(string propertyCallerName);
    }
}
