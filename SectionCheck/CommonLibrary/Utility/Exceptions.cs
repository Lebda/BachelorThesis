using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utility
{
    public static class Exceptions
    {
        public static void CheckNull(params Object[] parameters)
        {
            foreach (var iter in parameters)
            {
                if (iter == null)
                {
                    throw new ArgumentException("Argument is null");
                }
            }
        }
        //
        public static T CheckNull<T>(T param)
        {
            if (param == null)
            {
                throw new ArgumentException("Argument is null");
            }
            return param;
        }
        //
        public static void CheckNullArgument(string errorMessage, params Object[] parameters)
        {
            if (errorMessage == null)
            {
                errorMessage = "Argument(s) is(are) null";
            }
            foreach (var iter in parameters)
            {
                if (iter == null)
                {
                    throw new ArgumentException(errorMessage);
                }
            }
        }
        //
        public static void CheckNullAplication(string errorMessage, params Object[] parameters)
        {
            if (errorMessage == null)
            {
                errorMessage = "Argument(s) is(are) null";
            }
            foreach (var iter in parameters)
            {
                if (iter == null)
                {
                    throw new ApplicationException(errorMessage);
                }
            }
        }
        public static void CheckPredicate<T>(string errorMessage, T param, Predicate<T> predicate)
        {
            if (errorMessage == null)
            {
                errorMessage = "Argument(s) is(are) null";
            }
            if (predicate(param))
            {
                throw new ApplicationException(errorMessage);
            }
        }
        public static void CheckPredicate<T1, T2>(string errorMessage, T1 param1, T2 param2, Func<T1 ,T2, bool> predicate)
        {
            if (errorMessage == null)
            {
                errorMessage = "Argument(s) is(are) null";
            }
            if (predicate(param1, param2))
            {
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
