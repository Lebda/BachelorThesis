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
using Microsoft.Practices.Unity;
using XEP_SectionCheck.ViewModels;

namespace XEP_SectionCheck
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class XEP_MainView : UserControl
    {
        [Dependency]
        public XEP_MainViewModel ViewModel
        {
            get { return this.DataContext as XEP_MainViewModel; }
            set { this.DataContext = value; }
        }

        public XEP_MainView()
        {
            InitializeComponent();

            // just for test | how to put it in the ViewModel ????
//             ResourceDictionary resourceDictionary = new ResourceDictionary();
//             resourceDictionary.Source = new Uri(
//               "ResourceLibrary;component/Themes/ShinyRed.xaml", UriKind.Relative);
//             this.Resources.MergedDictionaries[0] = resourceDictionary;
        }
    }
}
