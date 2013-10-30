using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SectionDrawerControl.Infrastructure;
using SectionDrawerControl.Utility;

namespace SectionDrawerControl
{
    public class DrawingCanvas : Canvas
    {
        public DrawingCanvas()
        {
            _visuals = new List<VisualObjectData>();
        }
        private List<VisualObjectData> _visuals = null;
        private PathGeometry _wholeGeometry = new PathGeometry();
        private Matrix _conventer = new Matrix();

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _visuals.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return _visuals[index].VisualObject;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }
        public void DrawAll()
        {
            foreach (VisualObjectData iter in _visuals)
            {
                iter.Draw();
            }
        }
        public void TransformAll(Matrix conventer)
        {
            if (_conventer.Equals(conventer))
            {
                return;
            }
            foreach (VisualObjectData iter in _visuals)
            {
                iter.Transform(conventer);
            }
        }
        public Rect RecalculateBounds()
        {
            _wholeGeometry.Clear();
            foreach (VisualObjectData iter in _visuals)
            {
                foreach(Geometry iterGeometry in iter.ShapeGeometry)
                {
                    _wholeGeometry.AddGeometry(iterGeometry);
                }
            }
            return _wholeGeometry.Bounds;
        }
        public Rect GetBounds()
        {
            return _wholeGeometry.Bounds;
        }

        public List<VisualObjectData> GetVisualObjects()
        {
            return _visuals;
        }

        public void AddVisual(VisualObjectData visual)
        {
            _visuals.Add(visual);
            _conventer = new Matrix();

            base.AddVisualChild(visual.VisualObject);
            base.AddLogicalChild(visual.VisualObject);
        }
        public void AddVisual(Visual visual)
        {
            _visuals.Add(new VisualObjectData(visual));

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(VisualObjectData visual)
        {
            _visuals.Remove(visual);
            base.RemoveVisualChild(visual.VisualObject);
            base.RemoveLogicalChild(visual.VisualObject);
        }
        public void DeleteVisual(Visual visual)
        {
            foreach (VisualObjectData iter in _visuals)
            {
                if (iter.VisualObject == visual)
                {
                    _visuals.Remove(iter);
                    break;
                }
            }
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }

        public VisualObjectData GetVisual(int index)
        {
            return _visuals[index];
        }

        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        private List<DrawingVisual> hits = new List<DrawingVisual>();
        public List<DrawingVisual> GetVisuals(Geometry region)
        {
            hits.Clear();
            GeometryHitTestParameters parameters = new GeometryHitTestParameters(region);
            HitTestResultCallback callback = new HitTestResultCallback(this.HitTestCallback);
            VisualTreeHelper.HitTest(this, null, callback, parameters);
            return hits;
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if (visual != null &&
                geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }

    }
}
