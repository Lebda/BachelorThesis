using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Infrastructure;

namespace XEP_SectionDrawer.Infrastructure
{
    public class CssDataAxis : ObservableObject, XEP_ICssDataBase
    {
        public CssDataAxis()
        {

        }

        #region IVisualObejctDrawingData Members
        private Pen _visualPen = Application.Current.TryFindResource(CustomResources.HorAxisPen1_SCkey) as Pen;
        public static readonly string VisualPenPropertyName = "VisualPen";
        public Pen VisualPen
        {
            get { return _visualPen; }
            set { SetMember<Pen>(ref value, ref _visualPen, _visualPen == value, VisualPenPropertyName); }
        }
        private Brush _visualBrush = Application.Current.TryFindResource(CustomResources.HorAxisBrush1_SCkey) as Brush;
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
            return null;
        }
        #endregion
    }
}
