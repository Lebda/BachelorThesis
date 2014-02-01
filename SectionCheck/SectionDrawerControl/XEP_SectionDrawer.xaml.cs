using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using XEP_CommonLibrary.Geometry;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_SectionDrawer
{
    public partial class XEP_SectionDrawerUC : UserControl
    {
        private const double _axisOverLap = 0.2;
        private const double _axisArrowSize = 0.02;
        public XEP_SectionDrawerUC()
        {
            InitializeComponent();
            Exceptions.CheckNull<XEP_DrawingCanvas>(drawingSurface);
            for (int counter = 0; counter < (int)eUsedVisuals.eUsedVisualEnd; ++counter)
            {
                VisualObjectData visual = new VisualObjectData(new DrawingVisual());
               switch ((eUsedVisuals)counter)
               {
                   case eUsedVisuals.eCssShapeVisual:
                       visual.Delagate4Draw = DrawCssShape;
                       visual.CallBack4Change += CalculateAxisHorizontalFromCssShape;
                       visual.CallBack4Change += CalculateAxisVerticalFromCssShape;
                       visual.CallBack4Change += CalculateStrainShapeConcrete;
                       visual.CallBack4Change += CalculateStressShapeConcrete;
                       visual.CallBack4Change += CalculateStrainShapeReinforcement;
                       visual.CallBack4Change += CalculateStressShapeReinforcement;
                       break;
                   case eUsedVisuals.eCssCompressPartVisual:
                       visual.Delagate4Draw += DrawCssCompressPart;
                       break;
                   case eUsedVisuals.eCssReinforcementVisual:
                       visual.Delagate4Draw += DrawCssReinforcemnt;
                       break;
                   case eUsedVisuals.eCssFibersConcreteStrainVisual:
                       visual.Delagate4Draw += DrawFibersStrainConcrete;
                       visual.CallBack4Change += CalculateStrainShapeConcrete;
                       visual.CallBack4Change += CalculateStrainShapeReinforcement;
                       break;
                   case eUsedVisuals.eCssFibersConcreteStressVisual:
                       visual.Delagate4Draw += DrawFibersStressConcrete;
                       visual.CallBack4Change += CalculateStressShapeConcrete;
                       visual.CallBack4Change += CalculateStressShapeReinforcement;
                       break;
                   case eUsedVisuals.eCssFibersReinforcementStrainVisual:
                       visual.Delagate4Draw += DrawFibersStrainReinforcement;
                       visual.CallBack4Change += CalculateStrainShapeReinforcement;
                       break;
                   case eUsedVisuals.eCssFibersReinforcementStressVisual:
                       visual.Delagate4Draw += DrawFibersStressReinforcement;
                       visual.CallBack4Change += CalculateStressShapeReinforcement;
                       break;
                   case eUsedVisuals.eCssAxisHorizontalVisual:
                      visual.Delagate4Draw += DrawCssAxisHorizontal;
                       break;
                   case eUsedVisuals.eCssAxisVerticalVisual:
                      visual.Delagate4Draw += DrawCssAxisVertical;
                       break;
                   default:
                       Exceptions.CheckNull(null);
                       break;
               }
               drawingSurface.AddVisual(visual);
            }
        }

#region DRAWING FUNCTIONS DEFINITIONS
        private static void DrawInternal(VisualObjectData visual, Func<IVisualObejctDrawingData> propertyGetter, 
            params Func<IVisualObejctDrawingData>[] neededPropertyGetters)
        {
            IVisualObejctDrawingData obejctPropertyGetter = (IVisualObejctDrawingData)propertyGetter();
            if (obejctPropertyGetter == null)
            {
                return;
            }
            foreach (var iter in neededPropertyGetters)
            {
                if (iter() == null)
                {
                    return;
                }
            }
            using (DrawingContext dc = Exceptions.CheckNull<DrawingVisual>(Exceptions.CheckNull<VisualObjectData>(visual).VisualObject).RenderOpen())
            {
                Exceptions.CheckNullArgument(null, visual.VisualShape);
                dc.DrawGeometry(obejctPropertyGetter.GetBrush(), obejctPropertyGetter.GetPen(), visual.VisualShape.RenderedGeo);
            }
        }
        private void DrawFibersStrainReinforcement(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssFibersReinforcement4Draw);
        }
        private void DrawFibersStressReinforcement(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssFibersReinforcement4Draw);
        }
        private void DrawFibersStressConcrete(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssFibersConcrete4Draw);
        }
        private void DrawFibersStrainConcrete(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssFibersConcrete4Draw);
        }
        private void DrawCssShape(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssShape4Draw);
        }
        private void DrawCssCompressPart(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssCompressPart4Draw, () => CssShape4Draw);
        }
        private void DrawCssReinforcemnt(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssReinforcement4Draw, () => CssShape4Draw);
        }
        private void DrawCssAxisHorizontal(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssAxisHorizontal4Draw, () => CssShape4Draw);
        }
        private void DrawCssAxisVertical(VisualObjectData visual)
        {
            DrawInternal(visual, () => CssAxisVertical4Draw, () => CssShape4Draw);
        }
