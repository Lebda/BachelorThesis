using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SectionCheckInterfaces.Interfaces;
using System.Windows.Media;
using System.Windows;
using CommonLibrary.Utility;
using System.Windows.Shapes;

namespace SectionDrawUI.Services
{
    public class SectionShapeService : ISectionShapeService
    {
        public ISectionShape ShapeModel { get; set; }
        public CanvasDataContext CanvasData { get; set;}
        Matrix _conventer;
        public SectionShapeService()
        {
            _conventer = new Matrix();
        }

        public void Prepare()
        {
            _conventer = new Matrix();
            PathGeometry boundGeo = CreateGeometryFromPolygon(ShapeModel.CssShapeOuter);
            //GeometryGroup boundGeo = CreateGeometryFromPolygon(ShapeModel.TestShape);
            Rect bound = boundGeo.Bounds;
            _conventer.Translate(-bound.TopLeft.X, -bound.TopLeft.Y);
            double widthInPixel = CanvasData.CanvasWidthProperty;
            double heightInPixels = CanvasData.CanvasHeightProperty;
            double scaleX = widthInPixel / bound.Width;
            double scaleY = heightInPixels / bound.Height;
            double scale = (scaleX > scaleY) ? (scaleY) : (scaleX);
            scale *= 0.8;
            _conventer.ScaleAt(scale, scale, 0.0, 0.0);
            ShapeModel.TansformAll(_conventer);
        }

        public PathGeometry GetTestShape()
        {
            return CreateGeometryFromPolygon(ShapeModel.TestShape);
        }

        public PathGeometry GetOuterSectionShape()
        {
            return CreateGeometryFromPolygon(ShapeModel.CssShapeOuter);
        }

        public PathGeometry GetInnerSectionShape()
        {
            return CreateGeometryFromPolygon(ShapeModel.CssShapeInner);
        }

        private PathGeometry CreateGeometryFromPolygon(PointCollection polygon)
        {
            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(0.0, 0.0);
            //
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            int countStop = polygon.Count;
            for (int counter = 0; counter < countStop; ++counter)
            {
                LineSegment myLineSegment = new LineSegment();
                myLineSegment.Point = new Point(polygon[counter].X, polygon[counter].Y);
                myPathSegmentCollection.Add(myLineSegment);
            }
            myPathFigure.Segments = myPathSegmentCollection;
            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;
            return myPathGeometry;
        }

        public CombinedGeometry GetWholeSectionShape()
        {
            CombinedGeometry combinedGeometry = new CombinedGeometry(GetOuterSectionShape(), GetInnerSectionShape());
            combinedGeometry.GeometryCombineMode = GeometryCombineMode.Exclude;
            return combinedGeometry;
        }

        public GeometryGroup GetReinforcementShape()
        {
            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(50, 50);
            myEllipseGeometry.RadiusX = 10;
            myEllipseGeometry.RadiusY = 10;

            Rect testBound = myEllipseGeometry.Bounds;

            EllipseGeometry myEllipseGeometry2 = new EllipseGeometry();
            myEllipseGeometry.Center = new Point(200, 200);
            myEllipseGeometry.RadiusX = 10;
            myEllipseGeometry.RadiusY = 10;

            GeometryGroup geometryGroup = new GeometryGroup();
            geometryGroup.Children.Add(myEllipseGeometry);
            geometryGroup.Children.Add(myEllipseGeometry2);

            return geometryGroup;
        }
    }
}
