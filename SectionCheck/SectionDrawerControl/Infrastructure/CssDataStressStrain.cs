using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;
using XEP_CommonLibrary.Factories;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckInterfaces.SectionDrawer;

namespace XEP_SectionDrawer.Infrastructure
{
    [Serializable]
    public class CssDataFibers : ObservableObject, XEP_ICssDataFibers
    {
        public static readonly double s_moveStressStrain = 1.25;
        public static readonly double s_maxCssWidthStressStrain = 0.5;

        public CssDataFibers(IGeometryMaker geometryMaker)
        {
            _geometryMaker = Exceptions.CheckNull(geometryMaker);
        }

        #region IVisualObejctDrawingData Members
        private Pen _visualPen = Application.Current.TryFindResource(CustomResources.ConcreteStrainPen1_SCkey) as Pen;
        public static readonly string VisualPenPropertyName = "VisualPen";
        public Pen VisualPen
        {
            get { return _visualPen; }
            set { SetMember<Pen>(ref value, ref _visualPen, _visualPen == value, VisualPenPropertyName); }
        }
        private Brush _visualBrush = Application.Current.TryFindResource(CustomResources.ConcreteStrainBrush1_SCkey) as Brush;
        public static readonly string VisualBrushPropertyName = "VisualBrush";
        public Brush VisualBrush
        {
            get { return _visualBrush; }
            set { SetMember<Brush>(ref value, ref _visualBrush, _visualBrush == value, VisualPenPropertyName); }
        }
        #endregion

        #region IVisualObejctDrawingData Members
        public Pen GetPen()
        {
            return _visualPen;
        }
        public Brush GetBrush()
        {
            return _visualBrush;
        }
        #endregion

        #region IPathGeometryCreator Members
        public PathGeometry Create()
        {
            return null;
        }
        #endregion

        #region XEP_ICssDataReinforcement Members
        IGeometryMaker _geometryMaker = null;
        public IGeometryMaker GeometryMaker
        {
            get { return _geometryMaker; }
            set { _geometryMaker = value; }
        }
        List<ICssDataFiber> _fibers = Exceptions.CheckNull(new List<ICssDataFiber>());
        public static readonly string FibersPropertyName = "Fibers";
        public List<ICssDataFiber> Fibers
        {
            get { return _fibers; }
            set { SetMember<List<ICssDataFiber>>(ref value, ref _fibers, _fibers == value, FibersPropertyName); }
        }
        ILine2D _neuAxis = Line2DFactory.Instance().Create();
        public static readonly string NeuAxisPropertyName = "NeuAxis";
        public ILine2D NeuAxis
        {
            get { return _neuAxis; }
            set { SetMember<ILine2D>(ref value, ref _neuAxis, _neuAxis == value, NeuAxisPropertyName); }
        }
        public PathGeometry GetStrainGeometry(double CssWidth, bool move = true, IStrainStressShape dataDependentObject = null)
        {
            return DoWorkGeometryMaker(move, s_maxCssWidthStressStrain * CssWidth, s_moveStressStrain * CssWidth, true, dataDependentObject);
        }
        public PathGeometry GetStressGeometry(double CssWidth, bool move = true, IStrainStressShape baseLineFromDependentObject = null)
        {
            return DoWorkGeometryMaker(move, s_maxCssWidthStressStrain * CssWidth, 1.75 * s_moveStressStrain * CssWidth, false, baseLineFromDependentObject);
        }
        private PathGeometry DoWorkGeometryMaker(bool move, double maxWidth, double moveSize, bool isStrain, IStrainStressShape dataDependentObject = null)
        {
            Exceptions.CheckNull(_geometryMaker);
            _geometryMaker.IsMove = move;
            _geometryMaker.MaxWidth = maxWidth;
            _geometryMaker.MoveSize = moveSize;
            _geometryMaker.IsStrain = isStrain;
            Exceptions.CheckNull(_geometryMaker.ShapeMaker);
            _geometryMaker.ShapeMaker.NeuAxis = _neuAxis;
            return _geometryMaker.CreateGeometry(_fibers, dataDependentObject);
        }
        #endregion
    }
}