#endregion

#region PROPERTY CALLBACKS
        private static void OnFibersReinforcementChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataFibers>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssFibersReinforcement4Draw = value, eUsedVisuals.eCssFibersReinforcementStrainVisual);
            OnChangedGeneric<XEP_ICssDataFibers>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssFibersReinforcement4Draw = value, eUsedVisuals.eCssFibersReinforcementStressVisual);
        }
        private static void OnFibersConcreteChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataFibers>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssFibersConcrete4Draw = value, eUsedVisuals.eCssFibersConcreteStrainVisual);
            OnChangedGeneric<XEP_ICssDataFibers>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssFibersConcrete4Draw = value, eUsedVisuals.eCssFibersConcreteStressVisual);
        }
        private static void OnReinforcementChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
           OnChangedGeneric<XEP_ICssDataReinforcement>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssReinforcement4Draw = value, eUsedVisuals.eCssReinforcementVisual);
        }
        private static void OnCompressPartChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataCompressPart>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssCompressPart4Draw = value, eUsedVisuals.eCssCompressPartVisual);
        }
        private static void OnAxisVerticalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataBase>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssAxisVertical4Draw = value, eUsedVisuals.eCssAxisVerticalVisual);
        }
        private static void OnAxisHorizontalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataBase>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssAxisHorizontal4Draw = value, eUsedVisuals.eCssAxisHorizontalVisual);
        }
        private static void OnShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<XEP_ICssDataShape>(sender, e, value => ((XEP_SectionDrawerUC)sender).CssShape4Draw = value, eUsedVisuals.eCssShapeVisual);
        }
        //
        private static void OnChangedGeneric<T>(DependencyObject sender, DependencyPropertyChangedEventArgs e, Action<T> targetCall, eUsedVisuals visualType)
        {
            XEP_SectionDrawerUC drawignControl = (XEP_SectionDrawerUC)sender;
            Common.SetProperty<T>((T)e.NewValue, targetCall);
            drawignControl.OnChangedInternal(e, visualType);
            RenderAll(drawignControl);
        }
        private void OnChangedInternal(DependencyPropertyChangedEventArgs e, eUsedVisuals visualType)
        {
            Exceptions.CheckNull(drawingSurface);
            VisualObjectData visual = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)visualType));
            XEP_ICssDataBase newShape = Exceptions.CheckNull<XEP_ICssDataBase>((XEP_ICssDataBase)e.NewValue);
            Exceptions.CheckNull(visual.VisualShape);
            Common.SetPropertyIfNotNull<PathGeometry>(newShape.Create(), value => visual.VisualShape.BaseGeo = value);
            visual.CallBack4ShapeChanged();
        }
        private static void RenderAll(XEP_SectionDrawerUC drawignControl)
        {
            Exceptions.CheckNull(Exceptions.CheckNull<XEP_SectionDrawerUC>(drawignControl).drawingSurface);
            Rect bounds = drawignControl.drawingSurface.RecalculateBounds();
            // recalculate scale
            MatrixTransform conventer = GetTransformMatrix(ref bounds, drawignControl.drawingSurface);
            // transform all
            drawignControl.drawingSurface.TransformAll(conventer);
            // render all
            drawignControl.drawingSurface.DrawAll();
        }
        private static MatrixTransform GetTransformMatrix(ref Rect bounds, XEP_DrawingCanvas drawingSurface)
        {
            Matrix conventer = Exceptions.CheckNull<Matrix>(new Matrix());
            conventer.Translate(-bounds.TopLeft.X, -bounds.TopLeft.Y);
            double scale = Math.Min(Math.Abs(drawingSurface.ActualWidth / bounds.Width), Math.Abs(drawingSurface.ActualHeight / bounds.Height));
            conventer.ScaleAt(scale, scale, 0.0, 0.0);
            return Exceptions.CheckNull<MatrixTransform>(new MatrixTransform(conventer));
        }
