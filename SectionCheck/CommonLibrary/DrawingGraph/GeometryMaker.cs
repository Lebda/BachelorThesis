using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.InterfaceObjects;
using XEP_CommonLibrary.Utility;
using XEP_CommonLibrary.Geometry;

namespace XEP_CommonLibrary.DrawingGraph
{
    public class GeometryMaker : IGeometryMaker
    {
        public GeometryMaker(IStrainStressShape shapeMaker)
        {
            _shapeMaker = Exceptions.CheckNull(shapeMaker);
        }
        #region IGeometryMaker Members
        protected bool _isStrain = true;
        public bool IsStrain
        {
            get { return _isStrain; }
            set { _isStrain = value; }
        }
        protected bool _isMove = true;
        public bool IsMove
        {
            get { return _isMove; }
            set { _isMove = value; }
        }
        protected double _moveSize = 0.0;
        public double MoveSize
        {
            get { return _moveSize; }
            set { _moveSize = value; }
        }
        protected double _maxWidth = 0.0;
        public double MaxWidth
        {
            get { return _maxWidth; }
            set { _maxWidth = value; }
        }
        protected FillRule _rule4File = FillRule.Nonzero;
        public FillRule Rule4File
        {
            get { return _rule4File; }
            set { _rule4File = value; }
        }
        protected IStrainStressShape _shapeMaker = null;
        public IStrainStressShape ShapeMaker
        {
            get { return _shapeMaker; }
            set { _shapeMaker = value; }
        }
        public List<InterfaceObjects.StrainStressItem> CreateStressStrainItems(List<ICssDataFiber> fibers, bool strain = true)
        {
            List<StrainStressItem> items = new List<StrainStressItem>();
            if (fibers == null || fibers.Count == 0)
            {
                return items;
            }
            foreach (ICssDataFiber fiber in fibers)
            {
                if (strain)
                {
                    items.Add(new StrainStressItem(fiber.Point, fiber.DistanceFromNeuAxis, fiber.GetFiberData<SSInFiber>(SSInFiber.s_name).Strain));
                }
                else
                {
                    items.Add(new StrainStressItem(fiber.Point, fiber.DistanceFromNeuAxis, fiber.GetFiberData<SSInFiber>(SSInFiber.s_name).Stress));
                }
            }
            return items;
        }
        virtual public PathGeometry CreateGeometry(List<ICssDataFiber> fibers, IStrainStressShape dataDependentObject)
        {
            PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
            if (fibers == null || fibers.Count == 0)
            {
                return myPathGeometry;
            }
            Exceptions.CheckNull(Exceptions.CheckNull<IStrainStressShape>(_shapeMaker).NeuAxis);
            List<StrainStressItem> items = CreateStressStrainItems(fibers, _isStrain);
            _shapeMaker.CreateShape4MaxWidth(items, _maxWidth, dataDependentObject);
            if (_isMove)
            {
                _shapeMaker.TranslateInDirNeuAxis(_moveSize);
            }
            myPathGeometry.Figures.Add(GeometryOperations.Create(_shapeMaker.WholeShape));
            myPathGeometry.FillRule = _rule4File;
            return myPathGeometry;
        }
        #endregion
    }

    public class GeometryMakerReinf : GeometryMaker
    {
        public GeometryMakerReinf(IStrainStressShape shapeMaker) : base(shapeMaker)
        {
        }

        public override PathGeometry CreateGeometry(List<ICssDataFiber> fibers, IStrainStressShape dataDependentObject)
        {
            PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
            if (fibers == null || fibers.Count == 0)
            {
                return myPathGeometry;
            }
            Exceptions.CheckNull(Exceptions.CheckNull<IStrainStressShape>(_shapeMaker).NeuAxis);
            List<StrainStressItem> items = CreateStressStrainItems(fibers, _isStrain);
            _shapeMaker.IsStrain = _isStrain;
            _shapeMaker.CreateShape4MaxWidth(items, _maxWidth, dataDependentObject);
            if (_isMove)
            {
                _shapeMaker.TranslateInDirNeuAxis(_moveSize);
            }
            foreach (ILine2D iter in _shapeMaker.LinesInFibers)
            {
                LineGeometry line = new LineGeometry(GeometryOperations.Copy(iter.StartPoint), GeometryOperations.Copy(iter.EndPoint));
                myPathGeometry.AddGeometry(line);
            }
            myPathGeometry.FillRule = _rule4File;
            return myPathGeometry;
        }
    }

}
