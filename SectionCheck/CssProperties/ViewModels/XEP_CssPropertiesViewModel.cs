using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ResourceLibrary;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : XEP_ObservableObject
    {
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        readonly XEP_IDataCache _dataCache = null; // singleton
        XEP_IOneSectionData _activeSectionData = null;
        private XEP_IInternalForceItem _activeForce = null;
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        private XEP_IMaterialDataConcrete _activeMatConcrete = null;
        private XEP_IMaterialLibrary _materialLibrary = null;
        private List<XEP_IMaterialDataConcrete> _matConcreteHelp = new List<XEP_IMaterialDataConcrete>();
        private CssDataShape _cssShapeProperty = new CssDataShape(CustomResources.GetSaveBrush(CustomResources.CssBrush2_SCkey), CustomResources.GetSavePen(CustomResources.CssPen2_SCkey));
        private CssDataAxis _cssAxisHorizontalProperty = new CssDataAxis(CustomResources.GetSaveBrush(CustomResources.HorAxisBrush1_SCkey), CustomResources.GetSavePen(CustomResources.HorAxisPen1_SCkey));
        private CssDataAxis _cssAxisVerticalProperty = new CssDataAxis(CustomResources.GetSaveBrush(CustomResources.VerAxisBrush1_SCkey), CustomResources.GetSavePen(CustomResources.VerAxisPen1_SCkey));

        public XEP_CssPropertiesViewModel(XEP_IDataCache dataCache, XEP_IResolver<XEP_IInternalForceItem> resolverForce)
        {
            _resolverForce = resolverForce;
            _dataCache = dataCache;
            _materialLibrary = dataCache.MaterialLibrary;
            if (_dataCache.Structure != null &&_dataCache.Structure.MemberData != null && _dataCache.Structure.MemberData.Count > 0)
            {
                ObservableCollection<XEP_IOneSectionData> sectionsData = _dataCache.Structure.MemberData[0].SectionsData;
                if (sectionsData.Count > 0)
                {
                    _activeSectionData = sectionsData[0];
                    _internalForces = _activeSectionData.InternalForces;
                    _matConcreteHelp.Add(_activeSectionData.ConcreteSectionData.MaterialData);
                    _activeMatConcrete = _activeSectionData.ConcreteSectionData.MaterialData;
                }
            }
        }

        #region Commands
        public ICommand ChangeOuterShapeCommand
        {
            get { return new RelayCommand<GridViewRowEditEndedEventArgs>(ChangeOuterShapeExecute); }
        }

        //public DelegateCommand TestCommand2 { get; set; }
        public void ChangeOuterShapeExecute(object obj)
        {
            GridViewRowEditEndedEventArgs e = obj as GridViewRowEditEndedEventArgs;

            if (e.EditAction == GridViewEditAction.Commit)
            {
                CssShape = CssShape.CopyInstance();
            }
        }

        public ICommand AddMatToLib
        {
            get { return new RelayCommand(AddMatToLibExecute, CanAddMatToLibExecute); }
        }
        Boolean CanAddMatToLibExecute()
        {
            return (this._activeMatConcrete != null) && (_activeMatConcrete.MatFromLib == false);
        }
        void AddMatToLibExecute()
        {
            if (!CanAddMatToLibExecute())
            {
                return;
            }
            try
            {
                XEP_IMaterialData newItem = this._activeMatConcrete.CopyInstance();
                XEP_IMaterialDataConcrete  newItemConcrete = newItem as XEP_IMaterialDataConcrete;
                Exceptions.CheckNull(newItemConcrete);
                _dataCache.MaterialLibrary.SaveOneMaterialDataConcrete(newItemConcrete);
                _activeMatConcrete.ResetMatFromLib();
                _cssShapeProperty.CssShapeOuter = _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter;
                _cssShapeProperty.CssShapeInner = _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner;
                CssShape = CssShape.CopyInstance();
                RaisePropertyChanged(CssShapePropertyName);
                ResetForm();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
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
                _cssShapeProperty.CssShapeOuter = _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter;
                _cssShapeProperty.CssShapeInner = _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner;
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
        public static readonly string MaterialDataConcretePropertyName = "MaterialDataConcrete";
        public List<XEP_IMaterialDataConcrete> MaterialDataConcrete
        {
            get { return _matConcreteHelp; }
            set
            {
                if (_matConcreteHelp == value) { return; }
                _matConcreteHelp = value;
                RaisePropertyChanged(MaterialDataConcretePropertyName);
            }
        }

        public static readonly string ActiveMatConcretePropertyName = "ActiveMatConcrete";
        public XEP_IMaterialDataConcrete ActiveMatConcrete
        {
            get { return _activeMatConcrete; }
            set
            {
                if (_activeMatConcrete == value) { return; }
                _activeMatConcrete = value;
                RaisePropertyChanged(ActiveMatConcretePropertyName);
            }
        }
        //
        public static readonly string MaterialLibraryPropertyName = "MaterialLibrary";
        public XEP_IMaterialLibrary MaterialLibrary
        {
            get { return _materialLibrary; }
            set { SetMember<XEP_IMaterialLibrary>(ref value, ref _materialLibrary, (_materialLibrary == value), MaterialLibraryPropertyName); }
        }
    }
}
