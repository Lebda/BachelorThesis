using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class ContextVisualShape
    {
        public ContextVisualShape(List<IVisualShapeItem> shape, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
        {
            _items = shape;
            _rule = rule;
            _isCLosed = isCLosed;
        }
        FillRule _rule = FillRule.Nonzero;
        public FillRule Rule
        {
            get { return _rule; }
            set { _rule = value; }
        }
        bool _isCLosed = true;
        public bool IsCLosed
        {
            get { return _isCLosed; }
            set { _isCLosed = value; }
        }
        List<IVisualShapeItem> _items;
        public List<IVisualShapeItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

        public static bool CheckContext(ContextVisualShape context)
        {
            if (context == null || context.Items == null || context.Items.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
    //
    public interface IVisualShapeItem
    {
        IVisualShapeItem Clone();
        void Transform(Matrix conventer);
        Point Pos { get; set; }
        double ValueInPos { get; set; }
    }
    //
    public interface IVisualShape
    {
        IVisualShape Clone();
        void Transform(Matrix conventer);
        PathGeometry CreateGeometry(FillRule rule = FillRule.Nonzero, bool isCLosed = true);
        List<IVisualShapeItem> Items { get; set; }
    }
    //
    public interface IVisualShapes
    {
        PathGeometry BaseGeo { get; set; }
        PathGeometry RenderedGeo { get; }
        void UpdateRenderedGeometry(MatrixTransform conventer);
    }

    public interface IPathGeometryCreator
    {
        PathGeometry Create();
    }

    public interface IVisualObejctDrawingData
    {
        Pen GetPen();
        Brush GetBrush();
    }
}
