using System;
using System.Linq;
using System.Windows.Media;

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
