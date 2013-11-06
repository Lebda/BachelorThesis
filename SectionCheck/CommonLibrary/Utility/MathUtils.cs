using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utility
{
    public class MathUtils
    {
        public static double BarArea(double BarDiam)
        {
            return (3.14159265358979 * BarDiam * BarDiam / 4);
        }
        public static double ToRad(double Angle)
        {
            return Angle * Math.PI / 180;
        }
        //
        public static double ToDeg(double Angle)
        {
            return (Angle) * 360.0 / (2.0 * Math.PI);
        }
        //
        public static bool CompareDouble(double val1, double val2, double limit = 1e-6)
        {
            return (Math.Abs(val2 - val1) <= limit);
        }
        public static bool IsZero(double val, double limit = 1e-6)
        {
            return CompareDouble(val, 0.0, limit);
        }
        public static bool IsLessThan(double val1, double val2, double limit = 1e-6)
        {
            return ((val2) - (val1) > (limit));
        }
        public static bool IsLessOrEqual(double val1, double val2, double limit = 1e-6)
        {
            return ((val1) - (val2) <= (limit));
        }
        public static bool IsGreaterThan(double val1, double val2, double limit = 1e-6)
        {
            return (IsLessThan(val2, val1, limit));
        }
        public static bool IsGreaterOrEqual(double val1, double val2, double limit = 1e-6)
        {
            return (IsLessOrEqual(val2, val1, limit));
        }
        public static bool IsInInterval(double val, double minRange, double maxRange, double limit = 1e-6)
        {
            return (IsGreaterThan(val,minRange,limit)&&IsLessThan(val,maxRange,limit));
        }
        public static bool IsOnInterval(double val, double minRange, double maxRange, double limit = 1e-6)
        {
            return (IsGreaterOrEqual(val,minRange,limit)&&IsLessOrEqual(val,maxRange,limit));
        }
    }
}
