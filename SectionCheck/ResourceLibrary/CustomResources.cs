using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ResourceLibrary
{
    public static class CustomResources 
    {
        public static ComponentResourceKey SadTileBrushKey
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "SadTileBrush");
            }
        }

        public static ComponentResourceKey HorAxisPen1Key
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "HorAxisPen1");
            }
        }
        public static ComponentResourceKey VerAxisPen1Key
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "VerAxisPen1");
            }
        }

        public static ComponentResourceKey HorAxisBrush1Key
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "HorAxisBrush1");
            }
        }
        public static ComponentResourceKey VerAxisBrush1Key
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "VerAxisBrush1");
            }
        }
    }
}
