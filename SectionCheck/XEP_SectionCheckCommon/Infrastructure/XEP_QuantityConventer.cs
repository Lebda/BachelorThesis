using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Utility;
using System.Windows.Data;
using XEP_SectionCheckCommon.Interfaces;
using System.Globalization;
using System.Windows;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public class XEP_QuantityConventer : DependencyObject, IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            XEP_InternalForceItem force = values[0] as XEP_InternalForceItem;
            XEP_IQuantityManager manager = values[1] as XEP_IQuantityManager;
            Exceptions.CheckNull(force, manager);
            return force.GetString(manager);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
