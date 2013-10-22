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
using SectionCheck.ViewModels;
using Microsoft.Practices.Unity;

namespace SectionCheck
{
    public partial class ShellView : RibbonWindow
    {
        [Dependency]
        public ShellViewModel ViewModel
        {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }

        public ShellView()
        {
            InitializeComponent();
        }
    }
}
