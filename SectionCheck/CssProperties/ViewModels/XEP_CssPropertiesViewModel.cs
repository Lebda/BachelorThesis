using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ResourceLibrary;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionDrawer.Infrastructure;
using System.Windows.Media;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : ObservableObject
    {
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        readonly XEP_IDataCache _dataCache = null; // singleton
        XEP_IOneSectionData _activeSectionData = null;
        private XEP_IInternalForceItem _activeForce = null;
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        private CssDataShape _cssShapeProperty = new CssDataShape(CustomResources.GetSaveBrush(CustomResources.CssBrush2_SCkey), CustomResources.GetSavePen(CustomResources.CssPen2_SCkey));
        private CssDataAxis _cssAxisHorizontalProperty = new CssDataAxis(CustomResources.GetSaveBrush(CustomResources.HorAxisBrush1_SCkey), CustomResources.GetSavePen(CustomResources.HorAxisPen1_SCkey));
        private CssDataAxis _cssAxisVerticalProperty = new CssDataAxis(CustomResources.GetSaveBrush(CustomResources.VerAxisBrush1_SCkey), CustomResources.GetSavePen(CustomResources.VerAxisPen1_SCkey));

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

        public ICommand NewForceCommand
        {
            get { return new RelayCommand(NewForceExecute); }
        }
        void NewForceExecute()
        {
            _internalForces.Add(_resolverForce.Resolve());
            ResetForm();
        }
        public ICommand DelereForceCommand
        {
            get { return new RelayCommand(DeleteForceExecute, CanDeleteForceExecute); }
        }
        Boolean CanDeleteForceExecute()
        {
            return this.ActiveForce != null;
        }
        void DeleteForceExecute()
        {
            if (!CanDeleteForceExecute())
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
        public ICommand CopyForceCommand
        {
            get { return new RelayCommand(CopyForceExecute, CanCopyForceExecute); }
        }
        Boolean CanCopyForceExecute()
        {
            return this.ActiveForce != null;
        }
        void CopyForceExecute()
        {
            if (!CanCopyForceExecute())
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


        public const string CssShapePropertyName = "CssShape";
        public CssDataShape CssShape
        {
            get
            {
                Exceptions.CheckIsNull(_cssShapeProperty, _cssShapeProperty.CssShapeOuter, _cssShapeProperty.CssShapeInner);
                _cssShapeProperty.CssShapeOuter.Clear();
                foreach(var shapeItem in _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter)
                {
                    _cssShapeProperty.CssShapeOuter.Add(new System.Windows.Point(shapeItem.Point.X, shapeItem.Point.Y));
                }
                _cssShapeProperty.CssShapeInner.Clear();
                foreach (var shapeItem in _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner)
                {
                    _cssShapeProperty.CssShapeInner.Add(new System.Windows.Point(shapeItem.Point.X, shapeItem.Point.Y));
                }
                return _cssShapeProperty;
            }
            protected set
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
        public const string ActiveSectionDataPropertyName = "ActiveSectionData";
        public XEP_IOneSectionData ActiveSectionData
        {
            get { return _activeSectionData; }
            set
            {
                if (_activeSectionData == value) { return; }
                _activeSectionData = value;
                RaisePropertyChanged(ActiveSectionDataPropertyName);
            }
        }
    }
}
