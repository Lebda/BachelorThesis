using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Utility;
namespace SectionDrawerControl.Infrastructure
{
     public class VisualShapeItem : IVisualShapeItem
     {
        public VisualShapeItem (VisualShapeItem source)
	    {
            _pos = source.Pos;
            _valueInPos = source.ValueInPos;
	    }
        public VisualShapeItem(Point source)
        {
            _pos = source;
            _valueInPos = .0;
        }
        public VisualShapeItem(double horPos, double verPos, double valueInPos = 0.0)
        {
            _pos = new Point(horPos, verPos);
            _valueInPos = valueInPos;
        }
        Point _pos = new Point();
        public Point Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        double _valueInPos = 0.0;
        public double ValueInPos
        {
            get { return _valueInPos; }
            set { _valueInPos = value; }
        }
        public IVisualShapeItem Clone()
        {
            return VisualFactory.Instance().CreateItem(_pos.X, _pos.Y, _valueInPos);
        }
        public void Transform(Matrix conventer)
        {
            _pos = GeometryOperations.TransformOne(conventer, Pos);

            _valueInPos *= 1;
        }
    }
    //
     public class VisualShape : IVisualShape
     {

         #region IVisualShape Members

         public IVisualShape Clone()
         {
             IVisualShape clone = VisualFactory.Instance().CreateShape();
             foreach (IVisualShapeItem iter in _items)
             {
                 clone.Items.Add(iter.Clone());
             }
             return clone;
         }

         public void Transform(Matrix conventer)
         {
             foreach (IVisualShapeItem iter in _items)
             {
                 iter.Transform(conventer);
             }
         }

         public PathGeometry CreateGeometry(FillRule rule = FillRule.Nonzero, bool isCLosed = true)
         {
             return GeometryOperations.CreateGeometry(_items, rule, isCLosed);
         }

         List<IVisualShapeItem> _items = new List<IVisualShapeItem>();
         public List<IVisualShapeItem> Items
         {
             get { return _items; }
             set { _items = value; }
         }
         #endregion
     }
    //
    public class VisualShapes : IVisualShapes
    {
//         #region IVisualShapes
//         List<IVisualShape> _items = new List<IVisualShape>();
//         public List<IVisualShape> Items
//         {
//             get { return _items; }
//             protected set { _items = value; }
//         }
//         List<IVisualShape> _itemsRendered = new List<IVisualShape>();
//         public List<IVisualShape> ItemsRendered
//         {
//             get { return _itemsRendered; }
//             protected set { _itemsRendered = value; }
//         }
//         List<Geometry> _itemsGeometry = new List<Geometry>();
//         public List<Geometry> ItemsGeometry
//         {
//             get { return _itemsGeometry; }
//             protected set { _itemsGeometry = value; }
//         }
// 
//         public List<IVisualShape> Copy(List<IVisualShape> source)
//         {
//             List<IVisualShape> clone = new List<IVisualShape>();
//             foreach(IVisualShape iter in source)
//             {
//                 clone.Add(iter.Clone());
//             }
//             return clone;
//         }
// 
//         public void Transform(Matrix conventer)
//         {
//             _itemsRendered = Copy(_items);
//             foreach (IVisualShape iter in _itemsRendered)
//             {
//                 iter.Transform(conventer);
//             }
//         }
// 
//         public List<Geometry> CreateGeometry(List<IVisualShape> shape, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
//         {
//             List<Geometry> geometry = new List<Geometry>();
//             foreach (IVisualShape iter in shape)
//             {
//                 geometry.Add(iter.CreateGeometry(rule, isCLosed));
//             }
//             return geometry;
//         }
// 
//         public void SetShapeAndCreateGeometry(List<IVisualShape> shape, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
//         {
//             _items = Copy(shape);
//             _itemsGeometry = CreateGeometry(shape, rule, isCLosed);
//         }
// 
//         public Geometry CreateRenderedGeometry(int pos, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
//         {
//             if (_itemsRendered == null || _itemsRendered.Count <= pos || _itemsRendered[pos] == null)
//             {
//                 return null;
//             }
//             return _itemsRendered[pos].CreateGeometry(rule, isCLosed);
//         }
//         #endregion

        #region IVisualShapes Members
        PathGeometry _baseGeo = new PathGeometry();
        public PathGeometry BaseGeo
        {
            get { return _baseGeo; }
            set { _baseGeo = value; }
        }
        PathGeometry _renderedGeo = new PathGeometry();
        public System.Windows.Media.PathGeometry RenderedGeo
        {
            get { return _renderedGeo; }
            set { _renderedGeo = value; }
        }

        void IVisualShapes.UpdateRenderedGeometry(MatrixTransform conventer)
        {
            _renderedGeo = new PathGeometry(_baseGeo.Figures, _baseGeo.FillRule, conventer);
        }
        #endregion
    }
    //
    public class VisualFactory
    {
        static VisualFactory _visualFactory = new VisualFactory();
        public static VisualFactory Instance()
        {
            if (_visualFactory == null)
            {
                _visualFactory = new VisualFactory();
            }
            return  _visualFactory;
        }
        protected VisualFactory ()
	    {
	    }
        public IVisualShapeItem CreateItem(double horPos = 0.0, double verPos = 0.0, double valueInPos = 0.0)
        {
            return new VisualShapeItem(horPos, verPos, valueInPos);
        }
        public IVisualShape CreateShape()
        {
            return new VisualShape();
        }
        public IVisualShapes CreateShapes()
        {
            return new VisualShapes();
        }
    }

}
