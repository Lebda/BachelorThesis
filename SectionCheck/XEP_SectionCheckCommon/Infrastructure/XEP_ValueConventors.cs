using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [ValueConversion(typeof(bool), typeof(System.Windows.Visibility))]
    public class XEP_ValueConBoolOnVisibility : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            bool data = (bool)value;
            if ((data == true && _trueOnVisible == true) || (data == false && _trueOnVisible == false))
            {
                return System.Windows.Visibility.Visible;
            }
            else
            {
                return System.Windows.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion

        bool _trueOnVisible;
        public bool TrueOnVisible
        {
            get
            {
                return this._trueOnVisible;
            }
            set
            {
                this._trueOnVisible = value;
            }
        }
    }
}
