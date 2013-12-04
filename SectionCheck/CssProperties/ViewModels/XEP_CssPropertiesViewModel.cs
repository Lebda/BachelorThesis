using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_SectionCheckCommon.Interfaces;
using System.Collections.ObjectModel;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_CommonLibrary.Infrastructure;
using System.Windows;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : ObservableObject
    {
        XEP_IQuantityManager _quantityManager = null;
        public XEP_IQuantityManager QuantityManager
        {
            get { return _quantityManager; }
            set { _quantityManager = value; }
        }
        XEP_ICssPropertiesService _cssPropertiesService = null;
        public XEP_CssPropertiesViewModel(XEP_IQuantityManager quantityManager,
            XEP_ICssPropertiesService cssPropertiesService)
        {
            _quantityManager = Exceptions.CheckNull(quantityManager);
            _cssPropertiesService = Exceptions.CheckNull(cssPropertiesService);
            InternalForces = _cssPropertiesService.GetInternalForces();
        }

        /// <summary>
        /// The <see cref="InternalForces" /> property's name.
        /// </summary>
        public const string InternalForcesPropertyName = "InternalForces";

        private ObservableCollection<XEP_InternalForceItem> _internalForces = new ObservableCollection<XEP_InternalForceItem>();

        /// <summary>
        /// Sets and gets the InternalForces property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<XEP_InternalForceItem> InternalForces
        {
            get
            {
                return _internalForces;
            }
            set
            {
                if (_internalForces == value)
                {
                    return;
                }
                _internalForces = value;
                RaisePropertyChanged(InternalForcesPropertyName);
            }
        }
    }
}
