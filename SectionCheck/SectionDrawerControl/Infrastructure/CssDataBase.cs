using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows.Media;
using CommonLibrary.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public abstract class CssDataBase : ObservableObject, IPathGeometryCreator, IVisualObejctDrawingData
    {
        #region IPathGeometryCreator Members

        public abstract PathGeometry Create();

        #endregion

        #region IVisualObejctDrawingData Members

        Pen IVisualObejctDrawingData.GetPen()
        {
            return _visualPenProperty;
        }

        Brush IVisualObejctDrawingData.GetBrush()
        {
            return _visualBrushProperty;
        }

        #endregion
        /// <summary>
        /// The <see cref="VisualBrush" /> property's name.
        /// </summary>
        public const string BrushPropertyName = "VisualBrush";

        private Brush _visualBrushProperty = Brushes.AliceBlue;

        /// <summary>
        /// Sets and gets the Brush property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Brush VisualBrush
        {
            get
            {
                return _visualBrushProperty;
            }
            set
            {
                if (_visualBrushProperty == value)
                {
                    return;
                }
                _visualBrushProperty = value;
                RaisePropertyChanged(BrushPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VisualPen" /> property's name.
        /// </summary>
        public const string PenPropertyName = "VisualPen";

        private Pen _visualPenProperty = new Pen(Brushes.Brown, 3);

        /// <summary>
        /// Sets and gets the Pen property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Pen VisualPen
        {
            get
            {
                return _visualPenProperty;
            }

            set
            {
                if (_visualPenProperty == value)
                {
                    return;
                }
                _visualPenProperty = value;
                RaisePropertyChanged(PenPropertyName);
            }
        }
    }
}
