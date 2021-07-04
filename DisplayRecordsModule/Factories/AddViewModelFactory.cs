using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.ViewModels;
using DisplayRecordsModule.Views;

namespace DisplayRecordsModule.Factories
{
    public class AddViewModelFactory : IAddViewModelFactory
    {
        public AddViewModelFactory()
        {
        }

        private IAddViewModel Create()
        {
            return new AddRecordViewModel();
        }

        public IAddRecordView CreateView()
        {
            IAddRecordView window = new AddRecordView(Create() as AddRecordViewModel);
            return window;
        }
    }
}
