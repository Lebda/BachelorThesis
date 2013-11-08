using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Interfaces;
using System.Windows;
using CommonLibrary.Geometry;
using CommonLibrary.DrawingGraph;

namespace CommonLibrary.Factories
{
    public class Line2DFactory
    {
        static Line2DFactory _visualFactory = new Line2DFactory();
        public static Line2DFactory Instance()
        {
            if (_visualFactory == null)
            {
                _visualFactory = new Line2DFactory();
            }
            return _visualFactory;
        }
        protected Line2DFactory()
        {
        }
        public ILine2D Create(Point start, Vector direction)
        {
            return new Line2D(start, direction);
        }
        public ILine2D Create(Point start, double direction)
        {
            return new Line2D(start, direction);
        }
        public ILine2D Create(Point start, Point end)
        {
            return new Line2D(start, end);
        }
        /// <summary>
        /// Create line start point [0,0] end point [1.0, 0.0]
        /// </summary>
        /// <returns></returns>
        public ILine2D Create()
        {
            return new Line2D(new Point(0.0, 0.0), new Point(1.0, 0.0));
        }
    }

    public class StrainStressShapeFactory
    {
        static StrainStressShapeFactory _factory = new StrainStressShapeFactory();
        public static StrainStressShapeFactory Instance()
        {
            if (_factory == null)
            {
                _factory = new StrainStressShapeFactory();
            }
            return _factory;
        }
        protected StrainStressShapeFactory()
        {
        }

        public IStrainStressShape Create()
        {
            return new StrainStressShape();
        }
    }
}
