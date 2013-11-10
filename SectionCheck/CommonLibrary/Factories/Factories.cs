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

        public IStrainStressShape Create(eCssComponentType type)
        {
            return Exceptions.CheckNull(new StrainStressShape(StressStrainShapeLoopFactory.Instance().Create(type)));
        }
    }

    public enum eCssComponentType
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

        public ICssDataFiber Create(int index, Point pos, double neuAxisDistance, eCssComponentType type)
        {
            ICssDataFiber fiber = null;
            switch (type)
            {
                case eCssComponentType.eConcrete:
                    fiber = new CssDataFiberCon(index, pos, neuAxisDistance);
                    break;
                case eCssComponentType.eReinforcement:
                    fiber = new CssDataFiberReinf(index, pos, neuAxisDistance);
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return fiber;
        }
        public ICssDataFiber Create(int index, Point pos, double neuAxisDistance, Dictionary<string, IDataInFiber> data, eCssComponentType type)
        {
            ICssDataFiber fiber = null;
            switch (type)
            {
                case eCssComponentType.eConcrete:
                    fiber = new CssDataFiberCon(index, pos, neuAxisDistance, data);
                    break;
                case eCssComponentType.eReinforcement:
                    fiber = new CssDataFiberReinf(index, pos, neuAxisDistance, data);
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return fiber;
        }
    }

    public class GeometryMakerFactory
    {
        static GeometryMakerFactory _factory = null;
        public static GeometryMakerFactory Instance()
        {
            if (_factory == null)
            {
                _factory = new GeometryMakerFactory();
            }
            return _factory;
        }
        protected GeometryMakerFactory()
        {
        }

        public IGeometryMaker Create(eCssComponentType type)
        {
            IGeometryMaker newObject = null;
            IStrainStressShape shapeMaker = StrainStressShapeFactory.Instance().Create(type);
            switch (type)
            {
                case eCssComponentType.eConcrete:
                    newObject = new GeometryMaker(shapeMaker);
                    break;
                case eCssComponentType.eReinforcement:
                    newObject = new GeometryMakerReinf(shapeMaker);
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return newObject;
        }
    }

    public class StressStrainShapeLoopFactory
    {
        static StressStrainShapeLoopFactory _factory = null;
        public static StressStrainShapeLoopFactory Instance()
        {
            if (_factory == null) { _factory = new StressStrainShapeLoopFactory();}
            return _factory;
        }
        protected StressStrainShapeLoopFactory() {}

        public IStressStrainShapeLoop Create(eCssComponentType type)
        {
            IStressStrainShapeLoop newObject = null;
            switch (type)
            {
                case eCssComponentType.eConcrete:
                    newObject = new StressStrainShapeLoop();
                    break;
                case eCssComponentType.eReinforcement:
                    newObject = new StressStrainShapeLoopReinf();
                    break;
                default:
                    Exceptions.CheckNull(null);
                    break;
            }
            return newObject;
        }
    }
}
