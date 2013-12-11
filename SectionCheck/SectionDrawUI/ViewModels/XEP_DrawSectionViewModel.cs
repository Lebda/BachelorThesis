using System;
using System.Linq;
using XEP_CommonLibrary.Infrastructure;
using System.Windows;
using System.Windows.Media;
using XEP_CommonLibrary.Utility;
using XEP_SectionDrawer.Infrastructure;
using ResourceLibrary;
using XEP_CommonLibrary.Factories;
using System.Windows.Input;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.DrawingGraph;
using XEP_SectionDrawer.Interfaces;

namespace XEP_SectionDrawUI.ViewModels
{
    public class XEP_DrawSectionViewModel : ObservableObject
    {
        ICssDataService _cssDataService = null;
        public ICssDataService CssDataService
        {
            get { return _cssDataService; }
            set { _cssDataService = value; }
        }
        public XEP_DrawSectionViewModel(ICssDataService cssDataService)
        {
            _cssDataService = cssDataService;


            _fibersConcreteProperty = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete),
            Application.Current.TryFindResource(CustomResources.ConcreteStrainBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.ConcreteStrainPen1_SCkey) as Pen);
            _fibersReinforcementProperty = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eReinforcement),
             Application.Current.TryFindResource(CustomResources.ReinfStrainBrush1_SCkey) as Brush,
             Application.Current.TryFindResource(CustomResources.ReinfStrainPen1_SCkey) as Pen);
        }

        #region METHODS
        void LoadCssData()
        {
            Exceptions.CheckNull(_cssDataService);
            CssShape = _cssDataService.GetCssDataShape();
            CssAxisVertical = _cssDataService.GetCssDataAxisVertical();
            CssAxisHorizontal = _cssDataService.GetCssDataAxisHorizontal();
            CssCompressPart = _cssDataService.GetCssDataCompressPart();
            CssReinforcement = _cssDataService.GetCssDataReinforcement();
            FibersConcrete = _cssDataService.GetCssDataFibersConcrete();
            FibersReinforcement = _cssDataService.GetCssDataFibersReinforcement();
        }
        #endregion // METHODS

        #region COMMANDS
        public ICommand TestComand
        {
            get { return new RelayCommand(TestExecute, CanTestExecute); }
        }
        Boolean CanTestExecute()
        {
            return true;
        }
        long counter1 = 0;
        void TestExecute()
        {
            if (counter1 % 2 == 0)
            {
                this.FibersConcrete = Test1(1e-4);
                this.FibersReinforcement = Test3(this.FibersReinforcement, 1e-4);
            }
            else
            {
                this.FibersConcrete = Test2(1e-4);
                this.FibersReinforcement = Test3(this.FibersReinforcement, 1e-4);
            }
            counter1++;

        }
        #endregion

        public const string FibersConcretePropertyName = "FibersConcrete";
        private CssDataFibers _fibersConcreteProperty = null;
        public CssDataFibers FibersConcrete
        {
            get
            {
                return _fibersConcreteProperty;
            }
            set
            {
                if (_fibersConcreteProperty == value)
                {
                    return;
                }
                _fibersConcreteProperty = value;
                RaisePropertyChanged(FibersConcretePropertyName);
            }
        }
        public CssDataFibers Test2(double test)
        {
            CssDataFibers data = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete));
            ICssDataFiber fiber = CssFiberFactory.Instance().Create(0, new Point(0.15, -0.25), -0.30285, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(2600000, 57.0204 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(0.15, 0.25), 0.07949, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(-2600000, -14.919 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(-0.15, 0.25), -0.11384, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(2600000, 21.37 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(-0.15, -0.25), -0.49617, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(2600000, 92.81 * test));
            data.Fibers.Add(fiber);
            data.Fibers.Add(data.Fibers.First());
            data.NeuAxis = Line2DFactory.Instance().Create(new Point(0.0266539, 0.250), new Point(0.150, 0.1460493));
            return data;
        }
        public CssDataFibers Test1(double test)
        {
            CssDataFibers data = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete));
            ICssDataFiber fiber = CssFiberFactory.Instance().Create(0, new Point(0.15, -0.25), -0.25, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(-2600000, -20 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(0.15, 0.25), 0.25, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(2600000, 20 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(-0.15, 0.25), 0.25, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(2600000, 20 * test));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(-0.15, -0.25), -0.25, eCssComponentType.eConcrete);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(-2600000, -20 * test));
            data.Fibers.Add(fiber);
            data.Fibers.Add(data.Fibers.First());
            data.NeuAxis = Line2DFactory.Instance().Create(new Point(-0.15, 0.0), new Point(0.150, 0.0));
            return data;
        }

        public const string FibersReinforcementPropertyName = "FibersReinforcement";
        private CssDataFibers _fibersReinforcementProperty = null;
        public CssDataFibers FibersReinforcement
        {
            get
            {
                return _fibersReinforcementProperty;
            }
            set
            {
                if (_fibersReinforcementProperty == value)
                {
                    return;
                }
                _fibersReinforcementProperty = value;
                RaisePropertyChanged(FibersReinforcementPropertyName);
            }
        }
        private CssDataFibers Test3(CssDataFibers fibers, double test)
        {
            CssDataFibers data = new CssDataFibers(fibers.GeometryMaker, fibers.VisualBrush, fibers.VisualPen);
            data.NeuAxis = fibers.NeuAxis;
            ICssDataFiber fiber = CssFiberFactory.Instance().Create(0, new Point(-0.1, -0.2), -0.4257188, eCssComponentType.eReinforcement);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(302600000, 79.5265 * test));
            fiber.SetFiberData(BarData.s_name, new BarData(0.000314, 0.02));
            data.Fibers.Add(fiber);
            fiber = CssFiberFactory.Instance().Create(0, new Point(0.1, 0.2), 0.0090329, eCssComponentType.eReinforcement);
            fiber.SetFiberData(SSInFiber.s_name, new SSInFiber(-302600000, -1.7337 * test));
            fiber.SetFiberData(BarData.s_name, new BarData(0.000314, 0.02));
            data.Fibers.Add(fiber);
            data.NeuAxis = Line2DFactory.Instance().Create(new Point(0.0266539, 0.250), new Point(0.150, 0.1460493));
            return data;
        }

        public const string CssReinforcementPropertyName = "CssReinforcement";
        private CssDataReinforcement _cssReinforcementProperty = new CssDataReinforcement(
            Application.Current.TryFindResource(CustomResources.ReinfBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.ReinfPen1_SCkey) as Pen);
        public CssDataReinforcement CssReinforcement
        {
            get
            {
                double barDiameter = 0.02;
                _cssReinforcementProperty.BarData.Add(new CssDataOneReinf(-0.1, -0.2, barDiameter));
                _cssReinforcementProperty.BarData.Add(new CssDataOneReinf(-0.1, 0.2, barDiameter));
                _cssReinforcementProperty.BarData.Add(new CssDataOneReinf(0.1, -0.2, barDiameter));
                _cssReinforcementProperty.BarData.Add(new CssDataOneReinf(0.1, 0.2, barDiameter));
                return _cssReinforcementProperty;
            }
            set
            {
                if (_cssReinforcementProperty == value)
                {
                    return;
                }
                _cssReinforcementProperty = value;
                RaisePropertyChanged(CssReinforcementPropertyName);
            }
        }

        public const string CssCompressPartPropertyName = "CssCompressPart";
        private CssDataCompressPart _cssCompressPartProperty = new CssDataCompressPart(
            Application.Current.TryFindResource(CustomResources.CompressPartBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.CompressPartPen1_SCkey) as Pen);
        public CssDataCompressPart CssCompressPart
        {
            get
            {
                //                 _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.15));
                //                 _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.25));
                //                 _cssCompressPartProperty.CssCompressPart.Add(new Point(-0.15, 0.25));
                //                 _cssCompressPartProperty.CssCompressPart.Add(new Point(-0.15, 0.15));
                //                 _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.15));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.1460493));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.25));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.026653, 0.25));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.1460493));
                return _cssCompressPartProperty;
            }

            set
            {
                if (_cssCompressPartProperty == value)
                {
                    return;
                }
                _cssCompressPartProperty = value;
                RaisePropertyChanged(CssCompressPartPropertyName);
            }
        }

        public const string CssShapePropertyName = "CssShape";
        private CssDataShape _cssShapeProperty = new CssDataShape(
            Application.Current.TryFindResource(CustomResources.CssBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.CssPen1_SCkey) as Pen);
        public CssDataShape CssShape
        {
            get
            {
                _cssShapeProperty.CssShapeOuter.Add(new Point(0.15, -0.25));
                _cssShapeProperty.CssShapeOuter.Add(new Point(0.15, 0.25));
                _cssShapeProperty.CssShapeOuter.Add(new Point(-0.15, 0.25));
                _cssShapeProperty.CssShapeOuter.Add(new Point(-0.15, -0.25));
                _cssShapeProperty.CssShapeOuter.Add(new Point(0.15, -0.25));
                //
                _cssShapeProperty.CssShapeInner.Add(new Point(0.05, -0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(-0.05, -0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(-0.05, 0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(0.05, 0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(0.05, -0.05));
                return _cssShapeProperty;
            }
            set
            {
                if (_cssShapeProperty == value)
                {
                    return;
                }
                _cssShapeProperty = value;
                RaisePropertyChanged(CssShapePropertyName);
            }
        }

        public const string CssAxisHorizontalPropertyName = "CssAxisHorizontal";
        private CssDataAxis _cssAxisHorizontalProperty = new CssDataAxis(
            Application.Current.TryFindResource(CustomResources.HorAxisBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.HorAxisPen1_SCkey) as Pen);
        public CssDataAxis CssAxisHorizontal
        {
            get
            {
                return _cssAxisHorizontalProperty;
            }
            set
            {
                if (_cssAxisHorizontalProperty == value)
                {
                    return;
                }
                _cssAxisHorizontalProperty = value;
                RaisePropertyChanged(CssAxisHorizontalPropertyName);
            }
        }

        public const string CssAxisVerticalPropertyName = "CssAxisVertical";
        private CssDataAxis _cssAxisVerticalProperty = new CssDataAxis(
            Application.Current.TryFindResource(CustomResources.VerAxisBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.VerAxisPen1_SCkey) as Pen);
        public CssDataAxis CssAxisVertical
        {
            get
            {
                return _cssAxisVerticalProperty;
            }

            set
            {
                if (_cssAxisVerticalProperty == value)
                {
                    return;
                }
                _cssAxisVerticalProperty = value;
                RaisePropertyChanged(CssAxisVerticalPropertyName);
            }
        }
    }
}

