using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Infrastructure;
using CommonLibrary.Utility;

namespace SectionDrawerControl.Utility
{
    public static class GeometryOperations
    {
        static public void TransformOne(Matrix conventer, PointCollection shape)
        {
            for (int counter = 0; counter < shape.Count; ++counter)
            {
                shape[counter] = TransformOne(conventer, shape[counter]);
            }
        }
        static public Point TransformOne(Matrix conventer, Point shapePoint)
        {
            return conventer.Transform(shapePoint);
        }
        static public void TransformOne(Matrix conventer, ref IVisualShapeItem item)
        {
            item.Pos = TransformOne(conventer, item.Pos);
            item.ValueInPos = (TransformOne(conventer, new Point(item.ValueInPos, 0.0))).X;
        }
        public static double Add4Sign(double val1, double val2)
        {
            return (val1 + ((val1 > 0.0) ? (val2) : (-val2)));
        }
        public static double Take4Sign(double val1, double val2)
        {
            return Add4Sign(val1, -val2);
        }
        //
        public static PathGeometry CreateGeometry(List<IVisualShapeItem> shape, FillRule rule = FillRule.Nonzero, bool isCLosed = true)
        {
            if (shape == null || shape.Count == 0 || shape[0] == null || shape[0].Pos == null)
            {
                return null;
            }
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.IsClosed = isCLosed;
            myPathFigure.StartPoint = new Point(shape[0].Pos.X, shape[0].Pos.Y);
            //
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            int countStop = shape.Count;
            for (int counter = 1; counter < countStop; ++counter)
            {
                LineSegment myLineSegment = new LineSegment();
                myLineSegment.Point = new Point(shape[counter].Pos.X, shape[counter].Pos.Y);
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
        //
        public static bool IsEmpty(PointCollection points)
        {
            if (points != null && points.Count > 0)
            {
                return false;
            }
            return true;
        }
        public static Point Copy(Point source)
        {
            return new Point(source.X, source.Y);
        }
        public static PointCollection Copy(PointCollection source, int startPos = 0)
        {
            if (startPos == 0)
            {
                return new PointCollection(source);
            }
            Exceptions.CheckPredicate<int>(null, startPos, (start => start < 0));
            Exceptions.CheckPredicate<int, int>(null, startPos, source.Count, (start, itemCount) => itemCount <= start);
            PointCollection retVal = new PointCollection();
            for (int counter = startPos; counter < source.Count; ++counter)
            {
                retVal.Add(Copy(source[counter]));
            }
            return retVal;
        }
        public static PathFigure Create(PointCollection points)
        {
            PathFigure pathFigure = new PathFigure();
            if (GeometryOperations.IsEmpty(points))
            {
                return pathFigure;
            }
            pathFigure.StartPoint = GeometryOperations.Copy(points[0]);
            PolyLineSegment myPolyLineSegmentOuter = new PolyLineSegment();
            myPolyLineSegmentOuter.Points = GeometryOperations.Copy(points, 1);
            pathFigure.Segments.Add(myPolyLineSegmentOuter);
            return pathFigure;
        }
    }
}
