using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DisplayRecordsModule.Interfaces.Views;

namespace Common
{
    public class WindowService : IWindowService
    {
        public void ShowWindow(IAddRecordView view)
        {
            Window window = view as Window;
            window?.Show();
        }
    }
}
