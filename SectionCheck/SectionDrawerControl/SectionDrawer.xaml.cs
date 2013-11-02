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

namespace SectionDrawerControl
{
    public partial class SectionDrawer : UserControl
    {
        static Brush compressionPartBrush = null;
        private const double _axisOverLap = 0.2;
        private const double _axisArrowSize = 0.02;
        public SectionDrawer()
        {
            InitializeComponent();
            for (int counter = 0; counter < (int)eUsedVisuals.eUsedVisualEnd; ++counter)
            {
                VisualObjectData visual = new VisualObjectData(new DrawingVisual());
               switch ((eUsedVisuals)counter)
               {
                   case eUsedVisuals.eCssShapeVisual:
                       visual.Delagate4Draw = DrawCssShape;
                       //visual.CallBack4ShapeChange += CalculateAxisHorizontalFromCssShape;
                       //visual.CallBack4ShapeChange += CalculateAxisVerticalFromCssShape;
                       break;
                   case eUsedVisuals.eCssCompressPartVisual:
                       visual.Delagate4Draw += DrawCssCompressPart;
                       break;
                   case eUsedVisuals.eCssReinforcementVisual:
                       visual.Delagate4Draw += DrawCssReinforcemnt;
                       break;

//                    case eUsedVisuals.eCssAxisHorizontalVisual:
//                       visual.Delagate4Draw += DrawCssAxisHorizontal;
//                        break;
//                    case eUsedVisuals.eCssAxisVerticalVisual:
//                       visual.Delagate4Draw += DrawCssAxisVertical;
//                        break;
                   default:
                       throw new ApplicationException();
               }
               drawingSurface.AddVisual(visual);
            }
        }

    #region Drawing functions
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
            Exceptions.CheckNullArgument(null, visual);
            Exceptions.CheckNullArgument(null, visual.VisualObject);
            Exceptions.CheckNullArgument(null, obejctPropertyGetter.GetPen());
            Exceptions.CheckNullArgument(null, obejctPropertyGetter.GetBrush());
            using (DrawingContext dc = visual.VisualObject.RenderOpen())
            {
                Exceptions.CheckNullArgument(null, visual.VisualShape);
                dc.DrawGeometry(obejctPropertyGetter.GetBrush(), obejctPropertyGetter.GetPen(), visual.VisualShape.RenderedGeo);
            }
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
//         private void DrawPointCollection(DrawingVisual visual, PointCollection shape)
//         {
//             using (DrawingContext dc = visual.RenderOpen())
//             {
//                 dc.DrawGeometry(null, drawingPen, VisualObjectData.CreateGeometryFromPolygon(shape));
//             }
//         }
        private void DrawCssAxisHorizontal(VisualObjectData visual)
        {
//             if (CssAxisHorizontal4Draw == null)
//             {
//                 return;
//             }
//             using (DrawingContext dc = visual.VisualObject.RenderOpen())
//             {
//                 dc.DrawGeometry(Brushes.IndianRed, new Pen(Brushes.IndianRed, 2), visual.CreateRenderedGeometry(CssDataShape.CssShapeOuterPos, FillRule.Nonzero, false));
//             }
        }
        private void DrawCssAxisVertical(VisualObjectData visual)
        {
//             if (CssAxisHorizontal4Draw == null)
//             {
//                 return;
//             }
//             using (DrawingContext dc = visual.VisualObject.RenderOpen())
//             {
//                 dc.DrawGeometry(Brushes.IndianRed, new Pen(Brushes.IndianRed, 2), visual.CreateRenderedGeometry(CssDataShape.CssShapeOuterPos, FillRule.Nonzero, false));
//             }
        }
    #endregion

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
            //
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(
              "ResourceLibrary;component/Themes/generic.xaml", UriKind.Relative);
            compressionPartBrush = (Brush)resourceDictionary["CompressionPartBrush"];
        }
#region PROPERTY CALLBACKS
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
            //OnChangedGeneric<CssDataAxis>(sender, e, value => ((SectionDrawer)sender).CssAxisHorizontal4Draw = value, eUsedVisuals.eCssAxisVerticalVisual);
        }
        private static void OnAxisHorizontalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //OnChangedGeneric<CssDataAxis>(sender, e, value => ((SectionDrawer)sender).CssAxisHorizontal4Draw = value, eUsedVisuals.eCssAxisHorizontalVisual);
        }
        private static void OnShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            OnChangedGeneric<CssDataShape>(sender, e, value => ((SectionDrawer)sender).CssShape4Draw = value, eUsedVisuals.eCssShapeVisual);
        }
        //
        private static void OnChangedGeneric<T>(DependencyObject sender, DependencyPropertyChangedEventArgs e, Action<T> targetCall, eUsedVisuals visualType)
        {
            SectionDrawer drawignControl = (SectionDrawer)sender;
            SetProperty<T>(e, targetCall);
            drawignControl.OnChangedInternal(e, visualType);
            RenderAll(drawignControl);
        }
        private static void SetProperty<T>(DependencyPropertyChangedEventArgs source, Action<T> targetCall)
        {
            targetCall((T)source.NewValue);
        }
        private void OnChangedInternal(DependencyPropertyChangedEventArgs e, eUsedVisuals visualType)
        {
            VisualObjectData visual = drawingSurface.GetVisual((int)visualType);
            Exceptions.CheckNullAplication(null, visual);
            Exceptions.CheckNullAplication(null, visual.VisualShape);
            CssDataBase newShape = (CssDataBase)e.NewValue;
            Exceptions.CheckNullAplication(null, newShape);
            visual.VisualShape.BaseGeo = newShape.Create();
            visual.CallBack4ShapeChanged();
        }
        private static void RenderAll(SectionDrawer drawignControl)
        {
            Rect bounds = drawignControl.drawingSurface.RecalculateBounds();
            // recalculate scale
            MatrixTransform conventer = GetTransformMatrix(ref bounds, drawignControl.drawingSurface);
            // transform all
            drawignControl.drawingSurface.TransformAll(conventer); // to do scale !!
            // render all
            drawignControl.drawingSurface.DrawAll();
        }
        private static MatrixTransform GetTransformMatrix(ref Rect bounds, DrawingCanvas drawingSurface)
        {
            Matrix conventer = new Matrix();
            conventer.Translate(-bounds.TopLeft.X, -bounds.TopLeft.Y);
            double widthInPixel = drawingSurface.ActualWidth;
            double heightInPixels = drawingSurface.ActualHeight;
            double scaleX = widthInPixel / bounds.Width;
            double scaleY = heightInPixels / bounds.Height;
            double scale = (scaleX > scaleY) ? (scaleY) : (scaleX);
            scale *= 1;
            conventer.ScaleAt(scale, scale, 0.0, 0.0);
            MatrixTransform convetProvider = new MatrixTransform(conventer);
            return convetProvider;
        }
