using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using XEP_CommonLibrary.Infrastructure;
using System.Windows.Input;
using XEP_SectionCheckCommon.DataCache;
using XEP_Prism.Infrastructure;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : ObservableObject
    {
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        readonly XEP_IDataCache _dataCache = null; // singleton
        readonly XEP_IOneSectionData _activeSectionData = null;
        private XEP_IInternalForceItem _activeForce = null;
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;

        public XEP_CssPropertiesViewModel(XEP_IDataCache dataCache, XEP_IResolver<XEP_IInternalForceItem> resolverForce)
        {
            _resolverForce = resolverForce;
            _dataCache = dataCache;
            if (_dataCache.Structure != null &&_dataCache.Structure.MemberData != null && _dataCache.Structure.MemberData.Values.Count > 0)
            {
                Dictionary<Guid, XEP_IOneSectionData> sectionsData = (_dataCache.Structure.MemberData.Values.First()).SectionsData;
                if (sectionsData.Count > 0)
                {
                    _activeSectionData = sectionsData.First().Value;
                    _internalForces = _activeSectionData.InternalForces;
                }
            }
        }

        #region Commands

        public ICommand NewCommand
        {
            get { return new RelayCommand(NewExecute); }
        }
        void NewExecute()
        {
            _internalForces.Add(_resolverForce.Resolve());
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
                XEP_IInternalForceItem newItem = this.ActiveForce.CopyInstance();
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
        /// <summary>
        /// Sets and gets the ActiveForce property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_IInternalForceItem ActiveForce
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
        /// <summary>
        /// Sets and gets the InternalForces property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<XEP_IInternalForceItem> InternalForces
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
