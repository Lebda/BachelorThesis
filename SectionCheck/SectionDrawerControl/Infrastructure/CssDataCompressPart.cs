using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataCompressPart : CssDataBase
    {
        public CssDataCompressPart() : base (1)
        {
        }

        /// <summary>
        /// The <see cref="CssCompressPart" /> property's name.
        /// </summary>
        public const string CssCompressPartPropertyName = "CssCompressPart";
        public static readonly int CssCompressPartPos = 0;
        /// <summary>
        /// Sets and gets the CssCompressPart property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssCompressPart
        {
            get
            {
                return _shapeObjetcs[CssCompressPartPos];
            }

            set
            {
                if (_shapeObjetcs[CssCompressPartPos] == value)
                {
                    return;
                }
                _shapeObjetcs[CssCompressPartPos] = value;
                RaisePropertyChanged(CssCompressPartPropertyName);
            }
        }

    }
}
