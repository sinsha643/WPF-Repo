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
        private readonly IUserDetailCallbackClientService _callbackService;

        public AddViewModelFactory(IDisplayModuleService displayModuleService, 
                                   ILog log, 
                                   IUserDetailCallbackClientService callbackService)
        {
            _displayModuleService = displayModuleService;
            _log = log;
            _callbackService = callbackService;
        }

        private IAddViewModel Create()
        {
            return new AddRecordViewModel(_displayModuleService, _log, _callbackService);
        }

        public IAddRecordView CreateView()
        {
            IAddRecordView window = new AddRecordView(Create() as AddRecordViewModel);
            return window;
        }
    }
}
