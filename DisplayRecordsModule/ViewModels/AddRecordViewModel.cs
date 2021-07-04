using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using DisplayRecordsModule.Models;
using DisplayRecordsModule.Services;
using log4net;
using Microsoft.Practices.Prism.Commands;

namespace DisplayRecordsModule.ViewModels
{
    public class AddRecordViewModel : BaseNotificationObject, IAddViewModel
    {
        private UserDetail _data;
        private readonly SerialDisposable _disposable = new SerialDisposable();
        private readonly IDisplayModuleService _displayModuleService;
        private readonly ILog _log;


        public AddRecordViewModel(IDisplayModuleService displayModuleService, ILog log)
        {
            _displayModuleService = displayModuleService;
            _log = log;
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

        public void Save()
        {
            try
            {
                _disposable.Disposable = Observable.FromAsync(async () =>
                        await _displayModuleService.SaveUserAsync(UserData))
                    .Subscribe(userInfo =>
                    {
                        UserData = userInfo;
                    });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

    }
}
