using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonLibrary.Infrastructure;
using SectionDrawerControl.Infrastructure;
using ResourceLibrary;
using SectionDrawerControl.Utility;
using CommonLibrary.Utility;
using CommonLibrary.Geometry;

namespace SectionDrawerControl
{
    public partial class SectionDrawer : UserControl
    {
        private const double _axisOverLap = 0.2;
        private const double _axisArrowSize = 0.02;
        public SectionDrawer()
        {
            InitializeComponent();
            Exceptions.CheckNull<DrawingCanvas>(drawingSurface);
            for (int counter = 0; counter < (int)eUsedVisuals.eUsedVisualEnd; ++counter)
            {
                VisualObjectData visual = new VisualObjectData(new DrawingVisual());
               switch ((eUsedVisuals)counter)
               {
                   case eUsedVisuals.eCssShapeVisual:
                       visual.Delagate4Draw = DrawCssShape;
                       visual.CallBack4Change += CalculateAxisHorizontalFromCssShape;
                       visual.CallBack4Change += CalculateAxisVerticalFromCssShape;
                       break;
                   case eUsedVisuals.eCssCompressPartVisual:
                       visual.Delagate4Draw += DrawCssCompressPart;
                       break;
                   case eUsedVisuals.eCssReinforcementVisual:
                       visual.Delagate4Draw += DrawCssReinforcemnt;
                       break;
                   case eUsedVisuals.eCssFibersConcreteStrainVisual:
                       visual.Delagate4Draw += DrawFibersConcreteStrain;
                       visual.CallBack4Change += CalculateStrainShape;
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
        private void DrawFibersConcreteStrain(VisualObjectData visual)
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
        private static void OnFibersConcreteChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataFibers>(sender, e, value => ((SectionDrawer)sender).CssFibersConcrete4Draw = value, eUsedVisuals.eCssFibersConcreteStrainVisual);
        }
        private static void OnReinforcementChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
           OnChangedGeneric<CssDataReinforcement>(sender, e, value => ((SectionDrawer)sender).CssReinforcement4Draw = value, eUsedVisuals.eCssReinforcementVisual);
        }
        private static void OnCompressPartChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataCompressPart>(sender, e, value => ((SectionDrawer)sender).CssCompressPart4Draw = value, eUsedVisuals.eCssCompressPartVisual);
        }
        private static void OnAxisVerticalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataAxis>(sender, e, value => ((SectionDrawer)sender).CssAxisVertical4Draw = value, eUsedVisuals.eCssAxisVerticalVisual);
        }
        private static void OnAxisHorizontalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataAxis>(sender, e, value => ((SectionDrawer)sender).CssAxisHorizontal4Draw = value, eUsedVisuals.eCssAxisHorizontalVisual);
        }
        private static void OnShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataShape>(sender, e, value => ((SectionDrawer)sender).CssShape4Draw = value, eUsedVisuals.eCssShapeVisual);
        }
        //
        private static void OnChangedGeneric<T>(DependencyObject sender, DependencyPropertyChangedEventArgs e, Action<T> targetCall, eUsedVisuals visualType)
        {
            SectionDrawer drawignControl = (SectionDrawer)sender;
            Common.SetProperty<T>((T)e.NewValue, targetCall);
            drawignControl.OnChangedInternal(e, visualType);
            RenderAll(drawignControl);
        }
        private void OnChangedInternal(DependencyPropertyChangedEventArgs e, eUsedVisuals visualType)
        {
            Exceptions.CheckNull(drawingSurface);
            VisualObjectData visual = Exceptions.CheckNull<VisualObjectData>(drawingSurface.GetVisual((int)visualType));
            CssDataBase newShape = Exceptions.CheckNull<CssDataBase>((CssDataBase)e.NewValue);
            Exceptions.CheckNull(visual.VisualShape);
            Common.SetPropertyIfNotNull<PathGeometry>(newShape.Create(), value => visual.VisualShape.BaseGeo = value);
            visual.CallBack4ShapeChanged();
        }
        private static void RenderAll(SectionDrawer drawignControl)
        {
            Exceptions.CheckNull(Exceptions.CheckNull<SectionDrawer>(drawignControl).drawingSurface);
            Rect bounds = drawignControl.drawingSurface.RecalculateBounds();
            // recalculate scale
            MatrixTransform conventer = GetTransformMatrix(ref bounds, drawignControl.drawingSurface);
            // transform all
            drawignControl.drawingSurface.TransformAll(conventer);
            // render all
            drawignControl.drawingSurface.DrawAll();
        }
        private static MatrixTransform GetTransformMatrix(ref Rect bounds, DrawingCanvas drawingSurface)
        {
            Matrix conventer = Exceptions.CheckNull<Matrix>(new Matrix());
            conventer.Translate(-bounds.TopLeft.X, -bounds.TopLeft.Y);
            double widthInPixel = drawingSurface.ActualWidth;
            double heightInPixels = drawingSurface.ActualHeight;
            double scaleX = widthInPixel / bounds.Width;
            double scaleY = heightInPixels / bounds.Height;
            double scale = (scaleX > scaleY) ? (scaleY) : (scaleX);
            scale *= 1;
            conventer.ScaleAt(scale, scale, 0.0, 0.0);
            return Exceptions.CheckNull<MatrixTransform>(new MatrixTransform(conventer));
        }
