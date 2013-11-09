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
        void IStressStrainShapeLoop.Prepare(IStrainStressShape shape)
        {
            _shape = shape;
            Exceptions.CheckNull(_shape.Items, _shape.BaseLine, _shape.NeuAxis, _shape.WholeShape, _shape.ValueShape, _shape.LinesInFibers);
            ILine2D line1 = _shape.NeuAxis.GetParallelLine(_shape.Items.First().Position);
            ILine2D newBaseLine = line1.GetPerpendicularLine(_shape.Items.Last().Position);
            Point test = (Point)Exceptions.CheckNull(line1.Intersection(newBaseLine));
            _shape.BaseLine.StartPoint = newBaseLine.StartPoint;
            _shape.BaseLine.EndPoint = test;
            _shape.WholeShape.Add(test);
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

    public class StrainStressShape : IStrainStressShape
    {
        #region IStrainStressShape Members
        List<ILine2D> _linesInFibers = new List<ILine2D>();
        public List<ILine2D> LinesInFibers
        {
            get { return _linesInFibers; }
            set { _linesInFibers = value; }
        }
        IStressStrainShapeLoop _loop = new StressStrainShapeLoop();
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

        void IStrainStressShape.SetPointValues4MaxWidth(List<StrainStressItem> data, double maxValue)
        {
            _items = DeepCopy.Make<List<StrainStressItem>>(data);
            SetPointValuesInternal(1.0, maxValue);
        }

        void IStrainStressShape.SetPointValues(List<StrainStressItem> data, double valueScale)
        {
            _items = DeepCopy.Make<List<StrainStressItem>>(data);
            SetPointValuesInternal(valueScale);
        }

        void IStrainStressShape.ScaleValues(double scale)
        {
            SetPointValuesInternal(scale);
        }

        public void Transform(Matrix conventer)
        {
            GeometryOperations.TransformOne(conventer, _wholeShape);
            GeometryOperations.TransformOne(conventer, _valueShape);
        }

        void IStrainStressShape.TranslateInDirNeuAxis(double offset)
        {
            Vector move = GeometryOperations.Create(offset, _neuAxis.GetAngle());
            for (int counter = 0; counter < _wholeShape.Count; ++counter)
            {
                _wholeShape[counter] = Point.Add(_wholeShape[counter], move);
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

        private void SetPointValuesInternal(double valueScale, double maxValue = Double.MinValue)
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
                if (!MathUtils.IsZero(maxValueInPos))
                {
                    valueScale = maxValue / maxValueInPos; 
                }
            }
            Exceptions.CheckNull(_loop);
            _loop.Prepare(this);
            _loop.DoLoop(valueScale);
        }
    }
}