#endregion

#region GEOMETRY CALCULATIONS
        private void CalculateAxisHorizontalFromCssShape(VisualObjectData visualCssShape)
        {
//             VisualObjectData visualAxis = drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisHorizontalVisual);
//             if (visualCssShape.VisualShape == null || visualCssShape.VisualShape.ItemsGeometry == null || visualCssShape.VisualShape.ItemsGeometry.Count == 0)
//             {
//                 return;
//             }
//             Rect bounds = visualCssShape.VisualShape.ItemsGeometry[CssDataShape.CssShapeOuterPos].Bounds;
//             List<IVisualShape> shapes = new List<IVisualShape>();
//             IVisualShape axisHorizontal = VisualFactory.Instance().CreateShape();
//             double overLap = bounds.Height * _axisOverLap;
//             double extremeLeft = GeometryOperations.Add4Sign(bounds.Left, overLap);
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(extremeLeft, 0.0));
//             double exteremeRight = GeometryOperations.Add4Sign(bounds.Right, overLap);
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(exteremeRight, 0.0));
//             double arrowOverLap = bounds.Height * _axisArrowSize;
//             // arrow
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(exteremeRight, arrowOverLap));
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(exteremeRight + 2 * arrowOverLap, 0.0));
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(exteremeRight, -arrowOverLap));
//             axisHorizontal.Items.Add(VisualFactory.Instance().CreateItem(exteremeRight, 0.0));
//             shapes.Add(axisHorizontal);
//             visualAxis.VisualShape.SetShapeAndCreateGeometry(shapes, FillRule.EvenOdd, false);
        }

        private void CalculateAxisVerticalFromCssShape(VisualObjectData visualCssShape)
        {
//             VisualObjectData visualAxis = drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisVerticalVisual);
//             if (visualCssShape.VisualShape == null || visualCssShape.VisualShape.ItemsGeometry == null || visualCssShape.VisualShape.ItemsGeometry.Count == 0)
//             {
//                 return;
//             }
//             Rect bounds = visualCssShape.VisualShape.ItemsGeometry[CssDataShape.CssShapeOuterPos].Bounds;
//             List<IVisualShape> shapes = new List<IVisualShape>();
//             IVisualShape axisVertical = VisualFactory.Instance().CreateShape();
//             double overLap = bounds.Height * _axisOverLap;
//             double extremeBotton = GeometryOperations.Add4Sign(bounds.Bottom, overLap);
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(0.0, extremeBotton));
//             double exteremeTop = GeometryOperations.Add4Sign(bounds.Top, overLap);
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(0.0, exteremeTop));
//             double arrowOverLap = bounds.Height * _axisArrowSize;
//             // arrow
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(arrowOverLap, exteremeTop));
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(0.0, exteremeTop - 2 * arrowOverLap));
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(-arrowOverLap, exteremeTop));
//             axisVertical.Items.Add(VisualFactory.Instance().CreateItem(0.0, exteremeTop));
//             shapes.Add(axisVertical);
//             visualAxis.VisualShape.SetShapeAndCreateGeometry(shapes, FillRule.EvenOdd, false);
        }

