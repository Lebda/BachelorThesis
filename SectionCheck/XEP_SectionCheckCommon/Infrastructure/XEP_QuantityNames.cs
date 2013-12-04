using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEP_SectionCheckCommon.Infrastructure
{
    static public class XEP_QuantityNames
    {
        public static string GetName(eEP_QuantityType type)
        {
            string name;
            switch (type)
            {
                case eEP_QuantityType.eNoType:
                default:
                    name = "";
                    break;
                case eEP_QuantityType.eForce:
                    name = "N";
                    break;
                case eEP_QuantityType.eMoment:
                    name = "Nm";
                    break;
            }
            return name;
        }
        public static string GetName(eEP_ForceItemType type)
        {
            string name;
            switch (type)
            {
                case eEP_ForceItemType.eULS:
                default:
                    name = "ULS";
                    break;
                case eEP_ForceItemType.eSLS:
                    name = "N";
                    break;
            }
            return name;
        }
        public static string GetScaleName(double scale)
        {
            if (scale == 1.0)
            {
                return "";
            }
            if (scale == 1000.0)
            {
                return "k";
            }
            if (scale == 1000000.0)
            {
                return "M";
            }
            if (scale == 0.001)
            {
                return "m";
            }
            if (scale == 0.000001)
            {
                return "µ";
            }
            return scale.ToString();
        }
    }
}
