using DisplayRecordsModule.UserDetailsWcfService;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using System.ServiceModel;

namespace DisplayRecordsModule.Services
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    public class UserDetailCallbackClientService : IUserDetailCallbackClientService
    {
        private readonly UserDetailServiceClient _client;
        private readonly Subject<Models.UserDetail> _notifyUser = new Subject<Models.UserDetail>();

        public UserDetailCallbackClientService()
        {
            var context = new InstanceContext(this);
            _client = new UserDetailServiceClient(context);
        }

        public IObservable<Models.UserDetail> GetUserDetailStream => _notifyUser;

        public ObservableCollection<Models.UserDetail> GetAllUsers()
        {
            var usersEntity = _client.GetAllUsers();
            var users = new ObservableCollection<Models.UserDetail>();
            foreach (var userEntity in usersEntity)
            {
                var userDetail = new Models.UserDetail
                {
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    IsActive = userEntity.IsActive,
                    UserId = userEntity.UserId,
                    Location = userEntity.Location,
                    Role = userEntity.Role
                };
                users.Add(userDetail);
            }
            return users;
        }

        public Models.UserDetail SaveUser(Models.UserDetail userModel)
        {
            var userEntity= new UserDetail()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                IsActive = userModel.IsActive,
                UserId = userModel.UserId,
                Location = userModel.Location,
                Role = userModel.Role
            };
            _client.Create(userEntity);
            return userModel;
        }

        public void Notify(UserDetail userEntity)
        {
            var userModel = GetUserDetailModel(userEntity);
            //notifies the observer
            _notifyUser.OnNext(userModel);
        }

        private Models.UserDetail GetUserDetailModel(UserDetail userEntity)
        {
            var userModel = new Models.UserDetail
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                IsActive = userEntity.IsActive,
                UserId = userEntity.UserId,
                Location = userEntity.Location,
                Role = userEntity.Role
            };
            return userModel;
        }
    }
}
