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

namespace SectionDrawUI.ViewModels
{
    public class DrawSectionViewModel : ObservableObject
    {
        ISectionShapeService _sectionShapeService;

        public DrawSectionViewModel(ISectionShapeService sectionShapeService, ISectionShape sectionShape)
        {
            _sectionShapeService = sectionShapeService;
            _sectionShapeService.CanvasData = new CanvasDataContext();
            ShapeViewModel = new SectionShapeViewModel(_sectionShapeService, sectionShape);
        }

        /// <summary>
        /// The <see cref="ShapeViewModelProperty" /> property's name.
        /// </summary>
        public const string ShapeViewModelPropertyPropertyName = "ShapeViewModel";

        private SectionShapeViewModel _shapeViewModelProperty = null;

        /// <summary>
        /// Sets and gets the ShapeModelProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SectionShapeViewModel ShapeViewModel
        {
            get
            {
                return _shapeViewModelProperty;
            }

            set
            {
                if (_shapeViewModelProperty == value)
                {
                    return;
                }
                _shapeViewModelProperty = value;
                RaisePropertyChanged(ShapeViewModelPropertyPropertyName);
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
                _cssShapeProperty.CssShapeInner.Add(new Point(0.05, 0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(-0.05, 0.05));
                _cssShapeProperty.CssShapeInner.Add(new Point(-0.05, -0.05));
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

        private CssDataAxis _cssAxisHorizontalProperty = new CssDataAxis();

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
    }
}
