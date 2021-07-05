using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayRecordsModule.UserDetailsWcfService;

namespace DisplayRecordsModule.Services
{
    public interface IUserDetailCallbackClientService : IUserDetailServiceCallback
    {
        IObservable<Models.UserDetail> GetUserDetailStream { get; }
        ObservableCollection<Models.UserDetail> GetAllUsers();
        Models.UserDetail SaveUser(Models.UserDetail userEntity);
    }
}
