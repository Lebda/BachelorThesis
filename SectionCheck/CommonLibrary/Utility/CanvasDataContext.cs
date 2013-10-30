using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows.Media;

namespace CommonLibrary.Utility
{
    public class CanvasDataContext : ObservableObject
    {
        public CanvasDataContext()
        {
        }

        /// <summary>
        /// The <see cref="CanvasHeightProperty" /> property's name.
        /// </summary>
        public const string CanvasHeightPropertyPropertyName = "CanvasHeightProperty";

        private double _canvasHeightProperty = 300.0;

        /// <summary>
        /// Sets and gets the CanvasHeightProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double CanvasHeightProperty
        {
            get
            {
                return _canvasHeightProperty;
            }

            set
            {
                if (_canvasHeightProperty == value)
                {
                    return;
                }

                _canvasHeightProperty = value;
                RaisePropertyChanged(CanvasHeightPropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CanvasWidthProperty" /> property's name.
        /// </summary>
        public const string CanvasWidthPropertyPropertyName = "CanvasWidthProperty";

        private double _canvasWidthProperty = 300.0;

        /// <summary>
        /// Sets and gets the CanvasWidthProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public double CanvasWidthProperty
        {
            get
            {
                return _canvasWidthProperty;
            }

            set
            {
                if (_canvasWidthProperty == value)
                {
                    return;
                }
                _canvasWidthProperty = value;
                RaisePropertyChanged(CanvasWidthPropertyPropertyName);
            }
        }
    }
}
