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
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace XEP_CssProperties.ViewModels
{
    public class XEP_CssPropertiesViewModel : XEP_ObservableObject
    {
        readonly XEP_IResolver<XEP_ISectionShapeItem> _resolverShapeItem = null;
        readonly XEP_IResolver<XEP_IInternalForceItem> _resolverForce = null;
        private XEP_IInternalForceItem _activeForce = null;
        private ObservableCollection<XEP_IInternalForceItem> _internalForces = null;
        private XEP_ICssDataBase _cssAxisHorizontalProperty = new CssDataAxis();
        private XEP_ICssDataBase _cssAxisVerticalProperty = new CssDataAxis();

        public XEP_CssPropertiesViewModel(XEP_IDataCache dataCache, 
            XEP_IResolver<XEP_IInternalForceItem> resolverForce,
            XEP_IResolver<XEP_ICssDataShape> resolverICssDataShape,
            XEP_IResolver<XEP_ISectionShapeItem> resolverShapeItem)
        {
            _dataCache = Exceptions.CheckNull(dataCache);
            _resolverShapeItem = resolverShapeItem;
            _cssAxisHorizontalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.HorAxisBrush1_SCkey);
            _cssAxisHorizontalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.HorAxisPen1_SCkey);
            _cssAxisVerticalProperty.VisualBrush = CustomResources.GetSaveBrush(CustomResources.VerAxisBrush1_SCkey);
            _cssAxisVerticalProperty.VisualPen = CustomResources.GetSavePen(CustomResources.VerAxisPen1_SCkey);
            _resolverForce = resolverForce;

            if (_dataCache.Structure != null &&_dataCache.Structure.MemberData != null && _dataCache.Structure.MemberData.Count > 0)
            {
                ObservableCollection<XEP_IOneSectionData> sectionsData = _dataCache.Structure.MemberData[0].SectionsData;
                if (sectionsData.Count > 0)
                {
                    _activeSectionData = sectionsData[0];
                    _internalForces = _activeSectionData.InternalForces;
                }
                _activeMatConcreteFromLibrary = _dataCache.MaterialLibrary.GetOneMaterialDataConcrete(_activeSectionData.ConcreteSectionData.MaterialData.Name);
            }
        }

        private string _SeriesType = "Scatter point";
        public string SeriesType
        {
            get
            {
                return this._SeriesType;
            }
            set
            {
                if (this._SeriesType != value)
                {
                    this._SeriesType = value;
                    this.RaisePropertyChanged("SeriesType");
                }
            }
        }

        XEP_IDataCache _dataCache = null; // singleton
        public static readonly string DataCachePropertyName = "DataCache";
        public XEP_IDataCache DataCache
        {
            get { return _dataCache; }
            set { SetMember<XEP_IDataCache>(ref value, ref _dataCache, (_dataCache == value), DataCachePropertyName); }
        }
        public const string ActiveSectionDataPropertyName = "ActiveSectionData";
        XEP_IOneSectionData _activeSectionData = null;
        public XEP_IOneSectionData ActiveSectionData
        {
            get { return _activeSectionData; }
            set { SetMember<XEP_IOneSectionData>(ref value, ref _activeSectionData, (_activeSectionData == value), ActiveSectionDataPropertyName); }
        }
        public const string ActiveMatConcreteFromLibraryPropertyName = "ActiveMatConcreteFromLibrary";
        XEP_IMaterialDataConcrete _activeMatConcreteFromLibrary = null;
        public XEP_IMaterialDataConcrete ActiveMatConcreteFromLibrary
        {
            get { return _activeMatConcreteFromLibrary; }
            set { SetMember<XEP_IMaterialDataConcrete>(ref value, ref _activeMatConcreteFromLibrary, (_activeMatConcreteFromLibrary == value), ActiveMatConcreteFromLibraryPropertyName); }
        }

        #region MATERIAL COMANDS
        public ICommand AddMatToLibCommand
        {
            get { return new RelayCommand(AddMatToLibExecute, CanAddMatToLibExecute); }
        }
        Boolean CanAddMatToLibExecute()
        {
            if (_activeSectionData == null || _activeSectionData.ConcreteSectionData == null || _activeSectionData.ConcreteSectionData.MaterialData == null ||
                _activeSectionData.ConcreteSectionData.MaterialData.MatFromLibMode.IsTrue())
            {
                return false;
            }
            return true;
        }
        void AddMatToLibExecute()
        {
            try
            {
                XEP_IMaterialDataConcrete newConcrete = Exceptions.CheckNull(_activeSectionData.ConcreteSectionData.MaterialData.Clone() as XEP_IMaterialDataConcrete);
                newConcrete.ResetMatFromLib();
                _dataCache.MaterialLibrary.SaveOneMaterialDataConcrete(newConcrete);
                _activeSectionData.ConcreteSectionData.MaterialData = newConcrete.Clone() as XEP_IMaterialDataConcrete;
                ActiveMatConcreteFromLibrary = _dataCache.MaterialLibrary.GetOneMaterialDataConcrete(_activeSectionData.ConcreteSectionData.MaterialData.Name);
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }
        }
        public ICommand MaterialLibraryConcreteChangedCommand
        {
            get { return new RelayCommand<object>(MaterialLibraryConcreteChangedExecute); }
        }
        void MaterialLibraryConcreteChangedExecute(object obj)
        {
            if (_activeSectionData == null || _activeSectionData.ConcreteSectionData == null || _activeSectionData.ConcreteSectionData.MaterialData == null)
            {
                return;
            }
            SelectionChangedEventArgs e = obj as SelectionChangedEventArgs;
            RadComboBox myComboBox = e.Source as RadComboBox;
            if (myComboBox == null || e == null || e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }
            if (myComboBox.SelectedIndex < 0 || String.IsNullOrEmpty(myComboBox.Text))
            {
                return;
            }
            XEP_IMaterialDataConcrete newSelectedMat = e.AddedItems[0] as XEP_IMaterialDataConcrete;
            if (newSelectedMat == null)
            {
                return;
            }
            XEP_eMaterialDiagramType matTypeSave = _activeSectionData.ConcreteSectionData.MaterialData.DiagramType.GetEnumValue<XEP_eMaterialDiagramType>();
            _activeSectionData.ConcreteSectionData.MaterialData = newSelectedMat.Clone() as XEP_IMaterialDataConcrete;
            _activeSectionData.ConcreteSectionData.MaterialData.DiagramType.SetEnumValue(matTypeSave);
            ActiveMatConcreteFromLibrary = _dataCache.MaterialLibrary.GetOneMaterialDataConcrete(_activeSectionData.ConcreteSectionData.MaterialData.Name);
        }
        #endregion

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


        public const string ActiveForcePropertyName = "ActiveForce";
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

        public const string InternalForcesPropertyName = "InternalForces";
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
