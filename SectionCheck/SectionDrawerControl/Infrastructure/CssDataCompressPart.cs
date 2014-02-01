using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionDrawer.Infrastructure
{
    public class CssDataCompressPart : ObservableObject, XEP_ICssDataCompressPart
    {
        public CssDataCompressPart()
        {
        }

        #region IVisualObejctDrawingData Members
        private Pen _visualPen = Application.Current.TryFindResource(CustomResources.CompressPartPen1_SCkey) as Pen;
        public static readonly string VisualPenPropertyName = "VisualPen";
        public Pen VisualPen
        {
            get { return _visualPen; }
            set { SetMember<Pen>(ref value, ref _visualPen, _visualPen == value, VisualPenPropertyName); }
        }
        private Brush _visualBrush = Application.Current.TryFindResource(CustomResources.CompressPartBrush1_SCkey) as Brush;
        public static readonly string VisualBrushPropertyName = "VisualBrush";
        public Brush VisualBrush
        {
            get { return _visualBrush; }
            set { SetMember<Brush>(ref value, ref _visualBrush, _visualBrush == value, VisualPenPropertyName); }
        }
        #endregion

        #region IVisualObejctDrawingData Members
        public Pen GetPen()
        {
            return _visualPen;
        }
        public Brush GetBrush()
        {
            return _visualBrush;
        }
        #endregion

        #region IPathGeometryCreator Members
        public PathGeometry Create()
        {
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssCompressPart));
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }
        #endregion

        #region XEP_ICssDataCompressPart Members
        PointCollection _cssCompressPart = new PointCollection();
        public static readonly string CssCompressPartPropertyName = "CssCompressPart";
        public PointCollection CssCompressPart
        {
            get { return _cssCompressPart; }
            set { SetMember<PointCollection>(ref value, ref _cssCompressPart, _cssCompressPart == value, CssCompressPartPropertyName); }
        }
        #endregion

    }
}
