using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using XEP_SectionDrawer.Utility;
using XEP_CommonLibrary.Utility;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_SectionDrawer
{
    public class XEP_DrawingCanvas : Canvas
    {
        public XEP_DrawingCanvas()
        {
            _visuals = Exceptions.CheckNull(new List<VisualObjectData>());
        }
        private List<VisualObjectData> _visuals = null;
        private PathGeometry _wholeGeometry = Exceptions.CheckNull(new PathGeometry());
        private Matrix _conventer = Exceptions.CheckNull(new Matrix());

        protected override Visual GetVisualChild(int index)
        {
            Exceptions.CheckNull(_visuals);
            Exceptions.CheckPredicate<int>("Invalid index of visual child !", index, (start => start < 0));
            Exceptions.CheckPredicate<int, int>("Invalid index of visual child !", index, _visuals.Count, (start, itemCount) => itemCount <= start);
            return _visuals[index].VisualObject;
        }

        protected override int VisualChildrenCount
        {
            get
            {
                Exceptions.CheckNull(_visuals);
                return _visuals.Count;
            }
        }
        public void DrawAll()
        {
            foreach (VisualObjectData iter in Exceptions.CheckNull<List<VisualObjectData>>(_visuals))
            {
                Exceptions.CheckNull<VisualObjectData>(iter).Draw();
            }
        }
        public void TransformAll(MatrixTransform conventer)
        {
            Exceptions.CheckNullArgument(null, conventer, _conventer);
            if (_conventer.Equals(conventer))
            {
                return;
            }
            foreach (VisualObjectData iter in Exceptions.CheckNull<List<VisualObjectData>>(_visuals))
            {
                Exceptions.CheckNull<IVisualShapes>(Exceptions.CheckNull<VisualObjectData>(iter).VisualShape).UpdateRenderedGeometry(conventer);
            }
        }
        public Rect RecalculateBounds()
        {
            Exceptions.CheckNullArgument(null, _wholeGeometry, _visuals);
            _wholeGeometry.Clear();
            foreach (VisualObjectData iter in _visuals)
            {
                _wholeGeometry.AddGeometry(
                    Exceptions.CheckNull<PathGeometry>(Exceptions.CheckNull<IVisualShapes>(Exceptions.CheckNull<VisualObjectData>(iter).VisualShape).BaseGeo));
            }
            return _wholeGeometry.Bounds;
        }
        public Rect GetBounds()
        {
            return Exceptions.CheckNull<PathGeometry>(_wholeGeometry).Bounds;
        }

        public List<VisualObjectData> GetVisualObjects()
        {
            return _visuals;
        }

        public void AddVisual(VisualObjectData visual)
        {
            Exceptions.CheckNull(_visuals, _conventer);
            _visuals.Add(visual);
            _conventer = Exceptions.CheckNull<Matrix>(new Matrix());

            base.AddVisualChild(visual.VisualObject);
            base.AddLogicalChild(visual.VisualObject);
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
