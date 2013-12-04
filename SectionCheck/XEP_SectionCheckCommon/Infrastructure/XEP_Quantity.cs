using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [Serializable]
    public class XEP_Quantity
    {
        public XEP_Quantity(double value, eEP_QuantityType type)
        {
            _value = value;
            _quantityType = type;
        }
        double _value = 0.0;
        public double Value
        {
            get { return _value; }
            set { _value = value; }
        }
        eEP_QuantityType _quantityType = eEP_QuantityType.eNoType;
        public eEP_QuantityType QuantityType
        {
            get { return _quantityType; }
            set { _quantityType = value; }
        }
    }

    [Serializable]
    public class XEP_QuantityDefinition
    {
        double _scale = 1.0;
        public double Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        string _quantityName = "";
        public string QuantityName
        {
            get { return _quantityName; }
            set { _quantityName = value; }
        }
        string _quantityNameScale = "";
        public string QuantityNameScale
        {
            get { return _quantityNameScale; }
            set { _quantityNameScale = value; }
        }
    }
}
