using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using System.Windows;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataOneReinf : ObservableObject
    {
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
    }
    public class CssDataReinforcement : CssDataBase
    {
        public override PathGeometry Create()
        {
            PathGeometry myPathGeometry = new PathGeometry();
            PathFigure pathFigure1 = new PathFigure();

            pathFigure1.StartPoint = new Point(0.0, 0.0);
            foreach(CssDataOneReinf iter in _barDataProperty)
            {
                pathFigure1.Segments.Add(
                    new ArcSegment(
                        GeometryOperations.Copy(iter.BarPoint),
                        new Size(iter.Diam, iter.Diam),
                        0,
                        true, /* IsLargeArc */
                        SweepDirection.Clockwise,
                        true /* IsStroked */ ));
            }
            myPathGeometry.Figures.Add(pathFigure1);
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }
        //
        /// <summary>
        /// The <see cref="BarData" /> property's name.
        /// </summary>
        public const string BarDataPropertyName = "BarData";

        private List<CssDataOneReinf> _barDataProperty = new List<CssDataOneReinf>();

        /// <summary>
        /// Sets and gets the BarData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<CssDataOneReinf> BarData
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
