using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using XEP_CommonLibrary.Infrastructure;

namespace XEP_SectionCheck.ViewModels
{
    public class XEP_ShellViewModel : ObservableObject
    {

        #region Commands

        public ICommand ChangeSkinCommand
        {
            get { return new XEP_RelayCommand(ChangeSkinExecute); }
        }
        void ChangeSkinExecute(Object parameter)
        {
            string skinName = parameter.ToString();
            List<string> uris = new List<string>();
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/System.Windows.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.Input.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.Data.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.DataVisualization.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.GridView.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Controls.RibbonView.xaml");
            uris.Add(@"/Telerik.Windows.Themes." + skinName + @";component/Themes/Telerik.Windows.Documents.xaml");
            Application.Current.Resources.MergedDictionaries.Clear();
            foreach (string item in uris)
            {
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                {
                    Source = new Uri(item, UriKind.RelativeOrAbsolute)
                });
            }
        }
        #endregion //Commands
    }
}
