using System;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Unity;
using XEP_SectionCheck.ViewModels;

namespace XEP_SectionCheck
{
    public partial class XEP_ShellView : Window
    {
        [Dependency]
        public XEP_ShellViewModel ViewModel
        {
            get { return this.DataContext as XEP_ShellViewModel; }
            set { this.DataContext = value; }
        }

        public XEP_ShellView()
        {
            InitializeComponent();
        }
    }
}
