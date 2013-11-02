using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Infrastructure;
using System.Windows;
using System.Windows.Media;
using ResourceLibrary;

namespace SectionDrawerControl.Infrastructure
{
    public class CssDataAxis : CssDataBase
    {
        public CssDataAxis()
            : base( Application.Current.TryFindResource(CustomResources.HorAxisBrush1Key) as Brush, 
                    Application.Current.TryFindResource(CustomResources.HorAxisPen1Key) as Pen)
        {
        }
        public CssDataAxis(Brush newBrush, Pen newPen)
            : base(newBrush, newPen)
        {
        }
        public override PathGeometry Create()
        {
            return null;
        }
    }
}
