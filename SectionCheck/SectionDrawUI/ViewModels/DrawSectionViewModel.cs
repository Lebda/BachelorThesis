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

//         /// <summary>
//         /// The <see cref="ShapeViewModelProperty" /> property's name.
//         /// </summary>
//         public const string ShapeViewModelPropertyPropertyName = "ShapeViewModel";
// 
//         private SectionShapeViewModel _shapeViewModelProperty = null;
// 
//         /// <summary>
//         /// Sets and gets the ShapeModelProperty property.
//         /// Changes to that property's value raise the PropertyChanged event. 
//         /// </summary>
//         public SectionShapeViewModel ShapeViewModel
//         {
//             get
//             {
//                 return _shapeViewModelProperty;
//             }
// 
//             set
//             {
//                 if (_shapeViewModelProperty == value)
//                 {
//                     return;
//                 }
//                 _shapeViewModelProperty = value;
//                 RaisePropertyChanged(ShapeViewModelPropertyPropertyName);
//             }
//         }

        /// <summary>
        /// The <see cref="CssReinforcement" /> property's name.
        /// </summary>
        public const string CssReinforcementPropertyName = "CssReinforcement";

        private CssDataReinforcement _cssReinforcementProperty = new CssDataReinforcement();

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

        private CssDataCompressPart _cssCompressPartProperty = new CssDataCompressPart();

        /// <summary>
        /// Sets and gets the OuterCompressPartPath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CssDataCompressPart CssCompressPart
        {
            get
            {
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.15));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.25));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(-0.15, 0.25));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(-0.15, 0.15));
                _cssCompressPartProperty.CssCompressPart.Add(new Point(0.15, 0.15));
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

        private CssDataShape _cssShapeProperty = new CssDataShape();

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
            Application.Current.TryFindResource(CustomResources.HorAxisBrush1Key) as Brush,
            Application.Current.TryFindResource(CustomResources.HorAxisPen1Key) as Pen);
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
            Application.Current.TryFindResource(CustomResources.VerAxisBrush1Key) as Brush,
            Application.Current.TryFindResource(CustomResources.VerAxisPen1Key) as Pen);

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
