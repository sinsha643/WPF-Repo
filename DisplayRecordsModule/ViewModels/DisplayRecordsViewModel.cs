using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Services;
using log4net;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.ServiceModel;
using System.Windows.Input;
using UserDetail = DisplayRecordsModule.Models.UserDetail;

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
        private readonly IUserDetailCallbackClientService _userDetailCallbackClientService;
        private readonly ILog _log;
        private readonly CompositeDisposable _subscriptions;
        private readonly ISchedulerProvider _schedulerProvider;


        public DisplayRecordsViewModel(IWindowService windowService,
                                       IAddViewModelFactory addViewModelFactory,
                                       IDisplayModuleService displayModuleService,
                                       ILog log,
                                       IUserDetailCallbackClientService userDetailCallbackClientService,
                                       ISchedulerProvider scheduleProvider)
        {
            _windowService = windowService;
            _addViewModelFactory = addViewModelFactory;
            _displayModuleService = displayModuleService;
            _log = log;
            _userDetailCallbackClientService = userDetailCallbackClientService;
            _schedulerProvider = scheduleProvider;

            _subscriptions = new CompositeDisposable();
            SearchCommand = new DelegateCommand(Search, () => !IsBusy);
            AddCommand = new DelegateCommand(Add, () => true);

            //subscribes to notify method and updates UserDetails accordingly
            _userDetailCallbackClientService.GetUserDetailStream?
                .ObserveOn(_schedulerProvider.GetUi())
                .Subscribe(AddUser)
                .AddToDisposables(_subscriptions);

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

                UserDetails = _userDetailCallbackClientService.GetAllUsers();

                //GET service call
                //_disposable.Disposable = Observable.FromAsync(async () =>
                //        await _displayModuleService.GetUserDetailsAsync())
                //    .Subscribe(s =>
                //    {
                //        UserDetails = new ObservableCollection<UserDetail>(s);
                //        IsBusy = false;
                //    }, LostServerConnection);

                //UserDetailService.UserDetailServiceClient client = new UserDetailServiceClient(_context);
                //UserDetails = client.GetAllUsersAsync();

            }
            catch (Exception ex)
            {
                LostServerConnection(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void AddUser(Models.UserDetail userModel)
        {
            UserDetails.Add(userModel);
            NotifyPropertyChangedSpecific(nameof(UserDetails));
        }
        private void LostServerConnection(Exception ex)
        {
            IsBusy = false;
            _log.Info("lost server connection");
        }
        #endregion
    }
}
