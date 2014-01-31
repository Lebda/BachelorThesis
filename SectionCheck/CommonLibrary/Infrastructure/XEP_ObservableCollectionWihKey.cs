using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using XEP_CommonLibrary.Utility;

namespace XEP_CommonLibrary.Infrastructure
{
    [Serializable]
    public class XEP_ObservableCollectionWihKey<T, U>
    {
        readonly ObservableCollection<T> _data = new ObservableCollection<T>();
        public ObservableCollection<T> Data
        {
            get { return _data; }
        }
        readonly Dictionary<U, int> _indexes = new Dictionary<U, int>();
        int _counter = 0;
        public XEP_ObservableCollectionWihKey<T, U> CopyAll()
        {
           return DeepCopy.Make<XEP_ObservableCollectionWihKey<T, U>>(this);
        }
        public void AddOne(T newObject, U name)
        {
            _data.Add(newObject);
            _indexes.Add(name, _counter);
            _counter++;
        }
        public T GetOne(U name)
        {
            return _data[_indexes[name]];
        }
        public void Clear()
        {
            _data.Clear();
            _indexes.Clear();
            _counter = 0;
        }
    }
}
