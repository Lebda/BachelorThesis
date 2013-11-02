using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows;
using System.Windows.Media;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataAxis : CssDataBase
    {
        public override PathGeometry Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The <see cref="AxisYPen" /> property's name.
        /// </summary>
        public const string AxisPenPropertyName = "AxisPen";

        private Pen _axisPenProperty = new Pen(Brushes.IndianRed, 2);

        /// <summary>
        /// Sets and gets the AxisXPen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Pen AxisPen
        {
            get
            {
                return _axisPenProperty;
            }

            set
            {
                if (_axisPenProperty == value)
                {
                    return;
                }
                _axisPenProperty = value;
                RaisePropertyChanged(AxisPenPropertyName);
            }
        }
    }
}
