using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayRecordsModule.Interfaces.Views;

namespace Common
{
    public interface IWindowService
    {
        void ShowWindow(IAddRecordView view);
    }
}
