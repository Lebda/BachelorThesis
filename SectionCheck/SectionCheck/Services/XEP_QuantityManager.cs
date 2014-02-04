using System;
using System.Collections.Generic;
using System.Linq;
using XEP_CommonLibrary.Utility;
using XEP_SectionCheckCommon.DataCache;
using XEP_SectionCheckCommon.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace SectionCheck.Services
{
    [Serializable]
    public class XEP_QuantityManager : XEP_IQuantityManager
    {
        Dictionary<eEP_QuantityType, XEP_QuantityDefinition> _data = new Dictionary<eEP_QuantityType, XEP_QuantityDefinition>();
        public XEP_QuantityManager()
        {
            for (int counter = (int)eEP_QuantityType.eNoType; counter < (int)eEP_QuantityType.eQuantityTypeCount; ++counter)
            {
                XEP_QuantityDefinition quantityDefinition = new XEP_QuantityDefinition();
                quantityDefinition.QuantityName = XEP_QuantityNames.GetUnitName((eEP_QuantityType)counter);
                quantityDefinition.QuantityNameScale = XEP_QuantityNames.GetScaleName(quantityDefinition.Scale, (eEP_QuantityType)counter);
                _data.Add((eEP_QuantityType)counter, quantityDefinition);
            }
        }
        public string GetNameWithUnit(XEP_IQuantity source)
        {
            Exceptions.CheckNull(source);
            string builder = String.Empty;
            bool isSpecialType = (source.QuantityType == eEP_QuantityType.eBool || source.QuantityType == eEP_QuantityType.eEnum);
            if (!isSpecialType)
            {
                builder = "[" + _data[source.QuantityType].QuantityNameScale + _data[source.QuantityType].QuantityName + "]";
            }
            return builder;
        }
        public string GetName(XEP_IQuantity source)
        {
            return _data[source.QuantityType].QuantityName;
        }
        public double GetValueManaged(double value, eEP_QuantityType type)
        {
            Exceptions.CheckPredicate<double>("Scale can not be zero !!", _data[type].Scale, (param => MathUtils.IsZero(param, 1e-12)));
            return value * _data[type].Scale;
        }
        public double GetValue(XEP_IQuantity source)
        {
            Exceptions.CheckPredicate<double>("Scale can not be zero !!", _data[source.QuantityType].Scale, (param => MathUtils.IsZero(param, 1e-12)));
            return source.Value / _data[source.QuantityType].Scale;
        }
        public void SetScale(eEP_QuantityType type, double scaleValue)
        {
            Exceptions.CheckPredicate<double>("Scale can not be zero !!", scaleValue, (param => MathUtils.IsZero(param, 1e-12)));
            _data[type].Scale = scaleValue;
            _data[type].QuantityNameScale = XEP_QuantityNames.GetScaleName(_data[type].Scale, type);
        }
    }
}
