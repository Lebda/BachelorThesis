using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionDrawer.Infrastructure
{
    [Serializable]
    public class CssDataOneReinf : ObservableObject, XEP_ICssDataOneReinf
    {
        public CssDataOneReinf()
        {
        }
        public CssDataOneReinf(double horPos, double verPos, double diam)
        {
            _barPoint.X = horPos;
            _barPoint.Y = verPos;
            _diam = diam;
        }

        #region XEP_ICssDataOneReinf Members
        double _diam = 0.0;
        public static readonly string DiamPropertyName = "Diam";
        public double Diam
        {
            get { return _diam; }
            set { SetMember<double>(ref value, ref _diam, _diam == value, DiamPropertyName); }
        }
        double _barArea = 0.0;
        public static readonly string BarAreaPropertyName = "BarArea";
        public double BarArea
        {
            get { return _diam; }
            set { SetMember<double>(ref value, ref _barArea, _barArea == value, BarAreaPropertyName); }
        }
        Point _barPoint = new Point();
        public static readonly string BarPointPropertyName = "BarPoint";
        public Point BarPoint
        {
            get { return _barPoint; }
            set { SetMember<Point>(ref value, ref _barPoint, _barPoint == value, BarPointPropertyName); }
        }
        #endregion

        #region ICloneable Members
        public object Clone()
        {
            XEP_ICssDataOneReinf clone = new CssDataOneReinf();
            clone.Diam = _diam;
            clone.BarArea = _barArea;
            clone.BarPoint = GeometryOperations.Copy(_barPoint);
            return clone;
        }
        #endregion
    }

    public class CssDataReinforcement : ObservableObject, XEP_ICssDataReinforcement
    {
        public CssDataReinforcement()
        {

        }

        #region IVisualObejctDrawingData Members
        private Pen _visualPen = Application.Current.TryFindResource(CustomResources.ReinfPen1_SCkey) as Pen;
        public static readonly string VisualPenPropertyName = "VisualPen";
        public Pen VisualPen
        {
            get { return _visualPen; }
            set { SetMember<Pen>(ref value, ref _visualPen, _visualPen == value, VisualPenPropertyName); }
        }
        private Brush _visualBrush = Application.Current.TryFindResource(CustomResources.ReinfBrush1_SCkey) as Brush;
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
            if (Common.IsEmpty(_barData))
            {
                return myPathGeometry;
            }
            foreach (CssDataOneReinf iter in _barData)
            {
                EllipseGeometry circle = new EllipseGeometry(iter.BarPoint, iter.Diam / 2, iter.Diam / 2);
                myPathGeometry.AddGeometry(circle);
            }
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }
        #endregion

        #region XEP_ICssDataReinforcement Members
        ObservableCollection<XEP_ICssDataOneReinf> _barData = new ObservableCollection<XEP_ICssDataOneReinf>();
        public static readonly string BarDataPropertyName = "BarData";
        public ObservableCollection<XEP_ICssDataOneReinf> BarData
        {
            get { return _barData; }
            set { SetMember<ObservableCollection<XEP_ICssDataOneReinf>>(ref value, ref _barData, _barData == value, BarDataPropertyName); }
        }
        #endregion
    }
}
