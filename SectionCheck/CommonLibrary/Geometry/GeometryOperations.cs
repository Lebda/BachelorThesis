using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows;
using CommonLibrary.Utility;

namespace CommonLibrary.Geometry
{
    public static class GeometryOperations
    {
        static private readonly Vector s_oneUnitHorVector = new Vector(1.0, 0.0);
        public static Vector S_oneUnitHorVector
        {
            get { return s_oneUnitHorVector; }
        }
        static private readonly Vector s_oneUnitVerVector = new Vector(0.0, 1.0);
        public static Vector S_oneUnitVerVector
        {
            get { return s_oneUnitVerVector; }
        }
        static public void TransformOne(Matrix conventer, PathFigureCollection figures)
        {
            Exceptions.CheckNull(figures);
            for (int counter = 0; counter < figures.Count; ++counter)
            {
                TransformOne(conventer, Exceptions.CheckNull(figures[counter]));
            }
        }

        static public void TransformOne(Matrix conventer, PathFigure figure)
        {
            Exceptions.CheckNull(figure);
            figure.StartPoint = TransformOne(conventer, figure.StartPoint);
            TransformOne(conventer, Exceptions.CheckNull(figure.Segments));
        }

        static public void TransformOne(Matrix conventer, PathSegmentCollection segments)
        {
            Exceptions.CheckNull(segments);
            for (int counter = 0; counter < segments.Count; ++counter)
            {
                TransformOne(conventer, Exceptions.CheckNull(segments[counter] as PolyLineSegment));
            }
        }

        static public void TransformOne(Matrix conventer, PolyLineSegment poly)
        {
            Exceptions.CheckNull(Exceptions.CheckNull<PolyLineSegment>(poly).Points);
            for (int counter = 0; counter < poly.Points.Count; ++counter)
            {
                poly.Points[counter] = TransformOne(conventer, poly.Points[counter]);
            }
        }

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
        public static double Add4Sign(double val1, double val2)
        {
            return (val1 + ((val1 > 0.0) ? (val2) : (-val2)));
        }
        public static double Take4Sign(double val1, double val2)
        {
            return Add4Sign(val1, -val2);
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
            return Exceptions.CheckNull(new Point(source.X, source.Y));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="direction">in [rad]</param>
        /// <returns></returns>
        public static Vector RotateVector(Vector vec, double directionRadians)
        {
            Vector tmp = new Vector(vec.X, vec.Y);
            double si = Math.Sin(directionRadians);
            double co = Math.Cos(directionRadians);
            return new Vector((tmp.X*co - tmp.Y*si), (tmp.X*si + tmp.Y*co));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">in [rad]</param>
        /// <returns></returns>
        public static Vector CreateVectorFromAngle(double directionRadians)
        {
            return RotateVector(s_oneUnitHorVector, directionRadians);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="directionRadians">in [rad]</param>
        /// <returns></returns>
        public static Vector Create(double size, double directionRadians)
        {
            return RotateVector(new Vector(size, 0.0), directionRadians);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns>Angle in radians</returns>
        public static double AngleFromHorLine(Vector vec)
        {
            return MathUtils.ToRad(Vector.AngleBetween(s_oneUnitHorVector, vec));
        }

        public static Vector GetPerpendicularVector(Vector vec)
        {
            return new Vector(vec.Y, -vec.X);
        }
    }
}
