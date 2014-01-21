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
        public static ComponentResourceKey ReinfStrainPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ReinfStrainPen1_SC"); } }
        public static ComponentResourceKey ReinfStrainBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ReinfStrainBrush1_SC"); } }
        public static ComponentResourceKey ConcreteStrainPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ConcreteStrainPen1_SC"); } }
        public static ComponentResourceKey ConcreteStrainBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ConcreteStrainBrush1_SC"); } }
        public static ComponentResourceKey ReinfPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ReinfPen1_SC"); } }
        public static ComponentResourceKey ReinfBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "ReinfBrush1_SC"); } }
        public static ComponentResourceKey LightGrayBackgroundBrush_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "LightGrayBackgroundBrush_SC"); } }
        public static ComponentResourceKey CssPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CssPen1_SC"); } }
        public static ComponentResourceKey CssBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CssBrush1_SC"); } }
        public static ComponentResourceKey CssPen2_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CssPen2_SC"); } }
        public static ComponentResourceKey CssBrush2_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CssBrush2_SC"); } }
        public static ComponentResourceKey CompressPartPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CompressPartPen1_SC"); } }
        public static ComponentResourceKey CompressPartBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "CompressPartBrush1_SC"); } }
        public static ComponentResourceKey SadTileBrush_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "SadTileBrush_SC"); } }
        public static ComponentResourceKey HorAxisPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "HorAxisPen1_SC"); } }
        public static ComponentResourceKey VerAxisPen1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "VerAxisPen1_SC"); } }
        public static ComponentResourceKey HorAxisBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "HorAxisBrush1_SC"); } }
        public static ComponentResourceKey VerAxisBrush1_SCkey { get { return new ComponentResourceKey(typeof(CustomResources), "VerAxisBrush1_SC"); } }

        public static Brush GetSaveBrush(ComponentResourceKey myKey)
        {
            if (Application.Current == null)
            {
                return System.Windows.Media.Brushes.AliceBlue;
            }
            object myBrush = Application.Current.TryFindResource(myKey);
            if (myBrush == null)
            {
                myBrush = System.Windows.Media.Brushes.AliceBlue;
            }
            return myBrush as System.Windows.Media.Brush;
        }

        public static Pen GetSavePen(ComponentResourceKey myKey)
        {
            if (Application.Current == null)
            {
                return new Pen(Brushes.Brown, 3);
            }
            object myPen = Application.Current.TryFindResource(myKey);
            if (myPen == null)
            {
                myPen =  new Pen(Brushes.Brown, 3);
            }
            return myPen as System.Windows.Media.Pen;
        }
    }
}
