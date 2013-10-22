using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ResourceLibrary
{
    public static class CustomResources
    {
        public static ComponentResourceKey ExpressionDark
        {
            get
            {
                ResourceDictionary resourceDictionary = new ResourceDictionary();
                resourceDictionary.Source = new Uri(
                  "ResourceLibrary;component/ExpressionDark.xaml", UriKind.Relative);
                return new ComponentResourceKey(
                    typeof(ResourceDictionary), "ExpressionDark");
            }
        }
    }
}
