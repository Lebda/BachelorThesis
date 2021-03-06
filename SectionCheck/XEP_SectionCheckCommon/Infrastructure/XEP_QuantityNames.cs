﻿using System;
using System.Linq;
using XEP_SectionCheckInterfaces.Infrastructure;

namespace XEP_SectionCheckCommon.Infrastructure
{
    static public class XEP_QuantityNames
    {
        public static string GetUnitName(eEP_QuantityType type)
        {
            string name;
            switch (type)
            {
                case eEP_QuantityType.eNoType:
                case eEP_QuantityType.eBool:
                case eEP_QuantityType.eEnum:
                case eEP_QuantityType.eString:
                default:
                    name = "";
                    break;
                case eEP_QuantityType.eNoUnit:
                    name = "-";
                    break;
                case eEP_QuantityType.eForce:
                    name = "N";
                    break;
                case eEP_QuantityType.eMoment:
                    name = "Nm";
                    break;
                case eEP_QuantityType.eStress:
                    name = "Pa";
                    break;
                case eEP_QuantityType.eStrain:
                    name = "‰";
                    break;
                case eEP_QuantityType.eCssLength:
                    name = "m";
                    break;
            }
            return name;
        }
        public static string GetUnitName(eEP_ForceItemType type)
        {
            string name;
            switch (type)
            {
                case eEP_ForceItemType.eULS:
                default:
                    name = "ULS";
                    break;
                case eEP_ForceItemType.eSLS:
                    name = "SLS";
                    break;
            }
            return name;
        }
        public static string GetScaleName(double scale, eEP_QuantityType type)
        {
            if (scale == 1.0 || type == eEP_QuantityType.eStrain || type == eEP_QuantityType.eNoUnit || type == eEP_QuantityType.eBool || type == eEP_QuantityType.eEnum || type == eEP_QuantityType.eString)
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
