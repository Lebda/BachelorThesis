using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utility
{
    public static class Exceptions
    {
        public static void CheckNullArgument(string _errorMessage, params Object[] _references)
        {
            if (_errorMessage == null)
            {
                _errorMessage = "Argument(s) is(are) null";
            }
            foreach (var reference in _references)
            {
                if (reference == null)
                {
                    throw new ArgumentException(_errorMessage);
                }
            }
        }
        //
        public static void CheckNullAplication(string _errorMessage, params Object[] _references)
        {
            if (_errorMessage == null)
            {
                _errorMessage = "Argument(s) is(are) null";
            }
            foreach (var reference in _references)
            {
                if (reference == null)
                {
                    throw new ApplicationException(_errorMessage);
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
