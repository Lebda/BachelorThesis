using System;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using XEP_Prism.Infrastructure;
using XEP_SectionCheck.ModuleDefinitions;

namespace XEP_SectionCheck
{
    public class XEP_SectionCheckBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<XEP_ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog();
        }

        protected override void ConfigureModuleCatalog() 
        {
            Type mainModule = typeof(MainModule);
            ModuleCatalog.AddModule(new ModuleInfo(mainModule.Name, mainModule.AssemblyQualifiedName));


//             Type sectionDrawModule = typeof(SectionDrawModule);
//             ModuleCatalog.AddModule(new ModuleInfo(sectionDrawModule.Name, sectionDrawModule.AssemblyQualifiedName));

            DirectoryModuleCatalog directoryCatalog = new DirectoryModuleCatalog() { ModulePath = @".\DirectoryModules" };
            ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(directoryCatalog);

            // App config file is not working !!
//             ConfigurationModuleCatalog configurationCatalog = new ConfigurationModuleCatalog();
//             ((AggregateModuleCatalog)ModuleCatalog).AddCatalog(configurationCatalog);

        }
    }
}
