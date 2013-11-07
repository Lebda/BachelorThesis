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
using CommonLibrary.Interfaces;

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
            Int32 index = 0;
            while (index < fibers.Count - 1)
            {
                if (MathUtils.CompareDouble(fibers[index].DistanceFromNeuAxis, fibers[index + 1].DistanceFromNeuAxis, 1e-6))
                {
                    fibers.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
            PointCollection strainShape = Exceptions.CheckNull<PointCollection>(new PointCollection());
            ILine2D line1 = _neuAxisProperty.GetParallelLine(fibers[0].FiberPoint);
            ILine2D basicLine = line1.GetPerpendicularLine(fibers[fibers.Count-1].FiberPoint);
            Point test = (Point)Exceptions.CheckNull(line1.Intersection(basicLine));
            basicLine.EndPoint = test;
            strainShape.Add(test);
            for (int counter = 0; counter < fibers.Count; ++counter)
            {
                CssDataFiber fiber = fibers[counter];
                ILine2D lineInFiber = _neuAxisProperty.GetParallelLine(fiber.FiberPoint);
                Point intersectionWithBaseLine = (Point)Exceptions.CheckNull(lineInFiber.Intersection(basicLine));
                Vector moveVec = GeometryOperations.Create(fiber.GetFiberData<StressStrainFiber>(StressStrainFiber.s_dataInFiberName).FiberStrain, _neuAxisProperty.GetAngle());
                strainShape.Add(Point.Add(intersectionWithBaseLine, moveVec));
            }
            strainShape.Add(fibers[fibers.Count - 1].FiberPoint);
            strainShape.Add(strainShape[0]);
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
