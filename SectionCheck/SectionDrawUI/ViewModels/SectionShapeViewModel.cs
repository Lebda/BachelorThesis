using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows.Media;
using SectionCheckInterfaces.Interfaces;
using CommonLibrary.Utility;
using SectionDrawUI.Models;
using System.Windows;

namespace SectionDrawUI.Model
{
    public class SectionShapeViewModel : ObservableObject
    {
        ISectionShapeService _sectionShapeService;
        ISectionShape _sectionShapeModel;

        public SectionShapeViewModel(ISectionShapeService sectionShapeService, ISectionShape sectionShape)
        {
            _sectionShapeModel = sectionShape;
            _sectionShapeModel.Prepare();
            //
            _sectionShapeService = sectionShapeService;
            _sectionShapeService.ShapeModel = _sectionShapeModel;
            _sectionShapeService.Prepare();
            //
            _outerShapeProperty = _sectionShapeService.GetOuterSectionShape();
            _innerShapeProperty = _sectionShapeService.GetInnerSectionShape();
            _wholeShapeProperty = _sectionShapeService.GetWholeSectionShape();
            _reinforcementShapeProperty = sectionShapeService.GetReinforcementShape();
            _testShape = _sectionShapeService.GetTestShape();
        }

        /// <summary>
        /// The <see cref="OuterShape" /> property's name.
        /// </summary>
        public const string OuterShapePropertyName = "OuterShape";

        private PathGeometry _outerShapeProperty = null;

        /// <summary>
        /// Sets and gets the OuterShape property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PathGeometry OuterShapeProperty
        {
            get
            {
                return _outerShapeProperty;
            }

            set
            {
                if (_outerShapeProperty == value)
                {
                    return;
                }

                _outerShapeProperty = value;
                RaisePropertyChanged(OuterShapePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="InnerShapeProperty" /> property's name.
        /// </summary>
        public const string InnerShapePropertyPropertyName = "InnerShapeProperty";

        private PathGeometry _innerShapeProperty = null;

        /// <summary>
        /// Sets and gets the InnerShapeProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PathGeometry InnerShapeProperty
        {
            get
            {
                return _innerShapeProperty;
            }

            set
            {
                if (_innerShapeProperty == value)
                {
                    return;
                }

                _innerShapeProperty = value;
                RaisePropertyChanged(InnerShapePropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WholeShapeProperty" /> property's name.
        /// </summary>
        public const string WholeShapePropertyPropertyName = "WholeShapeProperty";

        private CombinedGeometry _wholeShapeProperty = null;

        /// <summary>
        /// Sets and gets the WholeShapeProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CombinedGeometry WholeShapeProperty
        {
            get
            {
                return _wholeShapeProperty;
            }

            set
            {
                if (_wholeShapeProperty == value)
                {
                    return;
                }

                _wholeShapeProperty = value;
                RaisePropertyChanged(WholeShapePropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ReinforcementShapeProperty" /> property's name.
        /// </summary>
        public const string ReinforcementShapePropertyPropertyName = "ReinforcementShapeProperty";

        private GeometryGroup _reinforcementShapeProperty = null;

        /// <summary>
        /// Sets and gets the ReinforcementShapeProperty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public GeometryGroup ReinforcementShapeProperty
        {
            get
            {
                return _reinforcementShapeProperty;
            }

            set
            {
                if (_reinforcementShapeProperty == value)
                {
                    return;
                }

                _reinforcementShapeProperty = value;
                RaisePropertyChanged(ReinforcementShapePropertyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TestShape" /> property's name.
        /// </summary>
        public const string TestShapePropertyName = "TestShape";

        private PathGeometry _testShape = null;

        /// <summary>
        /// Sets and gets the TestShape property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PathGeometry TestShape
        {
            get
            {
                return _testShape;
            }

            set
            {
                if (_testShape == value)
                {
                    return;
                }
                _testShape = value;
                RaisePropertyChanged(TestShapePropertyName);
            }
        }
    }
}
