using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Interfaces;
using System.Windows;
using CommonLibrary.Geometry;
using CommonLibrary.DrawingGraph;
using CommonLibrary.Utility;

namespace CommonLibrary.Factories
{
    public class Line2DFactory
    {
        static Line2DFactory _visualFactory = null;
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
        static StrainStressShapeFactory _factory = null;
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

    public enum eFiberType
    {
        eConcrete,
        eReinforcement
    }
    public class CssFiberFactory
    {
        static CssFiberFactory _factory = null;
        public static CssFiberFactory Instance()
        {
            if (_factory == null)
            {
                _factory = new CssFiberFactory();
            }
            return _factory;
        }
        protected CssFiberFactory()
        {
        }

        public ICssDataFiber Create(int index, Point pos, double neuAxisDistance, eFiberType type)
        {
            ICssDataFiber fiber = null;
            switch (type)
            {
                case eFiberType.eConcrete:
                    fiber = new CssDataFiberCon(index, pos, neuAxisDistance);
                    break;
                case eFiberType.eReinforcement:
                    fiber = new CssDataFiberReinf(index, pos, neuAxisDistance);
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return fiber;
        }
        public ICssDataFiber Create(int index, Point pos, double neuAxisDistance, Dictionary<string, IDataInFiber> data, eFiberType type)
        {
            ICssDataFiber fiber = null;
            switch (type)
            {
                case eFiberType.eConcrete:
                    fiber = new CssDataFiberCon(index, pos, neuAxisDistance, data);
                    break;
                case eFiberType.eReinforcement:
                    fiber = new CssDataFiberReinf(index, pos, neuAxisDistance, data);
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return fiber;
        }
    }
}
