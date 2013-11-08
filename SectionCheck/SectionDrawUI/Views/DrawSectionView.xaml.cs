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
using System.Windows.Media.Media3D;

namespace SectionDrawUI
{
    public partial class DrawSectionView : UserControl
    {
        [Dependency]
        public DrawSectionViewModel ViewModel
        {
            get { return this.DataContext as DrawSectionViewModel; }
            set { this.DataContext = value;}
        }

        public DrawSectionView()
        {
            InitializeComponent();
        }
        long counter = 0;
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (counter%2 == 0)
            {
                ViewModel.FibersConcrete = ViewModel.Test1(1e-4);
            }
            else
            {
                ViewModel.FibersConcrete = ViewModel.Test2(1e-4);
            }
            counter++;
            drawingScene.CssFibersConcrete4Draw = ViewModel.FibersConcrete;
        }
    }
}
