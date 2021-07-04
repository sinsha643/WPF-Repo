using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.ViewModels;
using DisplayRecordsModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace DisplayRecordsModule
{
    public class DisplayRecordsModuleInit : IModule
    {
        private IRegionManager _regionManager;
        private IUnityContainer _container;
        public DisplayRecordsModuleInit(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IWindowService, WindowService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAddViewModelFactory, AddViewModelFactory>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IAddRecordView, AddRecordView>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAddViewModel, AddRecordViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IAddViewModelFactory, AddViewModelFactory>(new ContainerControlledLifetimeManager());

            _regionManager.RegisterViewWithRegion("Shell", typeof(Views.DisplayRecordsView));
        }
    }
}