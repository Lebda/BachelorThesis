using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class VisualObjectData
    {
        public delegate void DrawDelegate(VisualObjectData visual);

        DrawDelegate _callBack4ShapeChange = null;
        public DrawDelegate CallBack4ShapeChange
        {
            get { return _callBack4ShapeChange; }
            set { _callBack4ShapeChange = value; }
        }
        DrawDelegate _delagate4Draw = null;
        public DrawDelegate Delagate4Draw
        {
            get { return _delagate4Draw; }
            set { _delagate4Draw = value; }
        }
        IVisualShapes _visualShape = VisualFactory.Instance().CreateShapes();
        public IVisualShapes VisualShape
        {
            get { return _visualShape; }
            set { _visualShape = value; }
        }
        Visual _visual = new DrawingVisual();
        public System.Windows.Media.DrawingVisual VisualObject
        {
            get { return (DrawingVisual)_visual; }
            set { _visual = value; }
        }
        //
        public VisualObjectData(Visual visual)
        {
            _visual = visual;
        }
        public VisualObjectData()
        {
        }
        //
        public void Draw()
        {
            if (_delagate4Draw == null)
            {
                return;
            }
            _delagate4Draw(this);
        }
        //
        public void CallBack4ShapeChanged()
        {
            if (CallBack4ShapeChange == null)
            {
                return;
            }
            CallBack4ShapeChange(this);
        }
        //
//         public Geometry CreateRenderedGeometry(int pos, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
//         {
//             if (_visualShape == null)
//             {
//                 return null;
//             }
//             return _visualShape.CreateRenderedGeometry(pos, rule, isCLosed);
//         }
//         //
//         public Geometry CreateRenderedGeometry(int pos1, int pos2, FillRule rule = FillRule.Nonzero, bool isCLosed = true, GeometryCombineMode mode = GeometryCombineMode.Exclude)
//         {
//             if (_visualShape == null)
//             {
//                 return null;
//             }
//             CombinedGeometry combinedGeometry = new CombinedGeometry(_visualShape.CreateRenderedGeometry(pos1, rule, isCLosed), _visualShape.CreateRenderedGeometry(pos2, rule, isCLosed));
//             combinedGeometry.GeometryCombineMode = mode;
//             return combinedGeometry;
//         }
    }
}
