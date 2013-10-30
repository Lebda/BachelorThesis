using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using ShapeType = System.Collections.Generic.List<System.Windows.Media.PointCollection>;
using ShapeGeometry = System.Collections.Generic.List<System.Windows.Media.Geometry>;
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
        Visual _visual = null;
        public System.Windows.Media.DrawingVisual VisualObject
        {
            get { return (DrawingVisual)_visual; }
            set { _visual = value; }
        }
        ShapeType _shapeObjects = new ShapeType();
        public ShapeType ShapeObjects
        {
            get { return _shapeObjects; }
        }
        ShapeType _shapeRendered = new ShapeType();
        public ShapeType ShapeRendered
        {
            get { return _shapeRendered; }
        }
        ShapeGeometry _shapeGeometry = new ShapeGeometry();
        public ShapeGeometry ShapeGeometry
        {
          get { return _shapeGeometry; }
        }
        //
        public VisualObjectData(Visual visual, ShapeType shapeObject, ShapeGeometry shapeGeometry)
        {
            _visual = visual;
            _shapeObjects = shapeObject; 
            _shapeGeometry = shapeGeometry;
        }
        public VisualObjectData(Visual visual)
        {
            _visual = visual;
        }
        public VisualObjectData(Visual visual, DrawDelegate delegate4Draw)
        {
            _visual = visual;
            _delagate4Draw = delegate4Draw;
        }
        public VisualObjectData()
        {
            _visual = new DrawingVisual();
        }
        //
        public void SetShapesAndCreateGeometry(ShapeType shapeObject, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
        {
            foreach(PointCollection iter in shapeObject)
            {
                _shapeObjects.Add(iter);
                _shapeGeometry.Add(CreateGeometryFromPolygon(iter, rule, isCLosed));
            }
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
        public void Transform(Matrix convemter)
        {
            _shapeRendered.Clear();
            foreach (PointCollection shape in _shapeObjects)
            {
                PointCollection renderedShape = new PointCollection(shape);
                GeometryOperations.TransformOne(convemter, renderedShape);
                _shapeRendered.Add(renderedShape);
            }
        }
        //
        public static PathGeometry CreateGeometryFromPolygon(PointCollection polygon, FillRule rule = FillRule.Nonzero, 
            bool isCLosed = true)
        {
            if (polygon == null ||polygon.Count == 0)
            {
                return null;
            }
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.IsClosed = isCLosed;
            myPathFigure.StartPoint = new Point(polygon[0].X, polygon[0].Y);
            //
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            int countStop = polygon.Count;
            for (int counter = 1; counter < countStop; ++counter)
            {
                LineSegment myLineSegment = new LineSegment();
                myLineSegment.Point = new Point(polygon[counter].X, polygon[counter].Y);
                myPathSegmentCollection.Add(myLineSegment);
            }
            myPathFigure.Segments = myPathSegmentCollection;
            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;
            myPathGeometry.FillRule = rule;
            return myPathGeometry;
        }

    }
}