#endregion

#region GEOMETRY CALCULATIONS
        private void CalculateStrainShapeConcrete(VisualObjectData visualAct)
        {
            if (!Exceptions.CheckIsNull(CssFibersConcrete4Draw, CssShape4Draw)) { return; }
            Exceptions.CheckNull(visualAct);
            VisualObjectData visualCssShape = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssShapeVisual));
            VisualObjectData visualStrainConcrete = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssFibersConcreteStrainVisual));
            visualStrainConcrete.VisualShape.BaseGeo = CssFibersConcrete4Draw.GetStrainGeometry(visualCssShape.VisualShape.BaseGeo.Bounds.Width);
        }

        private void CalculateStrainShapeReinforcement(VisualObjectData visualAct)
        {
            if (!Exceptions.CheckIsNull(CssFibersReinforcement4Draw, CssFibersConcrete4Draw, CssShape4Draw)) { return; }
            Exceptions.CheckNull(visualAct);
            VisualObjectData visualCssShape = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssShapeVisual));
            VisualObjectData visualStrainReinf = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssFibersReinforcementStrainVisual));
            // Recalc it for strain
            Rect boundCss = visualCssShape.VisualShape.BaseGeo.Bounds;
            CssFibersConcrete4Draw.GetStrainGeometry(boundCss.Width);
            IStrainStressShape shapeMaker = Exceptions.CheckNull(CssFibersConcrete4Draw.GeometryMaker.ShapeMaker);
            visualStrainReinf.VisualShape.BaseGeo = CssFibersReinforcement4Draw.GetStrainGeometry(boundCss.Width, true, shapeMaker);
        }

        private void CalculateStressShapeConcrete(VisualObjectData visualAct)
        {
            if (!Exceptions.CheckIsNull(CssFibersConcrete4Draw, CssShape4Draw)) { return; }
            Exceptions.CheckNull(visualAct);
            VisualObjectData visualCssShape = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssShapeVisual));
            Rect boundCss = visualCssShape.VisualShape.BaseGeo.Bounds;
            VisualObjectData visualStressConcrete = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssFibersConcreteStressVisual));
            visualStressConcrete.VisualShape.BaseGeo = CssFibersConcrete4Draw.GetStressGeometry(boundCss.Width);
        }

        private void CalculateStressShapeReinforcement(VisualObjectData visualAct)
        {
            if (!Exceptions.CheckIsNull(CssFibersReinforcement4Draw, CssFibersConcrete4Draw, CssShape4Draw)) { return; }
            Exceptions.CheckNull(visualAct);
            VisualObjectData visualCssShape = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssShapeVisual));
            VisualObjectData visualStressReinf = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssFibersReinforcementStressVisual));
            // Recalc it for strain
            Rect boundCss = visualCssShape.VisualShape.BaseGeo.Bounds;
            CssFibersConcrete4Draw.GetStressGeometry(boundCss.Width);
            IStrainStressShape shapeMaker = Exceptions.CheckNull(CssFibersConcrete4Draw.GeometryMaker.ShapeMaker);
            visualStressReinf.VisualShape.BaseGeo = CssFibersReinforcement4Draw.GetStressGeometry(boundCss.Width, true, shapeMaker);
        }

        private void CalculateAxisHorizontalFromCssShape(VisualObjectData visualCssShape)
        {
            if (CssShape4Draw == null)
            {
                return;
            }
            VisualObjectData visualAxis = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisHorizontalVisual));
            PathGeometry baseGeoCssShape = Exceptions.CheckNull<PathGeometry>(Exceptions.CheckNull<IVisualShapes>(Exceptions.CheckNull<VisualObjectData>(visualCssShape).VisualShape).BaseGeo);
            Rect bounds = baseGeoCssShape.Bounds;
            // axis line
            PointCollection axisGeo = new PointCollection();
            double overLap = bounds.Height * _axisOverLap;
            double extremeLeft = GeometryOperations.Add4Sign(bounds.Left, overLap);
            axisGeo.Add(new Point(extremeLeft, 0.0));
            double exteremeRight = GeometryOperations.Add4Sign(bounds.Right, overLap);
            axisGeo.Add(new Point(exteremeRight, 0.0));
            double arrowOverLap = bounds.Height * _axisArrowSize;
            // arrow
            axisGeo.Add(new Point(exteremeRight, arrowOverLap));
            axisGeo.Add(new Point(exteremeRight + 2 * arrowOverLap, 0.0));
            axisGeo.Add(new Point(exteremeRight, -arrowOverLap));
            axisGeo.Add(new Point(exteremeRight, 0.0));
            PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
            myPathGeometry.Figures.Add(GeometryOperations.Create(axisGeo));
            myPathGeometry.FillRule = FillRule.Nonzero;
            visualAxis.VisualShape.BaseGeo = myPathGeometry;
        }

        private void CalculateAxisVerticalFromCssShape(VisualObjectData visualCssShape)
        {
            if (CssShape4Draw == null)
            {
                return;
            }
            VisualObjectData visualAxis = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisVerticalVisual));
            PathGeometry baseGeoCssShape = Exceptions.CheckNull<PathGeometry>(Exceptions.CheckNull<IVisualShapes>(Exceptions.CheckNull<VisualObjectData>(visualCssShape).VisualShape).BaseGeo);
            Rect bounds = baseGeoCssShape.Bounds;
            // axis line
            PointCollection axisGeo = new PointCollection();
            double overLap = bounds.Height * _axisOverLap;
            double extremeBotton = GeometryOperations.Add4Sign(bounds.Bottom, overLap);
            axisGeo.Add(new Point(0.0, extremeBotton));
            double exteremeTop = GeometryOperations.Add4Sign(bounds.Top, overLap);
            axisGeo.Add(new Point(0.0, exteremeTop));
            double arrowOverLap = bounds.Height * _axisArrowSize;
            // arrow
            axisGeo.Add(new Point(arrowOverLap, exteremeTop));
            axisGeo.Add(new Point(0.0, exteremeTop - 2 * arrowOverLap));
            axisGeo.Add(new Point(-arrowOverLap, exteremeTop));
            axisGeo.Add(new Point(0.0, exteremeTop));
            PathGeometry myPathGeometry = Exceptions.CheckNull<PathGeometry>(new PathGeometry());
            myPathGeometry.Figures.Add(GeometryOperations.Create(axisGeo));
            myPathGeometry.FillRule = FillRule.Nonzero;
            visualAxis.VisualShape.BaseGeo = myPathGeometry;
        }

