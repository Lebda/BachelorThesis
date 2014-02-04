using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ResourceLibrary;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_Prism.Infrastructure;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;
using XEP_SectionCheckInterfaces.SectionDrawer;
using XEP_SectionDrawer.Infrastructure;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : XEP_ObservableObject
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolverShapeItem = null;
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        //readonly XEP_IResolver<XEP_ICssDataShape> _resolverICssDataShape = null;
        readonly XEP_IDataCache _dataCache = null; // singleton
        XEP_IOneSectionData _activeSectionData = null;
        private XEP_IInternalForceItem _activeForce = null;
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        private XEP_IMaterialDataConcrete _activeMatConcrete = null;
        private XEP_IMaterialLibrary _materialLibrary = null;
        private List<XEP_IMaterialDataConcrete> _matConcreteHelp = new List<XEP_IMaterialDataConcrete>();
        //private XEP_ICssDataShape _cssShapeProperty = null;
        private XEP_ICssDataBase _cssAxisHorizontalProperty = new CssDataAxis();
        private XEP_ICssDataBase _cssAxisVerticalProperty = new CssDataAxis();
        XEP_IEnum2StringManager _enum2StringManager = null; // singleton
        public XEP_IEnum2StringManager Enum2StringManager
        {
            get { return _enum2StringManager; }
            set { _enum2StringManager = value; }
        }
        public XEP_CssPropertiesViewModel(XEP_IDataCache dataCache, 
            XEP_IResolver<XEP_IInternalForceItem> resolverForce,
            XEP_IResolver<XEP_ICssDataShape> resolverICssDataShape,
            XEP_IResolver<XEP_ISectionShapeItem> resolverShapeItem,
             XEP_IEnum2StringManager enum2StringManager)
        {
            _enum2StringManager = enum2StringManager;
            _resolverShapeItem = resolverShapeItem;
            _cssAxisHorizontalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.HorAxisBrush1_SCkey);
            _cssAxisHorizontalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.HorAxisPen1_SCkey);
            _cssAxisVerticalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.VerAxisBrush1_SCkey);
            _cssAxisVerticalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.VerAxisPen1_SCkey);
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

        #region Cross section COMANDS
        XEP_ISectionShapeItem _activeShapeItem = null;
        public static readonly string ActiveShapeItemPropertyName = "ActiveShapeItem";
        public XEP_ISectionShapeItem ActiveShapeItem
        {
            get { return _activeShapeItem; }
            set { SetMemberWithAction<XEP_ISectionShapeItem>(ref value, ref _activeShapeItem, () => _activeShapeItem != value, null); }
        }
        // ADD
        public ICommand NewPointCommand
        {
            get { return new RelayCommand(NewPointExecute, CanNewPointExecute); }
        }
        void NewPointExecute()
        {
            if (_activeSectionData != null)
            {
                AddPointInternal(_activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter, _resolverShapeItem, true);
            }
            ResetCrossSectionForm();
        }
        private void AddPointInternal(ObservableCollection<XEP_ISectionShapeItem> shape, XEP_IResolver<XEP_ISectionShapeItem> resolverShapeItem, bool isOuter = true)
        {
            if (shape == null)
            {
                return;
            }
            if (shape.Count() == 0)
            {
                XEP_ISectionShapeItem newItem = resolverShapeItem.Resolve();
                newItem.PointType.SetEnumValue((isOuter) ? (eEP_CssShapePointType.eOuter) : (eEP_CssShapePointType.eInner));
                shape.Add(newItem);
            }
            else
            {
                shape.Add(shape.Last().Clone() as XEP_ISectionShapeItem);
            }
        }
        Boolean CanNewPointExecute()
        {
            if (_activeSectionData != null)
            {
                return _activeSectionData.ConcreteSectionData.SectionShape.PolygonMode.IsTrue();
            }
            return false;
        }
        public ICommand NewPointInnerCommand
        {
            get { return new RelayCommand(NewPointInnerExecute, CanNewPointExecute); }
        }
        void NewPointInnerExecute()
        {
            if (_activeSectionData != null)
            {
                AddPointInternal(_activeSectionData.ConcreteSectionData.SectionShape.ShapeInner, _resolverShapeItem);
            }
            ResetCrossSectionForm();
        }
        // DELETE
        public ICommand DeletePointCommand
        {
            get { return new RelayCommand(DeletePointExecute, CanDeletePointExecute); }
        }
        Boolean CanDeletePointExecute()
        {
            return (_activeShapeItem != null && _activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eOuter && CanNewPointExecute());
        }
        public ICommand DeletePointInnerCommand
        {
            get { return new RelayCommand(DeletePointExecute, CanDeletePointInnerExecute); }
        }
        Boolean CanDeletePointInnerExecute()
        {
            return (_activeShapeItem != null && _activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eInner && CanNewPointExecute());
        }
        void DeletePointExecute()
        {
            try
            {
                if (_activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eOuter)
                {
                    _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter.Remove(_activeShapeItem);
                }
                else if (_activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eInner)
                {
                    _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner.Remove(_activeShapeItem);
                }
                ResetCrossSectionForm();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        // COPY
        public ICommand CopyPointCommand
        {
            get { return new RelayCommand(CopyPointExecute, CanDeletePointExecute); }
        }
        public ICommand CopyPointInnerCommand
        {
            get { return new RelayCommand(CopyPointExecute, CanDeletePointInnerExecute); }
        }
        void CopyPointExecute()
        {
            try
            {
                XEP_ISectionShapeItem newItem = _activeShapeItem.Clone() as XEP_ISectionShapeItem;
                newItem.Name += "-copy";
                if (_activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eOuter)
                {
                    _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter.Add(newItem);
                }
                else if (_activeShapeItem.PointType.GetEnumValue<eEP_CssShapePointType>() == eEP_CssShapePointType.eInner)
                {
                    _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner.Add(newItem);
                }
                ResetCrossSectionForm();
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        // RESET
        void ResetCrossSectionForm()
        {
            if (_activeSectionData != null)
            {
                _activeSectionData.ConcreteSectionData.SectionShape.Intergrity(null);
            }
            _activeShapeItem = null;
        }
        #endregion

        #region Commands

        public ICommand AddMatToLib
        {
            get { return new RelayCommand(AddMatToLibExecute, CanAddMatToLibExecute); }
        }
        Boolean CanAddMatToLibExecute()
        {
            return (this._activeMatConcrete != null) && (!_activeMatConcrete.MatFromLibMode.IsTrue());
        }
        void AddMatToLibExecute()
        {
            if (!CanAddMatToLibExecute())
            {
                return;
            }
            try
            {
                XEP_IMaterialData newItem = this._activeMatConcrete.Clone() as XEP_IMaterialData;
                XEP_IMaterialDataConcrete  newItemConcrete = newItem as XEP_IMaterialDataConcrete;
                Exceptions.CheckNull(newItemConcrete);
                _dataCache.MaterialLibrary.SaveOneMaterialDataConcrete(newItemConcrete);
                _activeMatConcrete.ResetMatFromLib();
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


//         public const string CssShapePropertyName = "CssShape";
//         public XEP_ICssDataShape CssShape
//         {
//             get
//             {
//                 Exceptions.CheckIsNull(_cssShapeProperty, _cssShapeProperty.CssShapeOuter, _cssShapeProperty.CssShapeInner);
//                 _cssShapeProperty.CssShapeOuter = _activeSectionData.ConcreteSectionData.SectionShape.ShapeOuter;
//                 _cssShapeProperty.CssShapeInner = _activeSectionData.ConcreteSectionData.SectionShape.ShapeInner;
//                 return _cssShapeProperty;
//             }
//             protected set
//             {
//                 if (_cssShapeProperty == value)
//                 {
//                     return;
//                 }
//                 _cssShapeProperty = value;
//                 RaisePropertyChanged(CssShapePropertyName);
//             }
//         }

        public const string CssAxisHorizontalPropertyName = "CssAxisHorizontal";
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
