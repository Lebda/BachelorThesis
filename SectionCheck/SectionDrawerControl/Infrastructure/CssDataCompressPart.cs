using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using XEP_SectionDrawer.Utility;
using System.Windows;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;

namespace XEP_SectionDrawer.Infrastructure
{
    public class CssDataCompressPart : CssDataBase
    {
        public CssDataCompressPart()
            : base(Application.Current.TryFindResource(CustomResources.CompressPartBrush1_SCkey) as Brush,
                    Application.Current.TryFindResource(CustomResources.CompressPartPen1_SCkey) as Pen)
        {
        }
        public CssDataCompressPart(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }

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
