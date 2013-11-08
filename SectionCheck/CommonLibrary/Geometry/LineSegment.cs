using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CommonLibrary.Utility;
using CommonLibrary.Interfaces;

namespace CommonLibrary.Geometry
{
    /// <summary>
    /// Abstraction for line 2D with formula Ax + By = C
    /// </summary>
    public class Line2D : ILine2D
    {
        #region CTORS
        public Line2D(Point start, Vector direction)
            : this(start, Point.Add(start, direction))
        {
        }

        public Line2D(Point start, double direction)
            : this(start, GeometryOperations.CreateVectorFromAngle(direction))
        {
        }

        public Line2D(Point start, Point end)
        {
            _startPoint = GeometryOperations.Copy(start);
            _endPoint = GeometryOperations.Copy(end);
            CheckDataAndRecalcNeccesary();
        }
        #endregion

        #region STATIC
        static Point? Intersection(ILine2D first, ILine2D second)
        {
            double det = first.A*second.B - second.A*first.B;
            Point intersection = new Point();
            if (MathUtils.CompareDouble(det, 0.0, _preccision))
            {//Lines are parallel
                return null;
            }
            else
            {
                intersection.X = (second.B * first.C - first.B * second.C)/det;
                intersection.Y = (first.A*second.C - second.A*first.C)/det;
            }
            if (first.IsPointOnLine(intersection) && second.IsPointOnLine(intersection))
            {
                return intersection;
            }
            return null;
        }
        #endregion

        #region METHODS

        public override string ToString()
        {
            return "Start: " + _startPoint.ToString() + ", End: " + _endPoint.ToString(); 
        }
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types. 
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Line2D test = (Line2D)obj;
                return _startPoint.Equals(test._startPoint) && _endPoint.Equals(test._endPoint) && _isLineSegment.Equals(test._isLineSegment);
            }  
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private void CheckDataAndRecalcNeccesary()
        {
            RecreateVector();
            RecalculateABC();
            Exceptions.CheckPredicate<Point, Point>(null, _startPoint, _endPoint, (first, second) => first.Equals(second));
        }
        static private readonly double _preccision = 1e-6;
        private void RecalculateABC()
        {
            _a = _endPoint.Y - _startPoint.Y;
            _b = _startPoint.X - _endPoint.X;
            _c = _a * _startPoint.X + _b * _startPoint.Y;
        }
        private void RecreateVector()
        {
            _lineVector = Point.Subtract(_endPoint, _startPoint);
        }
        #endregion

        #region ILine2D Members

        double _a = 0.0;
        public double A
        {
            get { return _a; }
            protected set { _a = value; }
        }
        double _b = 0.0;
        public double B
        {
            get { return _b; }
            protected set { _b = value; }
        }
        double _c = 0.0;
        public double C
        {
            get { return _c; }
            protected set { _c = value; }
        }
        bool _isLineSegment = false;
        public bool IsLineSegment
        {
            get { return _isLineSegment; }
            set { _isLineSegment = value; }
        }
        Point _startPoint = new Point();
        public Point StartPoint
        {
            get { return _startPoint; }
            set 
            { 
                _startPoint = value;
                CheckDataAndRecalcNeccesary();
            }
        }
        Point _endPoint = new Point();
        public Point EndPoint
        {
            get { return _endPoint; }
            set 
            { 
                _endPoint = value;
                CheckDataAndRecalcNeccesary();
            }
        }
        Vector _lineVector;
        public System.Windows.Vector LineVector
        {
            get { return _lineVector; }
            protected set { _lineVector = value; }
        }

        double ILine2D.LinePointDistance(Point testPoint)
        {
            Vector vecStart2TestPoint = Point.Subtract(testPoint, _startPoint);
            double dist = (Vector.CrossProduct(_lineVector, vecStart2TestPoint)) / Math.Sqrt(Vector.Multiply(_lineVector, _lineVector));
            if (IsLineSegment)
            {
                Vector vecEnd2Test = Point.Subtract(testPoint, _endPoint);
                double dot1 = Vector.Multiply(vecEnd2Test, _lineVector);
                if (MathUtils.IsGreaterThan(dot1, 0.0, _preccision))
                {
                    Vector vecTest2End = Point.Subtract(_endPoint, testPoint);
                    return Math.Sqrt(Vector.Multiply(vecTest2End, vecTest2End));
                }
                Vector vecEnd2Start = Point.Subtract(_startPoint, _endPoint);
                double dot2 = Vector.Multiply(vecStart2TestPoint, vecEnd2Start);
                if (MathUtils.IsGreaterThan(dot2, 0.0, _preccision))
                {
                    Vector vecTest2Start = Point.Subtract(_startPoint, testPoint);
                    return Math.Sqrt(Vector.Multiply(vecTest2Start, vecTest2Start));
                }
            }
            return dist;
        }

        bool ILine2D.IsPointOnLine(Point testPoint)
        {
            bool isOnLine = MathUtils.CompareDouble(_a * testPoint.X + _b * testPoint.Y, _c);
            if (!_isLineSegment)
            {
                return isOnLine;
            }
            return isOnLine && MathUtils.IsInInterval(testPoint.X, Math.Min(_startPoint.X, _endPoint.X), Math.Max(_startPoint.X, _endPoint.X), _preccision) &&
                MathUtils.IsInInterval(testPoint.Y, Math.Min(_startPoint.Y, _endPoint.Y), Math.Max(_startPoint.Y, _endPoint.Y), _preccision);
        }

        Point? ILine2D.Intersection(ILine2D other)
        {
            return Line2D.Intersection(this, other);
        }

        double ILine2D.Lenght()
        {
            if (_isLineSegment)
            {
                return _lineVector.Length;
            }
            return 0.0;
        }

        /// <summary>
        /// Create perpendicular line in start point
        /// </summary>
        /// <returns></returns>
        ILine2D ILine2D.GetPerpendicularLine(Point pointOnNewLine)
        {
            return CreateLineInAngleInternal(pointOnNewLine, GeometryOperations.GetPerpendicularVector(_lineVector));
        }

        ILine2D ILine2D.GetLineInAngle(Point pointOnNewLine, double angle)
        {
            return CreateLineInAngleInternal(pointOnNewLine, GeometryOperations.RotateVector(_lineVector, angle));
        }

        ILine2D ILine2D.GetParallelLine(Point pointOnNewLine)
        {
            return new Line2D(pointOnNewLine, pointOnNewLine+_lineVector);
        }

        double ILine2D.GetAngle()
        {
            return GeometryOperations.AngleFromHorLine(_lineVector);
        }
        #endregion

        private ILine2D CreateLineInAngleInternal(Point pointOnNewLine, Vector normVector)
        {
            return new Line2D(pointOnNewLine, pointOnNewLine + normVector);
        }
    }
}