using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using System.Windows;
using SectionDrawerControl.Utility;
using CommonLibrary.Utility;
using ResourceLibrary;
using CommonLibrary.Geometry;
using System.Collections.ObjectModel;

namespace SectionDrawerControl.Infrastructure
{
    [Serializable]
    public class CssDataOneReinf : ObservableObject, ICloneable
    {
        public CssDataOneReinf()
        {
        }
        public CssDataOneReinf(double horPos, double verPos, double diam)
        {
            _barPointProperty.X = horPos;
            _barPointProperty.Y = verPos;
            _diameterProperty = diam;
        }
        /// <summary>
        /// The <see cref="Diameter" /> property's name.
        /// </summary>
        public const string DiameterPropertyName = "Diameter";

        private double _diameterProperty = 0.0;

        /// <summary>
        /// Sets and gets the Radius property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double Diam
        {
            get
            {
                return _diameterProperty;
            }

            set
            {
                if (_diameterProperty == value)
                {
                    return;
                }
                _diameterProperty = value;
                RaisePropertyChanged(DiameterPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="BarPoint" /> property's name.
        /// </summary>
        public const string BarPointPropertyName = "BarPoint";

        private Point _barPointProperty = new Point();
        public Point BarPoint
        {
            get
            {
                return _barPointProperty;
            }
            set
            {
                if (_barPointProperty == value)
                {
                    return;
                }
                _barPointProperty = value;
                RaisePropertyChanged(BarPointPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="BarArea" /> property's name.
        /// </summary>
        public const string BarAreaPropertyName = "BarArea";

        private double _barAreaProperty = 0.0;

        /// <summary>
        /// Sets and gets the BarArea property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double BarArea
        {
            get
            {
                return _barAreaProperty;
            }

            set
            {
                if (_barAreaProperty == value)
                {
                    return;
                }
                _barAreaProperty = value;
                RaisePropertyChanged(BarAreaPropertyName);
            }
        }

        #region ICloneable Members
        public object Clone()
        {
            CssDataOneReinf clone = new CssDataOneReinf();
            clone._diameterProperty = _diameterProperty;
            clone._barAreaProperty = _barAreaProperty;
            clone._barPointProperty = GeometryOperations.Copy(_barPointProperty);
            return clone;
        }
        #endregion
    }
    public class CssDataReinforcement : CssDataBase
    {
        public CssDataReinforcement()
            : base( Application.Current.TryFindResource(CustomResources.ReinfBrush1_SCkey) as Brush, 
                    Application.Current.TryFindResource(CustomResources.ReinfPen1_SCkey) as Pen)
        {
        }
        public CssDataReinforcement(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }

        public override PathGeometry Create()
        {
            PathGeometry myPathGeometry = new PathGeometry();
            if (Common.IsEmpty(_barDataProperty))
            {
                return myPathGeometry;
            }
            foreach(CssDataOneReinf iter in _barDataProperty)
            {
                EllipseGeometry circle = new EllipseGeometry(iter.BarPoint, iter.Diam / 2, iter.Diam / 2);
                myPathGeometry.AddGeometry(circle);
            }
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }
        //
        /// <summary>
        /// The <see cref="BarData" /> property's name.
        /// </summary>
        public const string BarDataPropertyName = "BarData";

        private ObservableCollection<CssDataOneReinf> _barDataProperty = new ObservableCollection<CssDataOneReinf>();

        /// <summary>
        /// Sets and gets the BarData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CssDataOneReinf> BarData
        {
            get
            {
                return _barDataProperty;
            }

            set
            {
                if (_barDataProperty == value)
                {
                    return;
                }
                _barDataProperty = value;
                RaisePropertyChanged(BarDataPropertyName);
            }
        }

    }
}
