using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace XEP_SectionCheckCommon.Infrastructure
{
    public class XEP_SkinStyleSelector : StyleSelector
    {
        public Style DefaultStyle
        {
            get; set;
        }
        public Style ChangedStyle
        {
            get; set;
        }
        public override Style SelectStyle(object item, DependencyObject container)
        {
            int count = Application.Current.Resources.MergedDictionaries.Count;
            return DefaultStyle;
        }
    }
}
