using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace CommonLibrary.Utility
{
    static public class Common
    {
        public static bool IsEmpty<T>(List<T> list4check)
        {
            if (list4check != null && list4check.Count > 0)
            {
                return false;
            }
            return true;
        }
        public static bool IsEmpty<T>(ObservableCollection<T> list4check)
        {
            if (list4check != null && list4check.Count > 0)
            {
                return false;
            }
            return true;
        }
        public static T Copy<T, U>(T source, int startPos = 0)
            where U : ICloneable, new()
            where T : List<U>, new()
        {
            Exceptions.CheckPredicate<int>(null, startPos, (start => start < 0));
            Exceptions.CheckPredicate<int, int>(null, startPos, source.Count, (start, itemCount) => itemCount <= start);
            T retVal = new T();
            for (int counter = startPos; counter < source.Count; ++counter)
            {
                Exceptions.CheckNullAplication(null, source[counter]);
                U test = source[counter];
                retVal.Add((U)test.Clone());
            }
            return retVal;
        }

        public static void SetProperty<T>(T data4Set, Action<T> propertySetCall)
        {
            Exceptions.CheckNull(propertySetCall);
            propertySetCall(data4Set);
        }

        public static void SetPropertyIfNotNull<T>(T data4Set, Action<T> propertySetCall)
        {
            if (data4Set == null)
            {
                return;
            }
            SetProperty<T>(data4Set, propertySetCall);
        }
    }
}
