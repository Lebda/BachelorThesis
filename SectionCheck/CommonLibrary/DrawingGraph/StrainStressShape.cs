using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Interfaces;
using CommonLibrary.Geometry;
using System.Windows.Media;
using System.Windows;
using CommonLibrary.Utility;
using CommonLibrary.Factories;
using CommonLibrary.InterfaceObjects;

namespace CommonLibrary.DrawingGraph
{
    public class StressStrainShapeLoop : IStressStrainShapeLoop
    {
        protected IStrainStressShape _shape = null;
        public virtual void Prepare(IStrainStressShape shape, IStrainStressShape dataDependentObject)
        {
            _shape = Exceptions.CheckNull(shape);
            Exceptions.CheckNull(_shape.Items, _shape.BaseLine, _shape.NeuAxis, _shape.WholeShape, _shape.ValueShape, _shape.LinesInFibers);
            ILine2D lineIn1stFiberParalel2NeuAxis = _shape.NeuAxis.GetParallelLine(_shape.Items.First().Position);
            ILine2D newBaseLine = lineIn1stFiberParalel2NeuAxis.GetPerpendicularLine(_shape.Items.Last().Position);
            Point intersection = (Point)Exceptions.CheckNull(lineIn1stFiberParalel2NeuAxis.Intersection(newBaseLine));
            _shape.BaseLine.StartPoint = newBaseLine.StartPoint;
            _shape.BaseLine.EndPoint = intersection;
            _shape.WholeShape.Add(intersection);
        }

        public void DoLoop(double valueScale)
        {
            Exceptions.CheckNull(_shape.Items, _shape.BaseLine, _shape.NeuAxis, _shape.WholeShape, _shape.ValueShape, _shape.LinesInFibers);
            for (int counter = 0; counter < _shape.Items.Count; ++counter)
            {
                StrainStressItem item = _shape.Items[counter];
                ILine2D lineInFiber = _shape.NeuAxis.GetParallelLine(item.Position);
                Point intersectionWithBaseLine = (Point)Exceptions.CheckNull(lineInFiber.Intersection(_shape.BaseLine));
                Vector moveVec = GeometryOperations.Create(item.ValueInPos * valueScale, _shape.NeuAxis.GetAngle());
                Point pos = Point.Add(intersectionWithBaseLine, moveVec);
                _shape.LinesInFibers.Add(Line2DFactory.Instance().Create(intersectionWithBaseLine, pos));
                _shape.WholeShape.Add(pos);
                _shape.ValueShape.Add(GeometryOperations.Copy(pos));
            }
            _shape.WholeShape.Add(_shape.Items.Last().Position);
            _shape.WholeShape.Add(_shape.WholeShape.First());
        }
    }

    public class StressStrainShapeLoopReinf : StressStrainShapeLoop
    {
        public override void Prepare(IStrainStressShape shape, IStrainStressShape dataDependentObject)
        {
            _shape = Exceptions.CheckNull(shape);
            Exceptions.CheckNull(_shape.Items, _shape.BaseLine, _shape.NeuAxis, _shape.WholeShape, _shape.ValueShape, _shape.LinesInFibers, dataDependentObject);
            ILine2D lineIn1stFiberParalel2NeuAxis = _shape.NeuAxis.GetParallelLine(_shape.Items.First().Position);
            _shape.BaseLine.StartPoint = dataDependentObject.BaseLine.StartPoint;
            _shape.BaseLine.EndPoint = dataDependentObject.BaseLine.EndPoint;
            Point intersection = (Point)Exceptions.CheckNull(lineIn1stFiberParalel2NeuAxis.Intersection(_shape.BaseLine));
            _shape.WholeShape.Add(intersection);
        }
    }

