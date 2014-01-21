using System;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using XEP_CssProperties.ViewModels;

namespace XEP_CssProperties
{
    /// <summary>
    /// Interaction logic for CssPropertiesView.xaml
    /// </summary>
    public partial class XEP_CssPropertiesView : UserControl
    {
        [Dependency]
        public XEP_CssPropertiesViewModel ViewModel
        {
            get { return this.DataContext as XEP_CssPropertiesViewModel; }
            set { this.DataContext = value; }
        }

        public XEP_CssPropertiesView()
        {
            InitializeComponent();
        }
    }
}
