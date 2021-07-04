using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.Services;
using DisplayRecordsModule.ViewModels;
using DisplayRecordsModule.Views;
using log4net;

namespace DisplayRecordsModule.Factories
{
    public class AddViewModelFactory : IAddViewModelFactory
    {
        private readonly IDisplayModuleService _displayModuleService;
        private readonly ILog _log;

        public AddViewModelFactory(IDisplayModuleService displayModuleService, ILog log)
        {
            _displayModuleService = displayModuleService;
            _log = log;
        }

        private IAddViewModel Create()
        {
            return new AddRecordViewModel(_displayModuleService, _log);
        }

        public IAddRecordView CreateView()
        {
            IAddRecordView window = new AddRecordView(Create() as AddRecordViewModel);
            return window;
        }
    }
}
