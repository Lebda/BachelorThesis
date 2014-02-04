using System;
using System.Linq;
using System.Windows.Data;
using XEP_SectionCheckInterfaces.DataCache;

namespace XEP_CssProperties.Infrastructure
{
    public class XEP_InternalForceConventer : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XEP_IInternalForceItem force = value as XEP_IInternalForceItem;

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
