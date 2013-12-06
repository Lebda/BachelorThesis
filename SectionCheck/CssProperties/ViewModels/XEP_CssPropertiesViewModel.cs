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
using Microsoft.Practices.Unity;
using System.Windows.Input;

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
        public XEP_CssPropertiesViewModel(IUnityContainer container, XEP_IQuantityManager quantityManager)
        {
            _quantityManager = Exceptions.CheckNull(quantityManager);
            _cssPropertiesService = Exceptions.CheckNull(UnityContainerExtensions.Resolve<XEP_ICssPropertiesService>(container, "CssPropertiesModule"));
            InternalForces = _cssPropertiesService.GetInternalForces();
        }

        #region Commands

        public ICommand NewCommand
        {
            get { return new RelayCommand(NewExecute); }
        }
        void NewExecute()
        {
            XEP_InternalForceItem newItem = _cssPropertiesService.CreateForce();
            _internalForces.Add(newItem);
            ResetForm();
        }
        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteExecute, CanDeleteExecute); }
        }
        Boolean CanDeleteExecute()
        {
            return this.ActiveForce != null;
        }
        void DeleteExecute()
        {
            if (!CanDeleteExecute())
            {
                return;
            }
            try
            {
                _internalForces.Remove(this.ActiveForce);
                ResetForm();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        public ICommand CopyCommand
        {
            get { return new RelayCommand(CopyExecute, CanCopyExecute); }
        }
        Boolean CanCopyExecute()
        {
            return this.ActiveForce != null;
        }
        void CopyExecute()
        {
            if (!CanCopyExecute())
            {
                return;
            }
            try
            {
                XEP_InternalForceItem newItem = new XEP_InternalForceItem(this.ActiveForce);
                newItem.Name += "-copy";
                _internalForces.Add(newItem);
                ResetForm();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }

        #endregion //Commands

        #region Methods
        void ResetForm()
        {
            RaisePropertyChanged(InternalForcesPropertyName);
            RaisePropertyChanged(ActiveForcePropertyName);
            this.ActiveForce = null;
        }
        #endregion
        /// <summary>
        /// The <see cref="ActiveForce" /> property's name.
        /// </summary>
        public const string ActiveForcePropertyName = "ActiveForce";

        private XEP_InternalForceItem _activeForce = null;

        /// <summary>
        /// Sets and gets the ActiveForce property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_InternalForceItem ActiveForce
        {
            get
            {
                return _activeForce;
            }
            set
            {
                if (_activeForce == value)
                {
                    return;
                }
                _activeForce = value;
                RaisePropertyChanged(ActiveForcePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="InternalForces" /> property's name.
        /// </summary>
        public const string InternalForcesPropertyName = "InternalForces";

        private ObservableCollection<XEP_InternalForceItem> _internalForces = null;

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
