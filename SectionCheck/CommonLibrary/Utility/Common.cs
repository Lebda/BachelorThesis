using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using CommonLibrary.Interfaces;

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
        public static List<T> Copy<T>(List<T> source, int startPos = 0) where T:ICloneAble
        {
            Exceptions.CheckPredicate<int>(null, startPos, (start => start < 0));
            Exceptions.CheckPredicate<int, int>(null, startPos, source.Count, (start, itemCount) => itemCount <= start);
            List<T> retVal = new List<T>();
            for (int counter = startPos; counter < source.Count; ++counter)
            {
                Exceptions.CheckNullAplication(null, source[counter]);
                retVal.Add((T)source[counter].Clone());
            }
            return retVal;
        }
    }
}