    public class StrainStressShape : IStrainStressShape
    {
        public StrainStressShape(IStressStrainShapeLoop loop)
        {
            _loop = Exceptions.CheckNull(loop);
        }
        #region IStrainStressShape Members
        List<ILine2D> _linesInFibers = new List<ILine2D>();
        public List<ILine2D> LinesInFibers
        {
            get { return _linesInFibers; }
            set { _linesInFibers = value; }
        }
        IStressStrainShapeLoop _loop = null;
        public IStressStrainShapeLoop Loop
        {
            get { return _loop; }
            set { _loop = value; }
        }
        ILine2D _baseLine = Line2DFactory.Instance().Create();
        public ILine2D BaseLine
        {
            get { return _baseLine; }
        }
        PointCollection _wholeShape = new PointCollection();
        public PointCollection WholeShape
        {
            get { return _wholeShape; }
        }
        PointCollection _valueShape = new PointCollection();
        public PointCollection ValueShape
        {
            get { return _valueShape; }
        }
        ILine2D _neuAxis = Line2DFactory.Instance().Create();
        public ILine2D NeuAxis
        {
            get { return _neuAxis; }
            set { _neuAxis = Line2DFactory.Instance().Create(value.StartPoint, value.EndPoint); }
        }
        List<StrainStressItem> _items = new List<StrainStressItem>();
        public List<StrainStressItem> Items
	    {
            get { return _items; }
	    }
        // Methods
        public double GetMaxValue(List<StrainStressItem> items)
        {
            return Math.Max(Math.Abs(items.First().ValueInPos), Math.Abs(items.Last().ValueInPos));
        }

        void IStrainStressShape.CreateShape4MaxWidth(List<StrainStressItem> data, double maxValue, IStrainStressShape dataDependentObject)
        {
            _items = DeepCopy.Make<List<StrainStressItem>>(data);
            CreateShapeInternal(1.0, maxValue, dataDependentObject);
        }

        void IStrainStressShape.CreateShape(List<StrainStressItem> data, double valueScale, IStrainStressShape dataDependentObject)
        {
            _items = DeepCopy.Make<List<StrainStressItem>>(data);
            CreateShapeInternal(valueScale, Double.MinValue, dataDependentObject);
        }

        void IStrainStressShape.ScaleValues(double scale)
        {
            CreateShapeInternal(scale);
        }

        public void Transform(Matrix conventer)
        {
            GeometryOperations.TransformOne(conventer, _wholeShape);
            GeometryOperations.TransformOne(conventer, _valueShape);
            foreach (ILine2D iter in _linesInFibers)
            {
                iter.StartPoint = conventer.Transform(iter.StartPoint);
                iter.EndPoint = conventer.Transform(iter.EndPoint);
            }
        }

        void IStrainStressShape.TranslateInDirNeuAxis(double offset)
        {
            Vector move = GeometryOperations.Create(offset, _neuAxis.GetAngle());
            for (int counter = 0; counter < _wholeShape.Count; ++counter)
            {
                _wholeShape[counter] = Point.Add(_wholeShape[counter], move);
            }
            foreach (ILine2D iter in _linesInFibers)
            {
                iter.StartPoint = Point.Add(iter.StartPoint, move);
                iter.EndPoint = Point.Add(iter.EndPoint, move);
            }
        }

        #endregion

        public static int ComparePosition(StrainStressItem first, StrainStressItem second)
        {
            if (MathUtils.CompareDouble(first.DisNeuAxis, second.DisNeuAxis, 1e-6))
            {
                return 0;
            }
            if (first.DisNeuAxis < second.DisNeuAxis)
            {
                return -1;
            }
            return 1;
        }

        private void CreateShapeInternal(double valueScale, double maxValue = Double.MinValue, IStrainStressShape dataDependentObject = null)
        {
            if (Common.IsEmpty(_items))
            {
                return;
            }
            _items.Sort(ComparePosition);
            Int32 index = 0;
            while (index < _items.Count - 1)
            {
                if (MathUtils.CompareDouble(_items[index].DisNeuAxis, _items[index + 1].DisNeuAxis, 1e-6))
                {
                    _items.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            _linesInFibers.Clear();
            _valueShape.Clear();
            _wholeShape.Clear();
            if (Common.IsEmpty(_items))
            {
                return;
            }
            if (maxValue != Double.MinValue)
            {
                double maxValueInPos = GetMaxValue(_items);
                if (dataDependentObject != null)
                {
                    maxValueInPos = Math.Max(Math.Abs(maxValueInPos), Math.Abs(dataDependentObject.GetMaxValue(Exceptions.CheckNull(dataDependentObject.Items))));
                }
                if (!MathUtils.IsZero(maxValueInPos))
                {
                    valueScale = maxValue / maxValueInPos;
                }
            }
            Exceptions.CheckNull(_loop);
            _loop.Prepare(this, dataDependentObject);
            _loop.DoLoop(valueScale);
        }
    }
}