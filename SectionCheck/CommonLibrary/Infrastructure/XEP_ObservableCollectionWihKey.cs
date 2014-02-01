using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using XEP_CommonLibrary.Utility;

namespace XEP_CommonLibrary.Infrastructure
{
    //[Serializable]
//     public class XEP_ObservableCollectionWihKey<T, U>
//     {
//         ObservableCollection<T> _data = new ObservableCollection<T>();
//         public ObservableCollection<T> Data
//         {
//             get { return _data; }
//             set { _data = value; }
//         }
//         readonly Dictionary<U, int> _indexes = new Dictionary<U, int>();
//         int _counter = 0;
//         public void CopyAll(XEP_ObservableCollectionWihKey<T, U> source)
//         {
//             _counter = source._counter;
//             _indexes = DeepCopy.Make<Dictionary<U, int>>(source._indexes);
//             _data.Clear();
//             foreach (var item in _indexes._data)
//             {
//                 _data.Add(item.CopyInstance());
//             }
//         }
//         public void AddOne(T newObject, U name)
//         {
//             _data.Add(newObject);
//             _indexes.Add(name, _counter);
//             _counter++;
//         }
//         public T GetOne(U name)
//         {
//             return _data[_indexes[name]];
//         }
//         public void Clear()
//         {
//             _data.Clear();
//             _indexes.Clear();
//             _counter = 0;
//         }
//     }
}
