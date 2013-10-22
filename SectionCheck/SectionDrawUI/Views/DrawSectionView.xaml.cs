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
using SectionDrawUI.ViewModels;
using Microsoft.Practices.Unity;

namespace SectionDrawUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DrawSectionView : UserControl
    {
        [Dependency]
        public DrawSectionViewModel ViewModel
        {
            get { return this.DataContext as DrawSectionViewModel; }
            set { this.DataContext = value; }
        }

        public DrawSectionView()
        {
            InitializeComponent();
        }
    }
}
