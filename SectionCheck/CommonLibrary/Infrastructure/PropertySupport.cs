using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using XEP_CommonLibrary.Utility;

namespace XEP_CommonLibrary.Infrastructure
{
    public static class PropertySupport
    {
        public static String ExtractPropertyName<T>(Expression<Func<T>> propertyExpresssion)
        {
            if (propertyExpresssion == null)
            {
                throw new ArgumentNullException("propertyExpresssion");
            }

            var memberExpression = propertyExpresssion.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("The expression is not a member access expression.", "propertyExpresssion");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("The member access expression does not access a property.", "propertyExpresssion");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic)
            {
                throw new ArgumentException("The referenced property is a static property.", "propertyExpresssion");
            }

            return memberExpression.Member.Name;
        }

        public static void OnMyPropertyChanged<TpropHolder, TproVal>(TpropHolder sender, DependencyPropertyChangedEventArgs e, Action<TproVal> setter, Action callback)
            where TpropHolder : class
        {
            Exceptions.CheckNull(sender, e);
            if (e.OldValue != e.NewValue)
            {
                if (setter != null)
                {
                    setter((TproVal)e.NewValue);
                }
                if (callback != null)
                {
                    callback();
                }
            }
        }
    }
}
