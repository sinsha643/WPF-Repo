using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace DisplayRecordsModule
{
    public class DisplayRecordsModuleInit : IModule
    {
        private IRegionManager _regionManager;

        public DisplayRecordsModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Shell", typeof(Views.DisplayRecordsView));
        }
    }
}