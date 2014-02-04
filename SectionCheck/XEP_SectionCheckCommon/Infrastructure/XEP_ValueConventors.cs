using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class XEP_BoolNegConventer : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? data = value as bool?;
            if (data != null)
            {
                return (data.Equals(true)) ? false : true;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(Int32), typeof(XEP_IQuantity))]
    public class XEP_SBYTEIQuantity_Conventer : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XEP_IQuantity data = value as XEP_IQuantity;
            if (data != null)
            {
                if (data.QuantityType == eEP_QuantityType.eBool)
                {
                    return 1;
                }
                else if (data.QuantityType == eEP_QuantityType.eEnum)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [ValueConversion(typeof(bool), typeof(XEP_IQuantity))]
    public class XEP_IsBoolIQuantityConventer : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XEP_IQuantity data = value as XEP_IQuantity;
            if (data != null)
            {
                return (data.QuantityType==eEP_QuantityType.eBool) ? true : false;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

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
