using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.Services;
using DisplayRecordsModule.UserDetailsWcfService;
using DisplayRecordsModule.ViewModels;
using DisplayRecordsModule.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Services;
using Services.RestClient;

namespace DisplayRecordsModule
{
    public class DisplayRecordsModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
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

            _container.RegisterType<IConfigProvider, ConfigProvider>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IDisplayModuleService, DisplayModuleService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IRestClient, RestClientImpl>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IHttpClientAdapter, HttpClientAdapter>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserDetailCallbackClientService, UserDetailCallbackClientService>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IUserDetailService, UserDetailServiceClient>(new ContainerControlledLifetimeManager());

            _container.RegisterType<ISchedulerProvider, SchedulerProvider>(new ContainerControlledLifetimeManager());

            _regionManager.RegisterViewWithRegion("Shell", typeof(Views.DisplayRecordsView));
        }
    }
}