#endregion

#region DEPENDENCY PROPERTY DEFINITIONS
        static XEP_SectionDrawerUC()
        {
            CssShape4DrawProperty = DependencyProperty.Register(CssShape4DrawPropertyName, typeof(XEP_ICssDataShape), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnShapeChanged)));
            CssAxisHorizontalDrawProperty = DependencyProperty.Register(CssAxisHorizontal4DrawPropertyName, typeof(XEP_ICssDataBase), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisHorizontalChanged)));
            CssAxisVerticalDrawProperty = DependencyProperty.Register(CssAxisVertical4DrawPropertyName, typeof(XEP_ICssDataBase), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisVerticalChanged)));
            CssCompressPart4DrawProperty = DependencyProperty.Register(CssCompressPart4DrawPropertyName, typeof(XEP_ICssDataCompressPart), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCompressPartChanged)));
            CssReinforcement4DrawProperty = DependencyProperty.Register(CssReinforcement4DrawPropertyName, typeof(XEP_ICssDataReinforcement), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnReinforcementChanged)));
            CssFibersConcrete4DrawProperty = DependencyProperty.Register(CssFibersConcrete4DrawPropertyName, typeof(XEP_ICssDataFibers), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnFibersConcreteChanged)));
            CssFibersReinforcement4DrawProperty = DependencyProperty.Register(CssFibersReinforcement4DrawPropertyName, typeof(XEP_ICssDataFibers), typeof(XEP_SectionDrawerUC),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnFibersReinforcementChanged)));
        }
        private static string CssFibersReinforcement4DrawPropertyName = "CssFibersReinforcement4Draw";
        public static DependencyProperty CssFibersReinforcement4DrawProperty;
        public XEP_ICssDataFibers CssFibersReinforcement4Draw
        {
            get { return (XEP_ICssDataFibers)GetValue(CssFibersReinforcement4DrawProperty); }
            set { SetValue(CssFibersReinforcement4DrawProperty, value); }
        }
        private static string CssFibersConcrete4DrawPropertyName = "CssFibersConcrete4Draw";
        public static DependencyProperty CssFibersConcrete4DrawProperty;
        public XEP_ICssDataFibers CssFibersConcrete4Draw
        {
            get { return (XEP_ICssDataFibers)GetValue(CssFibersConcrete4DrawProperty); }
            set { SetValue(CssFibersConcrete4DrawProperty, value); }
        }
        private static string CssReinforcement4DrawPropertyName = "CssReinforcement4Draw";
        public static DependencyProperty CssReinforcement4DrawProperty;
        public XEP_ICssDataReinforcement CssReinforcement4Draw
        {
            get { return (XEP_ICssDataReinforcement)GetValue(CssReinforcement4DrawProperty); }
            set { SetValue(CssReinforcement4DrawProperty, value); }
        }
        private static string CssCompressPart4DrawPropertyName = "CssCompressPart4Draw";
        public static DependencyProperty CssCompressPart4DrawProperty;
        public XEP_ICssDataCompressPart CssCompressPart4Draw
        {
            get { return (XEP_ICssDataCompressPart)GetValue(CssCompressPart4DrawProperty); }
            set { SetValue(CssCompressPart4DrawProperty, value); }
        }
        private static string CssShape4DrawPropertyName = "CssShape4Draw";
        public static DependencyProperty CssShape4DrawProperty;
        public XEP_ICssDataShape CssShape4Draw
        {
            get { return (XEP_ICssDataShape)GetValue(CssShape4DrawProperty); }
            set { SetValue(CssShape4DrawProperty, value); }
        }
        private static string CssAxisHorizontal4DrawPropertyName = "CssAxisHorizontal4Draw";
        public static DependencyProperty CssAxisHorizontalDrawProperty;
        public XEP_ICssDataBase CssAxisHorizontal4Draw
        {
            get { return (XEP_ICssDataBase)GetValue(CssAxisHorizontalDrawProperty);}
            set { SetValue(CssAxisHorizontalDrawProperty, value); }
        }
        private static string CssAxisVertical4DrawPropertyName = "CssAxisVertical4Draw";
        public static DependencyProperty CssAxisVerticalDrawProperty;
        public XEP_ICssDataBase CssAxisVertical4Draw
        {
            get { return (XEP_ICssDataBase)GetValue(CssAxisVerticalDrawProperty); }
            set { SetValue(CssAxisVerticalDrawProperty, value); }
        }
#endregion
    }
}
