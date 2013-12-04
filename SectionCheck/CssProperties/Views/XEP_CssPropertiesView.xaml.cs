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
using XEP_CssProperties.ViewModels;
using Microsoft.Practices.Unity;
using XEP_SectionCheckCommon.Interfaces;

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
