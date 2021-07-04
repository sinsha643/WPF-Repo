using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using log4net;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace TestWPFApp
{
    public class Bootstrapper : UnityBootstrapper
    {
        public Bootstrapper()
        {
            Log = LogManager.GetLogger("DisplayApp");
        }
        protected ILog Log { get; }

        #region Overridden Methods

        /// <summary>      
        /// Initializes shell.xaml      
        /// </summary>      
        /// <returns></returns>      
        protected override DependencyObject CreateShell()
        {
            return new MainShellView();
        }

        /// <summary>      
        /// loads the Shell.xaml      
        /// </summary>      
        protected override void InitializeShell()
        {
            App.Current.MainWindow = (Window)Shell;
            App.Current.MainWindow.Show();
        }

        /// <summary>      
        /// Add view(module) from other assemblies and begins with modularity      
        /// </summary>      
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(DisplayRecordsModule.DisplayRecordsModuleInit));
        }

        protected override IUnityContainer CreateContainer()
        {
            var container = base.CreateContainer();
            return container;
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterInstance(Log);

            base.ConfigureContainer();
        }
        #endregion
    }
}

