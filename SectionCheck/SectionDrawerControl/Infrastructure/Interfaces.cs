using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using XEP_SectionDrawer.Utility;

namespace XEP_SectionDrawer.Infrastructure
{
    public interface IVisualShapes
    {
        PathGeometry BaseGeo { get; set; }
        Matrix AdditionMatrix { get; set; }
        PathGeometry RenderedGeo { get; }
        void UpdateRenderedGeometry(MatrixTransform conventer);
        void UpdateBaseGeometry(MatrixTransform conventer);
    }

//     public interface IPathGeometryCreator
//     {
//         PathGeometry Create();
//     }
// 
//     public interface IVisualObejctDrawingData
//     {
//         Pen GetPen();
//         Brush GetBrush();
//     }
}
