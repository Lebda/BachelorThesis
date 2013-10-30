using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CommonLibrary.Infrastructure;
using CommonLibrary.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataShape : CssDataBase
    {
        public CssDataShape() : base(2)
        {
        }

        /// <summary>
        /// The <see cref="CssShapeOuter" /> property's name.
        /// </summary>
        public const string CssShapeOuterPropertyName = "CssShapeOuter";
        public static readonly int CssShapeOuterPos = 0;
        /// <summary>
        /// Sets and gets the CssShapeOuter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssShapeOuter
        {
            get
            {
                return _shapeObjetcs[CssShapeOuterPos];
            }

            set
            {
                if (_shapeObjetcs[CssShapeOuterPos] == value)
                {
                    return;
                }
                _shapeObjetcs[CssShapeOuterPos] = value;
                RaisePropertyChanged(CssShapeOuterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CssShapeInner" /> property's name.
        /// </summary>
        public const string CssShapeInnerPropertyName = "CssShapeInner";
        public static readonly int CssShapeInnerPos = 1;
        /// <summary>
        /// Sets and gets the CssShapeInner property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssShapeInner
        {
            get
            {
                return _shapeObjetcs[CssShapeInnerPos];
            }

            set
            {
                if (_shapeObjetcs[CssShapeInnerPos] == value)
                {
                    return;
                }
                _shapeObjetcs[CssShapeInnerPos] = value;
                RaisePropertyChanged(CssShapeInnerPropertyName);
            }
        }
    }
}
