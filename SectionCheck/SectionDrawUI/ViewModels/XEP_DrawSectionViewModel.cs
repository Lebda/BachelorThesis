using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ResourceLibrary;
using XEP_CommonLibrary.DrawingGraph;
using XEP_CommonLibrary.Factories;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Interfaces;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_SectionDrawUI.ViewModels
{
    public class XEP_DrawSectionViewModel : ObservableObject
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolverItem = null;
        readonly XEP_IResolver<XEP_ICssDataShape> _resolverICssDataShape = null;

        public XEP_DrawSectionViewModel(XEP_IResolver<XEP_ISectionShapeItem> resolverItem, XEP_IResolver<XEP_ICssDataShape> resolverICssDataShape)
        {
            _resolverICssDataShape = resolverICssDataShape;
            _cssShapeProperty = _resolverICssDataShape.Resolve();
            _cssReinforcementProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.ReinfBrush1_SCkey);
            _cssReinforcementProperty.VisualPen = CustomResources.GetSavePen(CustomResources.ReinfPen1_SCkey);
            _fibersConcreteProperty = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete));
            _fibersConcreteProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.ConcreteStrainBrush1_SCkey);
            _fibersConcreteProperty.VisualPen = CustomResources.GetSavePen(CustomResources.ConcreteStrainPen1_SCkey);
            _fibersReinforcementProperty = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eReinforcement));
            _fibersReinforcementProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.ReinfStrainBrush1_SCkey);
            _fibersReinforcementProperty.VisualPen = CustomResources.GetSavePen(CustomResources.ReinfStrainPen1_SCkey);
            _cssCompressPartProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.CompressPartBrush1_SCkey);
            _cssCompressPartProperty.VisualPen = CustomResources.GetSavePen(CustomResources.CompressPartPen1_SCkey);
            _cssShapeProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.CssBrush1_SCkey);
            _cssShapeProperty.VisualPen = CustomResources.GetSavePen(CustomResources.CssPen1_SCkey);
            _cssAxisHorizontalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.HorAxisBrush1_SCkey);
            _cssAxisHorizontalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.HorAxisPen1_SCkey);
            _cssAxisVerticalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.VerAxisBrush1_SCkey);
            _cssAxisVerticalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.VerAxisPen1_SCkey);
            _resolverItem = resolverItem;
        }

        #region METHODS
        
        #endregion // METHODS
        
        #region COMMANDS
        
        public ICommand TestComand
        {
            get
            {
                return new RelayCommand(TestExecute, CanTestExecute);
            }
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
        private XEP_ICssDataFibers _fibersConcreteProperty = null;
        
        public XEP_ICssDataFibers FibersConcrete
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
        
        public XEP_ICssDataFibers Test2(double test)
        {
            XEP_ICssDataFibers data = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete));
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
        
        public XEP_ICssDataFibers Test1(double test)
        {
            XEP_ICssDataFibers data = new CssDataFibers(GeometryMakerFactory.Instance().Create(eCssComponentType.eConcrete));
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
        private XEP_ICssDataFibers _fibersReinforcementProperty = null;
        
        public XEP_ICssDataFibers FibersReinforcement
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
        
        private XEP_ICssDataFibers Test3(XEP_ICssDataFibers fibers, double test)
        {
            XEP_ICssDataFibers data = new CssDataFibers(fibers.GeometryMaker);
            data.VisualBrush = fibers.VisualBrush;
            data.VisualPen = fibers.VisualPen;
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
        private XEP_ICssDataReinforcement _cssReinforcementProperty = new CssDataReinforcement();
        
        public XEP_ICssDataReinforcement CssReinforcement
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
        private XEP_ICssDataCompressPart _cssCompressPartProperty = new CssDataCompressPart();
        
        public XEP_ICssDataCompressPart CssCompressPart
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
        private XEP_ICssDataShape _cssShapeProperty = null;
        public XEP_ICssDataShape CssShape
        {
            get
            {
                ObservableCollection<XEP_ISectionShapeItem> data = new ObservableCollection<XEP_ISectionShapeItem>();
                XEP_ISectionShapeItem item = _resolverItem.Resolve();
                item.Y.Value = 0.15;
                item.Z.Value = -0.25;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = 0.15;
                item.Z.Value = 0.25;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = -0.15;
                item.Z.Value = 0.25;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = -0.15;
                item.Z.Value = -0.25;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = 0.15;
                item.Z.Value = -0.25;
                data.Add(item);
                _cssShapeProperty.CssShapeOuter = data;
                //
                data = new ObservableCollection<XEP_ISectionShapeItem>();
                item = _resolverItem.Resolve();
                item.Y.Value = 0.05;
                item.Z.Value = -0.05;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = -0.05;
                item.Z.Value = -0.05;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = -0.05;
                item.Z.Value = 0.05;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = 0.05;
                item.Z.Value = 0.05;
                data.Add(item);
                item = _resolverItem.Resolve();
                item.Y.Value = 0.05;
                item.Z.Value = -0.05;
                data.Add(item);
                _cssShapeProperty.CssShapeInner = data;
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
        private XEP_ICssDataBase _cssAxisHorizontalProperty = new CssDataAxis();

        public XEP_ICssDataBase CssAxisHorizontal
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
        private XEP_ICssDataBase _cssAxisVerticalProperty = new CssDataAxis();

        public XEP_ICssDataBase CssAxisVertical
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