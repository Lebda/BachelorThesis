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
using System.Windows.Shapes;
using Fluent;
using XEP_SectionCheck.ViewModels;
using Microsoft.Practices.Unity;

namespace XEP_SectionCheck
{
    public partial class XEP_ShellView : RibbonWindow
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