#endregion

#region DEPENDENCY PROPERTY DEFINITIONS
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




        // Variables for dragging shapes.
//         private bool isDragging = false;
//         private Vector clickOffset;
//         private DrawingVisual selectedVisual;
// 
//         // Variables for drawing the selection square.
//         private bool isMultiSelecting = false;
//         private Point selectionSquareTopLeft;

//         private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//         {
//             Point pointClicked = e.GetPosition(drawingSurface);
// 
//             if (cmdAdd.IsChecked == true)
//             {
//                 VisualObjectData visual = new VisualObjectData();
//                 DrawSquare(visual.VisualObject, pointClicked, false);
//                 drawingSurface.AddVisual(visual);
//             }
//             else if (cmdDelete.IsChecked == true)
//             {
//                 DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
//                 if (visual != null) drawingSurface.DeleteVisual(visual);
//             }
//             else if (cmdSelectMove.IsChecked == true)
//             {
//                 DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
//                 if (visual != null)
//                 {
//                     // Calculate the top-left corner of the square.
//                     // This is done by looking at the current bounds and
//                     // removing half the border (pen thickness).
//                     // An alternate solution would be to store the top-left
//                     // point of every visual in a collection in the 
//                     // DrawingCanvas, and provide this point when hit testing.
//                     Point topLeftCorner = new Point(
//                         visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
//                         visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
//                     DrawSquare(visual, topLeftCorner, true);
// 
//                     clickOffset = topLeftCorner - pointClicked;
//                     isDragging = true;
// 
//                     if (selectedVisual != null && selectedVisual != visual)
//                     {
//                         // The selection has changed. Clear the previous selection.
//                         ClearSelection();
//                     }
//                     selectedVisual = visual;
//                 }
//             }
//             else if (cmdSelectMultiple.IsChecked == true)
//             {
// 
//                 selectionSquare = new DrawingVisual();
// 
//                 drawingSurface.AddVisual(selectionSquare);
// 
//                 selectionSquareTopLeft = pointClicked;
//                 isMultiSelecting = true;
// 
//                 // Make sure we get the MouseLeftButtonUp event even if the user
//                 // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
//                 drawingSurface.CaptureMouse();
//             }
//         }

        // Drawing constants.
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
       // private DrawingVisual selectionSquare;

        // Rendering the square.
        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected) brush = selectedDrawingBrush;

                dc.DrawRectangle(brush, drawingPen,
                    new Rect(topLeftCorner, squareSize));
            }
        }

//         private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
//         {
//             isDragging = false;
// 
//             if (isMultiSelecting)
//             {
//                 // Display all the squares in this region.
//                 RectangleGeometry geometry = new RectangleGeometry(
//                     new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
//                 List<DrawingVisual> visualsInRegion =
//                     drawingSurface.GetVisuals(geometry);
//                 MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));
// 
//                 isMultiSelecting = false;
//                 drawingSurface.DeleteVisual(selectionSquare);
//                 drawingSurface.ReleaseMouseCapture();
//             }
//         }

//         private void ClearSelection()
//         {
//             Point topLeftCorner = new Point(
//                         selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
//                         selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
//             DrawSquare(selectedVisual, topLeftCorner, false);
//             selectedVisual = null;
//         }

//         private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
//         {
//             if (isDragging)
//             {
//                 Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
//                 DrawSquare(selectedVisual, pointDragged, true);
//             }
//             else if (isMultiSelecting)
//             {
//                 Point pointDragged = e.GetPosition(drawingSurface);
//                 DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
//             }
//         }

        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

//         private void DrawSelectionSquare(Point point1, Point point2)
//         {
//             selectionSquarePen.DashStyle = DashStyles.Dash;
// 
//             using (DrawingContext dc = selectionSquare.RenderOpen())
//             {
//                 dc.DrawRectangle(selectionSquareBrush, selectionSquarePen,
//                     new Rect(point1, point2));
//             }
//         }
    }
}
