using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Models;
using DisplayRecordsModule.Services;
using log4net;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;

namespace DisplayRecordsModule.ViewModels
{
    public class DisplayRecordsViewModel : BaseNotificationObject
    {
        private ObservableCollection<UserDetail> _userDetails;
        private bool _isBusy;
        private readonly IWindowService _windowService;
        private readonly IAddViewModelFactory _addViewModelFactory;
        private readonly IDisplayModuleService _displayModuleService;
        private readonly SerialDisposable _disposable = new SerialDisposable();

        private readonly ILog _log;
        public DisplayRecordsViewModel(IWindowService windowService, 
                                       IAddViewModelFactory addViewModelFactory,
                                       IDisplayModuleService displayModuleService,
                                       ILog log)
        {
            _windowService = windowService;
            _addViewModelFactory = addViewModelFactory;
            _displayModuleService = displayModuleService;
            _log = log;
            SearchCommand = new DelegateCommand(Search, () => !IsBusy);
            AddCommand = new DelegateCommand(Add, () => true);
            Search();
        }

        public ObservableCollection<UserDetail> UserDetails
        {
            get => _userDetails;
            set => CompareSetAndNotify(ref _userDetails, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => CompareSetAndNotify(ref _isBusy, value);
        }

        #region Commands
        public ICommand SearchCommand { get; }
        public ICommand AddCommand { get; }
        #endregion

        #region Methods

        public void Add()
        {
            _windowService.ShowWindow(_addViewModelFactory.CreateView());
        }

        public void Search()
        {
            try
            {
                IsBusy = true;

                //GET service call
                _disposable.Disposable = Observable.FromAsync(async () =>
                        await _displayModuleService.GetUserDetailsAsync())
                    .Subscribe(s =>
                    {
                        UserDetails = new ObservableCollection<UserDetail>(s);
                        IsBusy = false;
                    }, LostServerConnection);

            }
            catch (Exception ex)
            {
                LostServerConnection(ex);
            }
        }

        private void LostServerConnection(Exception ex)
        {
            IsBusy = false;
            _log.Info("lost server connection");
        }
        #endregion

    }
}
