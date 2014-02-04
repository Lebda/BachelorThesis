using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace XEP_SectionCheck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    { 
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ensure the current culture passed into bindings 
            // is the OS culture. By default, WPF uses en-US 
            // as the culture, regardless of the system settings.

            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                  XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);
            XEP_SectionCheckBootstrapper bootstrapper = new XEP_SectionCheckBootstrapper();
            bootstrapper.Run();
        }
    }
}
