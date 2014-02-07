using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace XEP_TelerikHelp
{
    [TemplateVisualState(Name = "Normal", GroupName = "GroupCommon")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "GroupCommon")]
    public class XEP_CustomEllipse : Control
    {
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(XEP_CustomEllipse), new PropertyMetadata(1d));

        public XEP_CustomEllipse()
        {
            this.DefaultStyleKey = typeof(XEP_CustomEllipse);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            var result = VisualStateManager.GoToState(this, "MouseOver", true);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            var result = VisualStateManager.GoToState(this, "Normal", true);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            VisualStateManager.GoToState(this, "Normal", false);
        }
    }
}
