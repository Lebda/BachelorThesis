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
using ShapeType = System.Collections.Generic.List<System.Windows.Media.PointCollection>;
using SectionDrawerControl.Utility;

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
                       visual.CallBack4ShapeChange += CalculateAxisHorizontalFromCssShape;
                       visual.CallBack4ShapeChange += CalculateAxisVerticalFromCssShape;
                       break;
                   case eUsedVisuals.eCssAxisHorizontalVisual:
                       visual.Delagate4Draw = DrawCssAxisHorizontal;
                       break;
                   case eUsedVisuals.eCssAxisVerticalVisual:
                       visual.Delagate4Draw = DrawCssAxisVertical;
                       break;
                   default:
                       throw new ApplicationException();
               }
               drawingSurface.AddVisual(visual);
            }
        }

    #region Drawing functions
        private void DrawPointCollection(DrawingVisual visual, PointCollection shape)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawGeometry(null, drawingPen, VisualObjectData.CreateGeometryFromPolygon(shape));
            }
        }
        private void DrawCssShape(VisualObjectData visual)
        {
            using (DrawingContext dc = visual.VisualObject.RenderOpen())
            {
                dc.DrawGeometry(compressionPartBrush, drawingPen, CreateGeometryFromPolygons(visual.ShapeRendered[CssDataShape.CssShapeOuterPos], visual.ShapeRendered[CssDataShape.CssShapeInnerPos]));
            }
        }
        private void DrawCssAxisHorizontal(VisualObjectData visual)
        {
            if (CssAxisHorizontal4Draw == null)
            {
                return;
            }
            using (DrawingContext dc = visual.VisualObject.RenderOpen())
            {
                dc.DrawGeometry(Brushes.IndianRed, new Pen(Brushes.IndianRed, 2), VisualObjectData.CreateGeometryFromPolygon(visual.ShapeRendered[CssDataShape.CssShapeOuterPos], FillRule.Nonzero, false));
            }
        }
        private void DrawCssAxisVertical(VisualObjectData visual)
        {
            if (CssAxisHorizontal4Draw == null)
            {
                return;
            }
            using (DrawingContext dc = visual.VisualObject.RenderOpen())
            {
                dc.DrawGeometry(Brushes.BlueViolet, new Pen(Brushes.BlueViolet, 2), VisualObjectData.CreateGeometryFromPolygon(visual.ShapeRendered[CssDataShape.CssShapeOuterPos], FillRule.Nonzero, false));
            }
        }
        static CombinedGeometry CreateGeometryFromPolygons(PointCollection polygon, PointCollection polygon2nd, GeometryCombineMode mode = GeometryCombineMode.Exclude)
        {
            CombinedGeometry combinedGeometry = new CombinedGeometry(VisualObjectData.CreateGeometryFromPolygon(polygon), VisualObjectData.CreateGeometryFromPolygon(polygon2nd));
            combinedGeometry.GeometryCombineMode = mode;
            return combinedGeometry;
        }
    #endregion

        #region DEPENDENCY PROPERTY
        static SectionDrawer()
        {
            CssShape4DrawProperty = DependencyProperty.Register(CssShape4DrawPropertyName, typeof(CssDataShape), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnShapeChanged)));
            CssAxisHorizontalDrawProperty = DependencyProperty.Register(CssAxisHorizontal4DrawPropertyName, typeof(CssDataAxis), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisHorizontalChanged)));
            CssAxisVerticalDrawProperty = DependencyProperty.Register(CssAxisVertical4DrawPropertyName, typeof(CssDataAxis), typeof(SectionDrawer),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnAxisVerticalChanged)));
            //
            ResourceDictionary resourceDictionary = new ResourceDictionary();
            resourceDictionary.Source = new Uri(
              "ResourceLibrary;component/Themes/generic.xaml", UriKind.Relative);
            compressionPartBrush = (Brush)resourceDictionary["CompressionPartBrush"];
        }

        private static void OnAxisVerticalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SectionDrawer drawignControl = (SectionDrawer)sender;
            drawignControl.CssAxisHorizontal4Draw = (CssDataAxis)e.NewValue;
            drawignControl.OnChangedInternal(e, eUsedVisuals.eCssAxisVerticalVisual, false);
            RenderAll(drawignControl);
        }
        private static void OnAxisHorizontalChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SectionDrawer drawignControl = (SectionDrawer)sender;
            drawignControl.CssAxisHorizontal4Draw = (CssDataAxis)e.NewValue;
            drawignControl.OnChangedInternal(e, eUsedVisuals.eCssAxisHorizontalVisual, false);
            RenderAll(drawignControl);
        }
        private static void OnShapeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SectionDrawer drawignControl = (SectionDrawer)sender;
            drawignControl.CssShape4Draw = (CssDataShape)e.NewValue;
            drawignControl.OnChangedInternal(e, eUsedVisuals.eCssShapeVisual);
            RenderAll(drawignControl);
        }

        private void OnChangedInternal(DependencyPropertyChangedEventArgs e, eUsedVisuals visualType, bool setShapes = true)
        {
            VisualObjectData visual = drawingSurface.GetVisual((int)visualType);
            CssDataBase newShape = (CssDataBase)e.NewValue;
            if (setShapes)
            {
                visual.SetShapesAndCreateGeometry(newShape.ShapeObjetcs);
            }
            visual.CallBack4ShapeChanged();
        }

        private void CalculateAxisHorizontalFromCssShape(VisualObjectData visualCssShape)
        {
            VisualObjectData visualAxis = drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisHorizontalVisual);
            if (visualCssShape.ShapeGeometry == null || visualCssShape.ShapeGeometry.Count == 0)
            {
                return;
            }
            Rect bounds = visualCssShape.ShapeGeometry[CssDataShape.CssShapeOuterPos].Bounds;
            List<PointCollection> shapes = new List<PointCollection>();
            PointCollection axisHorizontal = new PointCollection();
            double overLap = bounds.Height * _axisOverLap;
            double extremeLeft = GeometryOperations.Add4Sign(bounds.Left, overLap);
            axisHorizontal.Add(new Point(extremeLeft, 0.0));
            double exteremeRight = GeometryOperations.Add4Sign(bounds.Right, overLap);
            axisHorizontal.Add(new Point(exteremeRight, 0.0));
            double arrowOverLap = bounds.Height * _axisArrowSize;
            // arrow
            axisHorizontal.Add(new Point(exteremeRight, arrowOverLap));
            axisHorizontal.Add(new Point(exteremeRight + 2 * arrowOverLap, 0.0));
            axisHorizontal.Add(new Point(exteremeRight, -arrowOverLap));
            axisHorizontal.Add(new Point(exteremeRight, 0.0));
            shapes.Add(axisHorizontal);
            visualAxis.SetShapesAndCreateGeometry(shapes, FillRule.EvenOdd, false);
        }

        private void CalculateAxisVerticalFromCssShape(VisualObjectData visualCssShape)
        {
            VisualObjectData visualAxis = drawingSurface.GetVisual((int)eUsedVisuals.eCssAxisVerticalVisual);
            if (visualCssShape.ShapeGeometry == null || visualCssShape.ShapeGeometry.Count == 0)
            {
                return;
            }
            Rect bounds = visualCssShape.ShapeGeometry[CssDataShape.CssShapeOuterPos].Bounds;
            List<PointCollection> shapes = new List<PointCollection>();
            PointCollection axisVertical = new PointCollection();
            double overLap = bounds.Height * _axisOverLap;
            double extremeBotton = GeometryOperations.Add4Sign(bounds.Bottom, overLap);
            axisVertical.Add(new Point(0.0, extremeBotton));
            double exteremeTop = GeometryOperations.Add4Sign(bounds.Top, overLap);
            axisVertical.Add(new Point(0.0, exteremeTop));
            double arrowOverLap = bounds.Height * _axisArrowSize;
            // arrow
            axisVertical.Add(new Point(arrowOverLap, exteremeTop));
            axisVertical.Add(new Point(0.0, exteremeTop - 2 * arrowOverLap));
            axisVertical.Add(new Point(-arrowOverLap, exteremeTop));
            axisVertical.Add(new Point(0.0, exteremeTop));
            shapes.Add(axisVertical);
            visualAxis.SetShapesAndCreateGeometry(shapes, FillRule.EvenOdd, false);
        }

        private static void RenderAll(SectionDrawer drawignControl)
        {
            Rect bounds = drawignControl.drawingSurface.RecalculateBounds();
            // recalculate scale
            Matrix conventer = GetTransformMatrix(ref bounds, drawignControl.drawingSurface);
            // transform all
            drawignControl.drawingSurface.TransformAll(conventer);
            // render all
            drawignControl.drawingSurface.DrawAll();
        }
        private static Matrix GetTransformMatrix(ref Rect bounds, DrawingCanvas drawingSurface)
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
            return conventer;
        }
        //
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
        //
        #endregion



        // Variables for dragging shapes.
        private bool isDragging = false;
        private Vector clickOffset;
        private DrawingVisual selectedVisual;

        // Variables for drawing the selection square.
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;

        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);

            if (cmdAdd.IsChecked == true)
            {
                VisualObjectData visual = new VisualObjectData();
                DrawSquare(visual.VisualObject, pointClicked, false);
                drawingSurface.AddVisual(visual);
            }
            else if (cmdDelete.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null) drawingSurface.DeleteVisual(visual);
            }
            else if (cmdSelectMove.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if (visual != null)
                {
                    // Calculate the top-left corner of the square.
                    // This is done by looking at the current bounds and
                    // removing half the border (pen thickness).
                    // An alternate solution would be to store the top-left
                    // point of every visual in a collection in the 
                    // DrawingCanvas, and provide this point when hit testing.
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }
            else if (cmdSelectMultiple.IsChecked == true)
            {

                selectionSquare = new DrawingVisual();

                drawingSurface.AddVisual(selectionSquare);

                selectionSquareTopLeft = pointClicked;
                isMultiSelecting = true;

                // Make sure we get the MouseLeftButtonUp event even if the user
                // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
                drawingSurface.CaptureMouse();
            }
        }

        // Drawing constants.
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private DrawingVisual selectionSquare;

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

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            if (isMultiSelecting)
            {
                // Display all the squares in this region.
                RectangleGeometry geometry = new RectangleGeometry(
                    new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
                List<DrawingVisual> visualsInRegion =
                    drawingSurface.GetVisuals(geometry);
                MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));

                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                        selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawSquare(selectedVisual, pointDragged, true);
            }
            else if (isMultiSelecting)
            {
                Point pointDragged = e.GetPosition(drawingSurface);
                DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

        private void DrawSelectionSquare(Point point1, Point point2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;

            using (DrawingContext dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen,
                    new Rect(point1, point2));
            }
        }
    }
}
