﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [Serializable]
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

        readonly XEP_ObservableCollectionWihKey<XEP_IQuantity, string> _data= new XEP_ObservableCollectionWihKey<XEP_IQuantity, string>();
        public ObservableCollection<XEP_IQuantity> Data
        {
            get { return _data.Data; }
        }

        protected XEP_ObservableCollectionWihKey<XEP_IQuantity, string> CopyAllQuanties()
        {
            return _data.CopyAll();
        }
        protected void AddOneQuantity(XEP_IQuantityManager manager, double value, eEP_QuantityType type, string name)
        {
            _data.AddOne(XEP_QuantityFactory.Instance().Create(manager, value, type, name), name);
        }
        public XEP_IQuantity GetOneQuantity(string name)
        {
            return _data.GetOne(name);
        }
        protected void ClearQuanties()
        {
            _data.Clear();
        }

        protected void SetItem(ref XEP_IQuantity valueFromBinding, string index, params string[] names)
        {
            XEP_IQuantity data4Index = GetOneQuantity(index);
            if (data4Index == valueFromBinding || !SetItemFromBinding(ref valueFromBinding, data4Index))
            {
                return;
            }
            data4Index.Value = valueFromBinding.Value;
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