using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using XEP_SectionCheckCommon.Interfaces;

namespace XEP_SectionDrawUI.Models
{
    public class XEP_SectionShapeModel : XEP_ISectionShape
    {
        public PointCollection CssShapeOuter { get; set; }
        public PointCollection CssShapeInner { get; set; }
        public PointCollection ReinforcementShape { get; set; }

        List<PointCollection> _allShapes = new List<PointCollection>();
        //
        public PointCollection TestShape { get; set; }
        public void TansformAll(Matrix conventer)
        {
            foreach (PointCollection iter in _allShapes)
            {
                for (int counter = 0; counter < iter.Count; ++counter)
                {
                    iter[counter] = conventer.Transform(iter[counter]);
                }
            }
        }
        public void Prepare()
        {
            PrepareMock();
            _allShapes.Add(CssShapeOuter);
            _allShapes.Add(CssShapeInner);
            _allShapes.Add(ReinforcementShape);
            _allShapes.Add(TestShape);
        }
        protected void PrepareMock()
        {
            CssShapeOuter = new PointCollection();
            CssShapeInner = new PointCollection();
            ReinforcementShape = new PointCollection();
            TestShape = new PointCollection();
            //
            CssShapeOuter.Add(new Point(0.15, -0.25));
            CssShapeOuter.Add(new Point(0.15, 0.25));
            CssShapeOuter.Add(new Point(-0.15, 0.25));
            CssShapeOuter.Add(new Point(-0.15, -0.25));
            CssShapeOuter.Add(new Point(0.15, -0.25));
            //
            CssShapeInner.Add(new Point(0.05, -0.05));
            CssShapeInner.Add(new Point(0.05, 0.05));
            CssShapeInner.Add(new Point(-0.05, 0.05));
            CssShapeInner.Add(new Point(-0.05, -0.05));
            CssShapeInner.Add(new Point(0.05, -0.05));
            //
            ReinforcementShape.Add(new Point(0.1, -0.2));
            ReinforcementShape.Add(new Point(0.1, 0.2));
            ReinforcementShape.Add(new Point(-0.1, 0.2));
            ReinforcementShape.Add(new Point(-0.1, -0.2));
            ReinforcementShape.Add(new Point(0.1, -0.2));
            //
            TestShape.Add(new Point(-2858.5507, -1262.0612));
            TestShape.Add(new Point(-3800.7709, -240.825));
            TestShape.Add(new Point(-2579.0786, 876.1521));
            TestShape.Add(new Point(-942.1707, 46.3976));
            TestShape.Add(new Point(-1637.5034, -1325.3281));
            TestShape.Add(new Point(-2858.5507, -1262.0612));
        }
    }
}
