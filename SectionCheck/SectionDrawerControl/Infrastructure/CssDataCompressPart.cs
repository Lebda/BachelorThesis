using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataCompressPart : CssDataBase
    {
        public override PathGeometry Create()
        {
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssCompressPart));
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }

        /// <summary>
        /// The <see cref="CssCompressPart" /> property's name.
        /// </summary>
        public const string CssCompressPartPropertyName = "CssCompressPart";
        public static readonly int CssCompressPartPos = 0;
        PointCollection _cssCompressPart = new PointCollection();
        /// <summary>
        /// Sets and gets the CssCompressPart property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssCompressPart
        {
            get
            {
                return _cssCompressPart;
            }

            set
            {
                if (_cssCompressPart == value)
                {
                    return;
                }
                _cssCompressPart = value;
                RaisePropertyChanged(CssCompressPartPropertyName);
            }
        }

    }
}
