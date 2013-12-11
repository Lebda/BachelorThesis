using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using XEP_SectionDrawUI.ViewModels;

namespace XEP_SectionDrawUI
{
    public partial class XEP_DrawSectionView : UserControl
    {
        [Dependency]
        public XEP_DrawSectionViewModel ViewModel
        {
            get { return this.DataContext as XEP_DrawSectionViewModel; }
            set { this.DataContext = value;}
        }

        public XEP_DrawSectionView()
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
