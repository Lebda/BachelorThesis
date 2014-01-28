using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Interfaces;
using XEP_SectionCheckCommon.Res;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public class XEP_QuantityConventerType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            eEP_ForceItemType? type = value as eEP_ForceItemType?;
            Exceptions.CheckNull(type);
            string result;
            switch (type)
            {
                case eEP_ForceItemType.eULS:
                default:
                    result = "ULS";
                    break;
                case eEP_ForceItemType.eSLS:
                    result = "SLS";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string typeValue = value.ToString();
            if (typeValue == "ULS")
            {
                return eEP_ForceItemType.eULS;
            }
            else
            {
                return eEP_ForceItemType.eSLS;
            }
        }
    }

    public class XEP_QuantityConventerValue4Item : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            XEP_IQuantity force = value as XEP_IQuantity;
            Exceptions.CheckNull(force);
            XEP_IQuantityManager manager = force.Manager;
            Exceptions.CheckNull(force, manager);
            return manager.GetValue(force);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string forceValue = value.ToString();
            double result;
            if (double.TryParse(forceValue, NumberStyles.Any, culture, out result))
            {
                XEP_IQuantity finalForce = XEP_QuantityFactory.Instance().Create(null, result, eEP_QuantityType.eNoType, "");
                return finalForce;
            }
            return null;
        }
    }

    public class XEP_QuantityConventerNameWithUnit : IValueConverter
    {
        #region IValueConverter Members
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string paramValue = parameter.ToString();
            long type = 0;
            if (long.TryParse(paramValue, NumberStyles.Any, culture, out type))
            {
                XEP_IInternalForceItem forces = value as XEP_IInternalForceItem;
                Exceptions.CheckNull(forces);
                XEP_IQuantity force = forces.GetItem((eEP_ForceType)type);
                return force.Name + " " + forces.Manager.GetNameWithUnit(force);
            }
            return "";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    
        #endregion
    }
    
    [ValueConversion(typeof(XEP_IQuantity), typeof(string))]
    public class XEP_QCNameWithUnitGeneral : IValueConverter
    {
        #region IValueConverter Members
            
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            XEP_IQuantity data = value as XEP_IQuantity;
            Exceptions.CheckNull(data);
            return data.Name + " " + data.Manager.GetNameWithUnit(data);
        }
            
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    
        #endregion
    }
        
    [ValueConversion(typeof(XEP_IQuantity), typeof(string))]
    public class XEP_QCManagedValueGeneral : IValueConverter
    {
        #region IValueConverter Members
                
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            XEP_IQuantity data = value as XEP_IQuantity;
            XEP_IQuantityManager manager = data.Manager;
            Exceptions.CheckNull(data, manager);
            return manager.GetValue(data);
        }
            
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dataValue = value.ToString();
            double result;
            if (double.TryParse(dataValue, NumberStyles.Any, culture, out result))
            {
                XEP_IQuantity finalData = XEP_QuantityFactory.Instance().Create(null, result, eEP_QuantityType.eNoType, "");
                return finalData;
            }
            return null;
        }
        #endregion
    }

    [ValueConversion(typeof(XEP_IQuantity), typeof(string))]
    public class XEP_QCNameOnMarkGeneral : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            XEP_IQuantity data = value as XEP_IQuantity;
            Exceptions.CheckNull(data);
            return Resources.ResourceManager.GetString(data.Name + "_MARK");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}