#endregion

#region GEOMETRY CALCULATIONS
        private void CalculateStrainShape(VisualObjectData visualFibersConcreteStain)
        {
            Exceptions.CheckNull(visualFibersConcreteStain);
            VisualObjectData visualCssShape = Exceptions.CheckNull<VisualObjectData>(Exceptions.CheckNull<DrawingCanvas>(drawingSurface).GetVisual((int)eUsedVisuals.eCssShapeVisual));
            Rect boundCss = visualCssShape.VisualShape.BaseGeo.Bounds;
            Rect boundStrain = visualFibersConcreteStain.VisualShape.BaseGeo.Bounds;
            double scaleX = boundCss.Width / boundStrain.Width;



            //GeometryOperations.TransformOne(new Matrix(scaleX, scaleX, 0.0, 1.0, 0.0/*2*boundCss.Width*/, 0.0), visualFibersConcreteStain.VisualShape.BaseGeo.Figures);
        }
        private void CalculateAxisHorizontalFromCssShape(VisualObjectData visualCssShape)
        {
            VisualObjectData visualAxis = Exceptions.CheckNull<VisualObjectData>(Exceptions.CheckNull<DrawingCanvas>(drawingSurface).GetVisual((int)eUsedVisuals.eCssAxisHorizontalVisual));
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
            VisualObjectData visualAxis = Exceptions.CheckNull<VisualObjectData>(Exceptions.CheckNull<DrawingCanvas>(drawingSurface).GetVisual((int)eUsedVisuals.eCssAxisVerticalVisual));
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
        static SectionDrawer()
        {
            CssShape4DrawProperty = DependencyProperty.Register(CssShape4DrawPropertyName, typeof(CssDataShape), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnShapeChanged)));
            CssAxisHorizontalDrawProperty = DependencyProperty.Register(CssAxisHorizontal4DrawPropertyName, typeof(CssDataAxis), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisHorizontalChanged)));
            CssAxisVerticalDrawProperty = DependencyProperty.Register(CssAxisVertical4DrawPropertyName, typeof(CssDataAxis), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisVerticalChanged)));
            CssCompressPart4DrawProperty = DependencyProperty.Register(CssCompressPart4DrawPropertyName, typeof(CssDataCompressPart), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnCompressPartChanged)));
            CssReinforcement4DrawProperty = DependencyProperty.Register(CssReinforcement4DrawPropertyName, typeof(CssDataReinforcement), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnReinforcementChanged)));
            CssFibersConcrete4DrawProperty = DependencyProperty.Register(CssFibersConcrete4DrawPropertyName, typeof(CssDataFibers), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnFibersConcreteChanged)));
        }

        private static string CssFibersConcrete4DrawPropertyName = "CssFibersConcrete4Draw";
        public static DependencyProperty CssFibersConcrete4DrawProperty;
        public CssDataFibers CssFibersConcrete4Draw
        {
            get { return (CssDataFibers)GetValue(CssFibersConcrete4DrawProperty); }
            set { SetValue(CssFibersConcrete4DrawProperty, value); }
        }
        private static string CssReinforcement4DrawPropertyName = "CssReinforcement4Draw";
        public static DependencyProperty CssReinforcement4DrawProperty;
        public CssDataReinforcement CssReinforcement4Draw
        {
            get { return (CssDataReinforcement)GetValue(CssReinforcement4DrawProperty); }
            set { SetValue(CssReinforcement4DrawProperty, value); }
        }
        private static string CssCompressPart4DrawPropertyName = "CssCompressPart4Draw";
        public static DependencyProperty CssCompressPart4DrawProperty;
        public CssDataCompressPart CssCompressPart4Draw
        {
            get { return (CssDataCompressPart)GetValue(CssCompressPart4DrawProperty); }
            set { SetValue(CssCompressPart4DrawProperty, value); }
        }
        private static string CssShape4DrawPropertyName = "CssShape4Draw";
        public static DependencyProperty CssShape4DrawProperty;
        public CssDataShape CssShape4Draw
        {
            get { return (CssDataShape)GetValue(CssShape4DrawProperty); }
            set { SetValue(CssShape4DrawProperty, value); }
        }
        private static string CssAxisHorizontal4DrawPropertyName = "CssAxisHorizontal4Draw";
        public static DependencyProperty CssAxisHorizontalDrawProperty;
        public CssDataAxis CssAxisHorizontal4Draw
        {
            get { return (CssDataAxis)GetValue(CssAxisHorizontalDrawProperty);}
            set { SetValue(CssAxisHorizontalDrawProperty, value); }
        }
        private static string CssAxisVertical4DrawPropertyName = "CssAxisVertical4Draw";
        public static DependencyProperty CssAxisVerticalDrawProperty;
        public CssDataAxis CssAxisVertical4Draw
        {
            get { return (CssDataAxis)GetValue(CssAxisVerticalDrawProperty); }
            set { SetValue(CssAxisVerticalDrawProperty, value); }
        }
#endregion
    }
}
