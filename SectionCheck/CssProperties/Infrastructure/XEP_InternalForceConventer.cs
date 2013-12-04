using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using XEP_SectionCheckCommon.Infrastructure;

namespace XEP_CssProperties.Infrastructure
{
    public class XEP_InternalForceConventer : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XEP_InternalForceItem force = value as XEP_InternalForceItem;

            if (force != null)
            {
                return force.UsedInCheck ? true : false;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
