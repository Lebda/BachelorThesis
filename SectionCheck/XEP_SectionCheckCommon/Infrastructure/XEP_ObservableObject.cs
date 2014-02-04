using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

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

        [field: NonSerialized]
        ObservableCollection<XEP_IQuantity> _data = new ObservableCollection<XEP_IQuantity>();
        Dictionary<string, int> _indexes = new Dictionary<string, int>();

        public void NotifyOwnerProperty(XEP_IDataCacheNotificationData notificationData)
        {
            if (notificationData != null && notificationData.Owner != null)
            {
                Action<XEP_IDataCacheNotificationData> notificationMethod = notificationData.Owner.GetNotifyOwnerAction();
                if (notificationMethod != null)
                {
                    notificationMethod(notificationData);
                }
            }
        }

        public bool CallPropertySet4NewManagedValue(string propertyName, double newManagedValue)
        {
            VerifyPropertyName(propertyName);
            XEP_IQuantity testObject = GetOneQuantity(propertyName);
            if (testObject.ManagedValue == newManagedValue)
            {
                return false;
            }
            XEP_IQuantity copy = testObject.Clone() as XEP_IQuantity;
            copy.ManagedValue = newManagedValue;
            double valueOld = testObject.ManagedValue;
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
                _data.Add(item.Clone() as XEP_IQuantity);
                item.Owner = owner;
            }
        }
        protected void AddOneQuantity(double value, eEP_QuantityType type, string name, XEP_IDataCacheObjectBase owner = null, string enumName = null)
        {
            XEP_IQuantity data = XEP_QuantityFactory.Instance().Create(value, type, name, enumName);
            data.Owner = owner;
            data.EnumName = enumName;
            _data.Add(data);
            _indexes.Add(name, _data.Count - 1);
        }
        protected void ClearQuanties()
        {
            _data.Clear();
            _indexes.Clear();
        }

        protected void SetItem(ref XEP_IQuantity valueFromBinding, string index, params string[] names)
        {
            SetItemWithActions(ref valueFromBinding, index, null, null, names);
        }
        protected void SetItemWithActions(ref XEP_IQuantity valueFromBinding, string index, Func<bool> isSetValid, Action<string> provideNeccessary, params string[] names)
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
        protected void SetMemberWithAction<T>(ref T newValue, ref T member, Func<bool> isSetValid, Action<string> provideNeccessary, params string[] names4Raised)
        {
            if (isSetValid != null && !isSetValid())
            {
                return;
            }
            member = newValue;
            if (provideNeccessary != null && names4Raised != null && names4Raised.Count() != 0)
            {
                provideNeccessary(names4Raised[0]);
            }
            foreach (string item in names4Raised)
            {
                RaisePropertyChanged(item);
            }
        }
        private void FinishSet(Action<string> provideNeccessary, string index, params string[] names)
        {
            if (provideNeccessary != null)
            {
                provideNeccessary(index);
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