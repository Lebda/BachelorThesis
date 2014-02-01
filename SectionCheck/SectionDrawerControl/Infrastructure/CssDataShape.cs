using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionDrawer.Infrastructure
{
    [Serializable]
    public class CssDataShape : ObservableObject, XEP_ICssDataShape
    {
        // Members
        PointCollection _cssShapeOuterInternal = new PointCollection();
        PointCollection _cssShapeInnerInternal = new PointCollection();
        // ctor
         public CssDataShape()
        {
            TransformOuter();
            TransformInner();
        }

         #region IVisualObejctDrawingData Members
         private Pen _visualPen = Application.Current.TryFindResource(CustomResources.CssPen1_SCkey) as Pen;
         public static readonly string VisualPenPropertyName = "VisualPen";
         public Pen VisualPen
         {
             get { return _visualPen; }
             set { SetMember<Pen>(ref value, ref _visualPen, _visualPen == value, VisualPenPropertyName); }
         }
         private Brush _visualBrush = Application.Current.TryFindResource(CustomResources.CssBrush1_SCkey) as Brush;
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
             TransformOuter();
             TransformInner();
             PathGeometry myPathGeometry = new PathGeometry();
             myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeOuterInternal));
             myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeInnerInternal));
             myPathGeometry.FillRule = FillRule.Nonzero;
             return myPathGeometry;
         }
         #endregion

         #region ICloneable Members
         public object Clone()
         {
             XEP_ICssDataShape data = new CssDataShape();
             foreach (var item in _cssShapeOuter)
             {
                 data.CssShapeOuter.Add(item.CopyInstance());
             }
             foreach (var item in _cssShapeInner)
             {
                 data.CssShapeInner.Add(item.CopyInstance());
             }
             return data;
         }
         #endregion

         #region XEP_ICssDataShape Members
         ObservableCollection<XEP_ISectionShapeItem> _cssShapeOuter = new ObservableCollection<XEP_ISectionShapeItem>();
         public static readonly string CssShapeOuterPropertyName = "CssShapeOuter";
         public ObservableCollection<XEP_ISectionShapeItem> CssShapeOuter
         {
             get { return _cssShapeOuter; }
             set { SetMember<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _cssShapeOuter, (_cssShapeOuter == value), TransformOuter, CssShapeOuterPropertyName); }
         }
         ObservableCollection<XEP_ISectionShapeItem> _cssShapeInner = new ObservableCollection<XEP_ISectionShapeItem>();
         public static readonly string CssShapeInnerPropertyName = "CssShapeInner";
         public ObservableCollection<XEP_ISectionShapeItem> CssShapeInner
         {
             get { return _cssShapeInner; }
             set { SetMember<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _cssShapeInner, (_cssShapeInner == value), TransformInner, CssShapeInnerPropertyName); }
         }
         #endregion

        #region METHODS
        void TransformOuter()
        {
            _cssShapeOuterInternal = XEP_Conventors.TransformOne(_cssShapeOuter);
            if (_cssShapeOuter.Count != 0)
            {
                _cssShapeOuterInternal.Add(new Point(_cssShapeOuter[0].Y.Value, _cssShapeOuter[0].Z.Value));
            }
        }
        void TransformInner()
        {
            _cssShapeInnerInternal = XEP_Conventors.TransformOne(_cssShapeInner);
            if (_cssShapeInner.Count != 0)
            {
                _cssShapeInnerInternal.Add(new Point(_cssShapeInner[0].Y.Value, _cssShapeInner[0].Z.Value));
            }
        }
        #endregion
    }
}
