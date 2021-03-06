﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XEP_CommonLibrary.Utility
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
        public static double CorrectOnRange(double value, double minRange, double maxRange, double limit = 1e-6)
        {
            if (!IsOnInterval(value, minRange, maxRange, limit))
            {
                if (IsLessOrEqual(value, minRange, limit))
                {
                    value = minRange;
                }
                else
                {
                    value = maxRange;
                }
            }
            return value;
        }
        public static double CorrectInRange(double value, double minRange, double maxRange, double limit = 1e-6)
        {
            if (!IsInInterval(value, minRange, maxRange, limit))
            {
                if (IsLessThan(value, minRange, limit))
                {
                    value = minRange;
                }
                else
                {
                    value = maxRange;
                }
            }
            return value;
        }
        static public T FindMaxValue<T>(List<T> list, Converter<T, double> projection)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double maxValue = double.MinValue;
            T maxObejct = list[0];
            foreach (T item in list)
            {
                double value = projection(item);
                if (value > maxValue)
                {
                    maxValue = value;
                    maxObejct = item;
                }
            }
            return maxObejct;
        }
        static public T FindMinValue<T>(List<T> list, Converter<T, double> projection)
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Empty list");
            }
            double minValue = double.MaxValue;
            T minObject = list[0];
            foreach (T item in list)
            {
                double value = projection(item);
                if (value < minValue)
                {
                    minValue = value;
                    minObject = item;
                }
            }
            return minObject;
        }

        static public bool GetBoolFromDouble(double value)
        {
            if (MathUtils.CompareDouble(value, 0.0, 1e-6))
            {
                return false;
            }
            return true;
        }

        static public double GetDoubleFromBool(bool value)
        {
            if (value == false)
            {
                return 0.0;
            }
            else
            {
                return 1.0;
            }
        }
    }
}
