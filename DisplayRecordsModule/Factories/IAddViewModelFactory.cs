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
    public interface IAddViewModelFactory
    {
        IAddRecordView CreateView();
    }
}
