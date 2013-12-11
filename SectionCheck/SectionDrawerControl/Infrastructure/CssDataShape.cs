using System;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using ResourceLibrary;
using XEP_CommonLibrary.Geometry;

namespace XEP_SectionDrawer.Infrastructure
{
    public class CssDataShape : CssDataBase
    {
         public CssDataShape()
            : base(Application.Current.TryFindResource(CustomResources.CssBrush1_SCkey) as Brush,
                    Application.Current.TryFindResource(CustomResources.CssPen1_SCkey) as Pen)
        {
        }
         public CssDataShape(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }

        public override PathGeometry Create()
        {
            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeOuter));
            myPathGeometry.Figures.Add(GeometryOperations.Create(_cssShapeInner));
            myPathGeometry.FillRule = FillRule.Nonzero;
            return myPathGeometry;
        }

        /// <summary>
        /// The <see cref="CssShapeOuter" /> property's name.
        /// </summary>
        public const string CssShapeOuterPropertyName = "CssShapeOuter";
        public static readonly int CssShapeOuterPos = 0;
        PointCollection _cssShapeOuter = new PointCollection();
        /// <summary>
        /// Sets and gets the CssShapeOuter property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssShapeOuter
        {
            get
            {
                return _cssShapeOuter;
            }

            set
            {
                if (_cssShapeOuter == value)
                {
                    return;
                }
                _cssShapeOuter = value;
                RaisePropertyChanged(CssShapeOuterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CssShapeInner" /> property's name.
        /// </summary>
        public const string CssShapeInnerPropertyName = "CssShapeInner";
        public static readonly int CssShapeInnerPos = 1;
        PointCollection _cssShapeInner = new PointCollection();
        /// <summary>
        /// Sets and gets the CssShapeInner property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PointCollection CssShapeInner
        {
            get
            {
                return _cssShapeInner;
            }

            set
            {
                if (_cssShapeInner == value)
                {
                    return;
                }
                _cssShapeInner = value;
                RaisePropertyChanged(CssShapeInnerPropertyName);
            }
        }
    }
}
