using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using DisplayRecordsModule.Models;
using Microsoft.Practices.Prism.Commands;

namespace DisplayRecordsModule.ViewModels
{
    public class AddRecordViewModel : BaseNotificationObject, IAddViewModel
    {
        private UserDetail _data;

        public AddRecordViewModel()
        {
            UserData = new UserDetail();
            SaveCommand = new DelegateCommand(Save, () => true);
        }

        public UserDetail UserData
        {
            get => _data;
            set => CompareSetAndNotify(ref _data, value);
        }

        #region Commands
        public ICommand SaveCommand { get; }
        #endregion

        private void Save()
        {
            try
            {
               //call service to save
            }
            catch (Exception ex)
            {
            }
        }

    }
}
