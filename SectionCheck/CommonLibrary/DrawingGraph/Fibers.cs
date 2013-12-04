using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Geometry;
using XEP_CommonLibrary.Utility;
using System.Windows;

namespace XEP_CommonLibrary.DrawingGraph
{
    [Serializable]
    public class SSInFiber : IDataInFiber
    {
        public SSInFiber(double stress, double strain)
        {
            _stress = stress;
            _strain = strain;
        }
        public static readonly string s_name = "StressStrain";
        double _stress = 0.0;
        public double Stress
        {
            get { return _stress; }
            set { _stress = value; }
        }
        double _strain = 0.0;
        public double Strain
        {
            get { return _strain; }
            set { _strain = value; }
        }
    }

    [Serializable]
    public class BarData : IDataInFiber
    {
        public BarData(double barArea, double barDiam)
        {
            _barArea = barArea;
            _barDiam = barDiam;
        }
        public static readonly string s_name = "BarData";
        double _barArea = 0.0;
        public double BarArea
        {
            get { return _barArea; }
            set { _barArea = value; }
        }
        double _barDiam = 0.0;
        public double BarDiam
        {
            get { return _barDiam; }
            set { _barDiam = value; }
        }
    }

    [Serializable]
    public class CssDataFiberImpl : ObservableObject, ICssDataFiber
    {
        public CssDataFiberImpl(int index, Point pos, double neuAxisDistance)
        {
            _indexProperty = index;
            _pointProperty = GeometryOperations.Copy(pos);
            _distanceFromNeuAxisProperty = neuAxisDistance;
        }
        public CssDataFiberImpl()
            : this(0, new Point(), 0.0)
        {
        }

        #region METHODS
        public void SetFiberData(string dataName, IDataInFiber fiberData)
        {
            bool isIn = _dataProperty.ContainsKey(Exceptions.CheckNull(dataName));
            if (!isIn)
            {
                _dataProperty.Add(Exceptions.CheckNull(dataName), Exceptions.CheckNull(fiberData));
            }
            else
            {
                _dataProperty[dataName] = fiberData;
            }
        }
        public void AddFiberData(string dataName, IDataInFiber fiberData)
        {
            _dataProperty.Add(Exceptions.CheckNull(dataName), Exceptions.CheckNull(fiberData));
        }

        public T GetFiberData<T>(string dataName) where T : class
        {
            IDataInFiber retVal = null;
            _dataProperty.TryGetValue(Exceptions.CheckNull(dataName), out retVal);
            return Exceptions.CheckNull<T>(retVal as T);
        }
        #endregion

        #region OBSERVABLE MEMBERS
        public const string FiberDataPropertyName = "FiberData";
        protected Dictionary<string, IDataInFiber> _dataProperty = Exceptions.CheckNull(new Dictionary<string, IDataInFiber>());
        public Dictionary<string, IDataInFiber> Data
        {
            get { return _dataProperty; }
            set
            {
                if (_dataProperty == value) { return; }
                _dataProperty = value;
                RaisePropertyChanged(FiberDataPropertyName);
            }
        }

        public const string FiberPointPropertyName = "FiberPoint";
        protected Point _pointProperty = Exceptions.CheckNull(new Point());
        public Point Point
        {
            get { return _pointProperty; }
            set
            {
                if (_pointProperty == value) { return; }
                _pointProperty = value;
                RaisePropertyChanged(FiberPointPropertyName);
            }
        }

        public const string IndexPropertyName = "FiberIndex";
        protected int _indexProperty = 0;
        public int Index
        {
            get { return _indexProperty; }
            set
            {
                if (_indexProperty == value) { return; }
                _indexProperty = value;
                RaisePropertyChanged(IndexPropertyName);
            }
        }

        public const string DistanceFromNeuAxisPropertyName = "DistanceFromNeuAxis";
        protected double _distanceFromNeuAxisProperty = 0.0;
        public double DistanceFromNeuAxis
        {
            get { return _distanceFromNeuAxisProperty; }
            set
            {
                if (_distanceFromNeuAxisProperty == value) { return; }
                _distanceFromNeuAxisProperty = value;
                RaisePropertyChanged(DistanceFromNeuAxisPropertyName);
            }
        }
        #endregion
    }

    [Serializable]
    public class CssDataFiberCon : CssDataFiberImpl
    {
        #region CTORS
        public CssDataFiberCon(int index, Point pos, double neuAxisDistance)
            : base(index, pos, neuAxisDistance)
        {
            _dataProperty.Add(SSInFiber.s_name, Exceptions.CheckNull(new SSInFiber(0.0, 0.0)));
        }
        public CssDataFiberCon(int index, Point pos, double neuAxisDistance, Dictionary<string, IDataInFiber> data)
            : this(index, pos, neuAxisDistance)
        {
            _dataProperty = data;
        }
        #endregion
    }

    [Serializable]
    public class CssDataFiberReinf : CssDataFiberImpl
    {
        #region CTORS
        public CssDataFiberReinf(int index, Point pos, double neuAxisDistance)
            : base(index, pos, neuAxisDistance)
        {
            _dataProperty.Add(SSInFiber.s_name, Exceptions.CheckNull(new SSInFiber(0.0, 0.0)));
            _dataProperty.Add(BarData.s_name, Exceptions.CheckNull(new BarData(0.0, 0.0)));
        }
        public CssDataFiberReinf(int index, Point pos, double neuAxisDistance, Dictionary<string, IDataInFiber> data)
            : this(index, pos, neuAxisDistance)
        {
            _dataProperty = data;
        }
        #endregion
    }
}
