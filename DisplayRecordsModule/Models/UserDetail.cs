using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DisplayRecordsModule.Models
{
    public class UserDetail : BaseNotificationObject
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _role;
        private string _location;

        private bool _isActive;

        #region Properties
        public string UserId
        {
            get => _id;
            set => CompareSetAndNotify(ref _id, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => CompareSetAndNotify(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => CompareSetAndNotify(ref _lastName, value);
        }

        public string Role
        {
            get => _role;
            set => CompareSetAndNotify(ref _role, value);
        }

        public string Location
        {
            get => _location;
            set => CompareSetAndNotify(ref _location, value);
        }

        public bool IsActive
        {
            get => _isActive;
            set => CompareSetAndNotify(ref _isActive, value);
        }

        #endregion
    }
}
