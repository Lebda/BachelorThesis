using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using CommonLibrary.Utility;

namespace SectionCheckInterfaces.Interfaces
{
    public interface ISectionShapeService
    {
        PathGeometry GetOuterSectionShape();
        PathGeometry GetInnerSectionShape();
        CombinedGeometry GetWholeSectionShape();
        GeometryGroup GetReinforcementShape();
        void Prepare();
        PathGeometry GetTestShape();
        CanvasDataContext CanvasData {get; set;}
        ISectionShape ShapeModel { get; set; }
    }
}
