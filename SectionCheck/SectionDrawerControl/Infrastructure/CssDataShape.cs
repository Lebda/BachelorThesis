using System;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionDrawer.Infrastructure
{
    [Serializable]
    public class CssDataShape : CssDataBase
    {
        // Members
        PointCollection _cssShapeOuterInternal = new PointCollection();
        ObservableCollection<XEP_ISectionShapeItem> _cssShapeOuter = new ObservableCollection<XEP_ISectionShapeItem>();
        PointCollection _cssShapeInnerInternal = new PointCollection();
        ObservableCollection<XEP_ISectionShapeItem> _cssShapeInner = new ObservableCollection<XEP_ISectionShapeItem>();
        // ctor
         public CssDataShape()
            : base(Application.Current.TryFindResource(CustomResources.CssBrush1_SCkey) as Brush,
                    Application.Current.TryFindResource(CustomResources.CssPen1_SCkey) as Pen)
        {
        }
         public CssDataShape(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
            TransformOuter();
            TransformInner();
        }

        public CssDataShape CopyInstance()
        {
            CssDataShape data = new CssDataShape(GetBrush(), GetPen());
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
        public override PathGeometry Create()
        {
            TransformOuter();
            TransformInner();
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeOuterInternal));
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeInnerInternal));
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }

        public static readonly string CssShapeOuterPropertyName = "CssShapeOuter";
        public ObservableCollection<XEP_ISectionShapeItem> CssShapeOuter
        {
            get { return _cssShapeOuter; }
            set { SetMember<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _cssShapeOuter, (_cssShapeOuter == value), TransformOuter, CssShapeOuterPropertyName); }
        }
        public static readonly string CssShapeInnerPropertyName = "CssShapeInner";
        public ObservableCollection<XEP_ISectionShapeItem> CssShapeInner
        {
            get { return _cssShapeInner; }
            set { SetMember<ObservableCollection<XEP_ISectionShapeItem>>(ref value, ref _cssShapeInner, (_cssShapeInner == value), TransformInner, CssShapeInnerPropertyName); }
        }

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
