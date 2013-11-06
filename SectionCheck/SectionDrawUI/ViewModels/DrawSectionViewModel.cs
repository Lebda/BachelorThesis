using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using SectionCheckInterfaces.Interfaces;
using SectionDrawUI.Model;
using CommonLibrary.Utility;
using SectionDrawerControl.Infrastructure;
using ResourceLibrary;
using CommonLibrary.Geometry;

namespace SectionDrawUI.ViewModels
{
    public class DrawSectionViewModel : ObservableObject
    {
        //ISectionShapeService _sectionShapeService;

        public DrawSectionViewModel(ISectionShapeService sectionShapeService, ISectionShape sectionShape)
        {
//             _sectionShapeService = sectionShapeService;
//             _sectionShapeService.CanvasData = new CanvasDataContext();
//             ShapeViewModel = new SectionShapeViewModel(_sectionShapeService, sectionShape);
        }

        /// <summary>
        /// The <see cref="FibersConcrete" /> property's name.
        /// </summary>
        public const string FibersConcretePropertyName = "FibersConcrete";

        private CssDataFibers _fibersConcreteProperty = new CssDataFibers(
            Application.Current.TryFindResource(CustomResources.ConcreteStrainBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.ConcreteStrainPen1_SCkey) as Pen);

        /// <summary>
        /// Sets and gets the FibersConcrete property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CssDataFibers FibersConcrete
        {
            get
            {
                double test = 1e-4;
//                 _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, -0.25), -2600000, -20 * test, -0.25));
//                 _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, 0.25), 2600000, 20 * test, 0.25));
//                 _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(-0.15, 0.25), 2600000, 20 * test, 0.25));
//                 _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(-0.15, -0.25), -2600000, -20 * test, -0.25));
//                 _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, -0.25), -2600000, -20 * test, -0.25));
                //
                _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, -0.25), -2600000, 57.0204 * test, -0.30285));
                _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, 0.25), 2600000, -14.919 * test, 0.07949));
                _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(-0.15, 0.25), 2600000, 21.37 * test, -0.11384));
                _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(-0.15, -0.25), -2600000, 92.81 * test, -0.49617));
                _fibersConcreteProperty.Fibers.Add(new CssDataFiber(0, new Point(0.15, -0.25), -2600000, 57.0204 * test, -0.30285));
                _fibersConcreteProperty.NeuAxis = Line2DFactory.Instance().Create(new Point(0.0266539, 0.250), new Point(0.150, 0.1460493));
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

        /// <summary>
        /// The <see cref="FibersReinforcement" /> property's name.
        /// </summary>
        public const string FibersReinforcementPropertyName = "FibersReinforcement";

        private CssDataFibers _fibersReinforcementProperty = new CssDataFibers();

        /// <summary>
        /// Sets and gets the FibersReinforcement property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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


        /// <summary>
        /// The <see cref="CssReinforcement" /> property's name.
        /// </summary>
        public const string CssReinforcementPropertyName = "CssReinforcement";

        private CssDataReinforcement _cssReinforcementProperty = new CssDataReinforcement(
            Application.Current.TryFindResource(CustomResources.ReinfBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.ReinfPen1_SCkey) as Pen);

        /// <summary>
        /// Sets and gets the CssReinforcement property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// Sets and gets the OuterCompressPartPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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


        /// <summary>
        /// The <see cref="OuterShapePath" /> property's name.
        /// </summary>
        public const string CssShapePropertyName = "CssShape";

        private CssDataShape _cssShapeProperty = new CssDataShape(
            Application.Current.TryFindResource(CustomResources.CssBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.CssPen1_SCkey) as Pen);

        /// <summary>
        /// Sets and gets the OuterShapePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// The <see cref="CssAxisHorizontal" /> property's name.
        /// </summary>
        public const string CssAxisHorizontalPropertyName = "CssAxisHorizontal";

        private CssDataAxis _cssAxisHorizontalProperty = new CssDataAxis(
            Application.Current.TryFindResource(CustomResources.HorAxisBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.HorAxisPen1_SCkey) as Pen);
        /// <summary>
        /// Sets and gets the CssAxisHorizontal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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

        /// <summary>
        /// The <see cref="CssAxisHorizontal" /> property's name.
        /// </summary>
        public const string CssAxisVerticalPropertyName = "CssAxisVertical";

        private CssDataAxis _cssAxisVerticalProperty = new CssDataAxis(
            Application.Current.TryFindResource(CustomResources.VerAxisBrush1_SCkey) as Brush,
            Application.Current.TryFindResource(CustomResources.VerAxisPen1_SCkey) as Pen);

        /// <summary>
        /// Sets and gets the CssAxisVertical property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
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
