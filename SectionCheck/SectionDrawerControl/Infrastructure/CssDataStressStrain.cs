using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using ResourceLibrary;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using SectionDrawerControl.Utility;
using CommonLibrary.Utility;
using System.Windows.Shapes;
using CommonLibrary.Geometry;

namespace SectionDrawerControl.Infrastructure
{
    public interface IDataInFiber
    {

    }
    [Serializable]
    public class StressStrainFiber : ObservableObject, IDataInFiber
    {
        public static readonly string s_dataInFiberName = "StressStrainFiber";
        /// <summary>
        /// The <see cref="FiberStress" /> property's name.
        /// </summary>
        public const string _fiberStressPropertyName = "FiberStress";

        private double _fiberStressProperty = 0.0;

        /// <summary>
        /// Sets and gets the _stress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double FiberStress
        {
            get
            {
                return _fiberStressProperty;
            }

            set
            {
                if (_fiberStressProperty == value)
                {
                    return;
                }
                _fiberStressProperty = value;
                RaisePropertyChanged(_fiberStressPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FiberStrain" /> property's name.
        /// </summary>
        public const string FiberStrainPropertyName = "FiberStrain";

        private double _fiberStrainProperty = 0.0;

        /// <summary>
        /// Sets and gets the FiberStrain property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double FiberStrain
        {
            get
            {
                return _fiberStrainProperty;
            }

            set
            {
                if (_fiberStrainProperty == value)
                {
                    return;
                }
                _fiberStrainProperty = value;
                RaisePropertyChanged(FiberStrainPropertyName);
            }
        }
    }

    [Serializable]
    public class CssDataFiber : ObservableObject
    {
        public CssDataFiber()
        {
            _fiberDataProperty.Add(StressStrainFiber.s_dataInFiberName, Exceptions.CheckNull(new StressStrainFiber()));
        }
        public CssDataFiber(int index, Point pos) : this()
        {
            _fiberIndexProperty = index;
            _fibderPointProperty = GeometryOperations.Copy(pos);
        }
        public CssDataFiber(int index, Point pos, double stress, double strain, double neuAxisDistance)
            : this(index, pos)
        {
            _fiberIndexProperty = index;
            _distanceFromNeuAxisProperty = neuAxisDistance;
            _fibderPointProperty = GeometryOperations.Copy(pos);
            StressStrainFiber stressStrain = GetFiberData<StressStrainFiber>(StressStrainFiber.s_dataInFiberName);
            stressStrain.FiberStrain = strain;
            stressStrain.FiberStress = stress;
        }
        //Methods
        public static int ComparePosition(CssDataFiber first, CssDataFiber second)
        {
            if (MathUtils.CompareDouble(first._distanceFromNeuAxisProperty, second._distanceFromNeuAxisProperty, 1e-6))
            {
                return 0;
            }
            if (first._distanceFromNeuAxisProperty < second._distanceFromNeuAxisProperty)
            {
                return -1;
            }
            return 1;
        }
        public void AddFiberData(string dataName, IDataInFiber fiberData)
        {
            _fiberDataProperty.Add(Exceptions.CheckNull(dataName), Exceptions.CheckNull(fiberData));
        }
        public T GetFiberData<T>(string dataName)  where T : class
        {
            IDataInFiber retVal = null;
            _fiberDataProperty.TryGetValue(Exceptions.CheckNull(dataName), out retVal);
            return Exceptions.CheckNull<T>(retVal as T);
        }

        /// <summary>
        /// The <see cref="FiberData" /> property's name.
        /// </summary>
        public const string FiberDataPropertyName = "FiberData";

        private Dictionary<string, IDataInFiber> _fiberDataProperty = Exceptions.CheckNull(new Dictionary<string, IDataInFiber>());

        /// <summary>
        /// Sets and gets the FiberData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Dictionary<string, IDataInFiber> FiberData
        {
            get
            {
                return _fiberDataProperty;
            }

            set
            {
                if (_fiberDataProperty == value)
                {
                    return;
                }
                _fiberDataProperty = value;
                RaisePropertyChanged(FiberDataPropertyName);
            }
        }



        /// <summary>
        /// The <see cref="FiberPoint" /> property's name.
        /// </summary>
        public const string FiberPointPropertyName = "FiberPoint";

        private Point _fibderPointProperty = Exceptions.CheckNull(new Point());

        /// <summary>
        /// Sets and gets the FiberPoint property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Point FiberPoint
        {
            get
            {
                return _fibderPointProperty;
            }

            set
            {
                if (_fibderPointProperty == value)
                {
                    return;
                }
                _fibderPointProperty = value;
                RaisePropertyChanged(FiberPointPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="FiberIndex" /> zero based
        /// </summary>
        public const string FiberIndexPropertyName = "FiberIndex";

        private int _fiberIndexProperty = 0;

        /// <summary>
        /// Sets and gets the FiberIndex property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int FiberIndex
        {
            get
            {
                return _fiberIndexProperty;
            }

            set
            {
                if (_fiberIndexProperty == value)
                {
                    return;
                }
                _fiberIndexProperty = value;
                RaisePropertyChanged(FiberIndexPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="DistanceFromNeuAxis" /> property's name.
        /// </summary>
        public const string DistanceFromNeuAxisPropertyName = "DistanceFromNeuAxis";

        private double _distanceFromNeuAxisProperty = 0.0;

        /// <summary>
        /// Sets and gets the DistanceFromNeuAxis property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double DistanceFromNeuAxis
        {
            get
            {
                return _distanceFromNeuAxisProperty;
            }

            set
            {
                if (_distanceFromNeuAxisProperty == value)
                {
                    return;
                }
                _distanceFromNeuAxisProperty = value;
                RaisePropertyChanged(DistanceFromNeuAxisPropertyName);
            }
        }
    }

    [Serializable]
    public class CssDataFibers : CssDataBase
    {
         public CssDataFibers()
            : base(Application.Current.TryFindResource(CustomResources.ConcreteStrainBrush1_SCkey) as Brush, 
                    Application.Current.TryFindResource(CustomResources.ConcreteStrainPen1_SCkey) as Pen)
        {

        }
         public CssDataFibers(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }
        public override PathGeometry Create()
        {
            PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
            Comparison<CssDataFiber> predicate = CssDataFiber.ComparePosition;
            List<CssDataFiber> fibers = DeepCopy.Make<List<CssDataFiber>>(_fibersProperty);
            if (Common.IsEmpty<CssDataFiber>(fibers))
            {
                return null;
            }
            fibers.Sort(predicate);
            PointCollection strainShape = Exceptions.CheckNull<PointCollection>(new PointCollection());
            Point actualPoint = GeometryOperations.Copy(fibers[0].FiberPoint);
            strainShape.Add(actualPoint);
            ILine2D line1 = _neuAxisProperty.GetParallelLine(fibers[0].FiberPoint);
            ILine2D line2 = line1.GetPerpendicularLine(fibers[fibers.Count-1].FiberPoint);
            Point? test = line1.Intersection(line2);
            if (test == null)
            {
            }
            // Second point
            //Vector move1 = GeometryOperations.Create(fibers[0].GetFiberData<StressStrainFiber>(StressStrainFiber.s_dataInFiberName).FiberStrain, _neuAxisAngleProperty);
            //actualPoint = Point.Add(actualPoint, move1);
            strainShape.Add(Exceptions.CheckNull<Point>(new Point(fibers[0].FiberPoint.X, fibers[0].FiberPoint.Y)));
            double previousNeuAxisDistance = fibers[0].DistanceFromNeuAxis;
            for (int counter = 0; counter < fibers.Count; ++counter)
            {
                if (counter != 0 && MathUtils.CompareDouble(fibers[counter-1].DistanceFromNeuAxis, fibers[counter].DistanceFromNeuAxis))
                {
                    continue;
                }
                strainShape.Add(Exceptions.CheckNull<Point>(new Point(fibers[counter].GetFiberData<StressStrainFiber>(StressStrainFiber.s_dataInFiberName).FiberStrain, fibers[counter].DistanceFromNeuAxis)));
            }
            if (strainShape.Count > 0)
            {
                strainShape.Add(Exceptions.CheckNull<Point>(new Point(0.0, strainShape[strainShape.Count - 1].Y)));
            }
            strainShape.Add(Exceptions.CheckNull<Point>(new Point(strainShape[0].X, strainShape[0].Y)));
            myPathGeometry.Figures.Add(GeometryOperations.Create(strainShape));
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }

        /// <summary>
        /// The <see cref="Fibers" /> property's name.
        /// </summary>
        public const string FibersPropertyName = "Fibers";

        private List<CssDataFiber> _fibersProperty = Exceptions.CheckNull(new List<CssDataFiber>());

        /// <summary>
        /// Sets and gets the Fibers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<CssDataFiber> Fibers
        {
            get
            {
                return _fibersProperty;
            }

            set
            {
                if (_fibersProperty == value)
                {
                    return;
                }
                _fibersProperty = value;
                RaisePropertyChanged(FibersPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="NeuAxis" /> property's name.
        /// </summary>
        public const string NeuAxisPropertyName = "NeuAxis";

        private ILine2D _neuAxisProperty = null;

        /// <summary>
        /// value in [rad]
        /// Sets and gets the NeuAxisAngle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ILine2D NeuAxis
        {
            get
            {
                return _neuAxisProperty;
            }

            set
            {
                if (_neuAxisProperty == value)
                {
                    return;
                }
                _neuAxisProperty = value;
                RaisePropertyChanged(NeuAxisPropertyName);
            }
        }
    }
}
