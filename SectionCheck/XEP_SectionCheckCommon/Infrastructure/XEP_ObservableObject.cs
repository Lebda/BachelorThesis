using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;
using System.Collections.Generic;
using System.ComponentModel;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public class XEP_ObservableObject : ObservableObject
    {
        public static T GetOneData<T>(ObservableCollection<T> source, string name)
            where T : XEP_IDataCacheObjectBase
        {
            return Enumerable.Where<T>(source, (e) => e.Name.Equals(name)).SingleOrDefault();
        }

        public static T GetOneData<T>(ObservableCollection<T> source, Guid id)
            where T : XEP_IDataCacheObjectBase
        {
            return Enumerable.Where<T>(source, (e) => e.Id.Equals(id)).SingleOrDefault();
        }

        public static eDataCacheServiceOperation SaveOneData<T>(ObservableCollection<T> source, T matData)
            where T : XEP_IDataCacheObjectBase
        {
            Exceptions.CheckNull(matData);
            T data = GetOneData<T>(source, matData.Id);
            if (data == null)
            {
                source.Add(matData);
            }
            else
            {
                if (data.Name == matData.Name)
                {
                    matData.Name += "-copy";
                }
                data = matData;
            }
            return eDataCacheServiceOperation.eSuccess;
        }

        public static eDataCacheServiceOperation RemoveOneData<T>(ObservableCollection<T> source, T matData)
            where T : XEP_IDataCacheObjectBase
        {
            Exceptions.CheckNull(matData);
            T data = GetOneData(source, matData.Id);
            if (data == null)
            {
                return eDataCacheServiceOperation.eNotFound;
            }
            source.Remove(data);
            return eDataCacheServiceOperation.eSuccess;
        }

        ObservableCollection<XEP_IQuantity> _data = new ObservableCollection<XEP_IQuantity>();
        Dictionary<string, int> _indexes = new Dictionary<string, int>();

        public bool CallPropertySet4ManagedValue(string propertyName, double oldValue)
        {
            VerifyPropertyName(propertyName);
            XEP_IQuantity testObject = GetOneQuantity(propertyName);
            XEP_IQuantity copy = testObject.CopyInstance();
            testObject.ManagedValue = oldValue;
            TypeDescriptor.GetProperties(this)[propertyName].SetValue(this, copy);
            return true;
        }
        public bool CallPropertySet4NewManagedValue(string propertyName, double newValue)
        {
            VerifyPropertyName(propertyName);
            XEP_IQuantity testObject = GetOneQuantity(propertyName);
            if (testObject.ManagedValue == newValue)
            {
                return false;
            }
            XEP_IQuantity copy = testObject.CopyInstance();
            copy.ManagedValue = newValue;
            double valueOld = testObject.ManagedValue;
            TypeDescriptor.GetProperties(this);
            TypeDescriptor.GetProperties(this)[propertyName].SetValue(this, copy);
            if (testObject.ManagedValue == valueOld)
            {
                return false;
            }
            foreach (var item in _data)
            { // raise property change in rest
                item.ManagedValue = item.ManagedValue;
            }
            return true;
        }
        public static readonly string DataPropertyName = "Data";
        public virtual ObservableCollection<XEP_IQuantity> Data
        {
            get
            {
                return _data;
            }
            set 
            { 
                _data = value;
                RaisePropertyChanged(DataPropertyName);
            }
        }
        public XEP_IQuantity GetOneQuantity(string name)
        {
            return _data[_indexes[name]];
        }
        public bool GetOneQuantityBool(string name)
        {
            return MathUtils.GetBoolFromDouble(GetOneQuantity(name).Value);
        }
        protected void CopyAllQuanties(XEP_ObservableObject source, XEP_IDataCacheObjectBase owner)
        {
            _indexes = DeepCopy.Make<Dictionary<string, int>>(source._indexes);
            _data.Clear();
            foreach (var item in source._data)
            {
                _data.Add(item.CopyInstance());
                item.Owner = owner;
            }
        }
        protected void AddOneQuantity(XEP_IQuantityManager manager, double value, eEP_QuantityType type, string name, XEP_IDataCacheObjectBase owner = null)
        {
            XEP_IQuantity data = XEP_QuantityFactory.Instance().Create(manager, value, type, name);
            data.Owner = owner;
            _data.Add(data);
            _indexes.Add(name, _data.Count - 1);
        }
        protected void ClearQuanties()
        {
            _data.Clear();
            _indexes.Clear();
        }
        protected void SetItemBoolWithActions(ref bool valueFromBinding, string index, Func<bool> isSetValid, Action provideNeccessary, params string[] names)
        {
            if (isSetValid != null && !isSetValid())
            {
                return;
            }
            XEP_IQuantity data4Index = GetOneQuantity(index);
            if (valueFromBinding == MathUtils.GetBoolFromDouble(data4Index.Value))
            {
                return;
            }
            data4Index.Value = MathUtils.GetDoubleFromBool(valueFromBinding);
            FinishSet(provideNeccessary, index, names);
            return;
        }
        protected void SetItem(ref XEP_IQuantity valueFromBinding, string index, params string[] names)
        {
            SetItemWithActions(ref valueFromBinding, index, null, null, names);
        }
        protected void SetItemWithActions(ref XEP_IQuantity valueFromBinding, string index, Func<bool> isSetValid, Action provideNeccessary, params string[] names)
        {
            if (isSetValid != null && !isSetValid())
            {
                return;
            }
            XEP_IQuantity data4Index = GetOneQuantity(index);
            if (data4Index == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, data4Index))
            {
                return;
            }
            data4Index.Value = valueFromBinding.Value;
            FinishSet(provideNeccessary, index, names);
        }
        protected void SetMemberWithAction<T>(ref T newValue, ref T member, Func<bool> isSetValid, Action provideNeccessary, params string[] names4Raised)
        {
            if (isSetValid != null && !isSetValid())
            {
                return;
            }
            member = newValue;
            if (provideNeccessary != null)
            {
                provideNeccessary();
            }
            foreach (string item in names4Raised)
            {
                RaisePropertyChanged(item);
            }
        }
        private void FinishSet(Action provideNeccessary, string index, params string[] names)
        {
            if (provideNeccessary != null)
            {
                provideNeccessary();
            }
            if (names == null || names.Count() == 0)
            {
                RaisePropertyChanged(index);
            }
            else
            {
                foreach (string item in names)
                {
                    RaisePropertyChanged(item);
                }
            }
        }
        private bool SetItemFromBinding(ref XEP_IQuantity valueFromBinding, XEP_IQuantity propertyItem)
        {
            if (valueFromBinding == null)
            {
                return false;
            }
            if (valueFromBinding.Manager == null && string.IsNullOrEmpty(valueFromBinding.Name) && valueFromBinding.QuantityType == eEP_QuantityType.eNoType)
            { // setting throw binding
                valueFromBinding.Owner = propertyItem.Owner;
                valueFromBinding.Manager = propertyItem.Manager;
                valueFromBinding.Name = propertyItem.Name;
                valueFromBinding.QuantityType = propertyItem.QuantityType;
                valueFromBinding.Value = valueFromBinding.Manager.GetValueManaged(valueFromBinding.Value, valueFromBinding.QuantityType);
                if (valueFromBinding.Value == propertyItem.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }
}