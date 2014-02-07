using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XEP_SmartControls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:XEP_SmartControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:XEP_SmartControl;assembly=XEP_SmartControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:XEP_CustomElipse/>
    ///
    /// </summary>


    [TemplateVisualState(Name = "Normal", GroupName = "GroupCommon")]
    [TemplateVisualState(Name = "MouseOver", GroupName = "GroupCommon")]
    public class XEP_CustomElipse : Control
    {
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Thickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(XEP_CustomElipse), new PropertyMetadata(1d));

        public XEP_CustomElipse()
        {
            this.DefaultStyleKey = typeof(XEP_CustomElipse);
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
