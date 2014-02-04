using System;
using System.Linq;
using System.Windows.Controls;
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
