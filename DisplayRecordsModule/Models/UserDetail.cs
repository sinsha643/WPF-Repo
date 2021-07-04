using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace DisplayRecordsModule.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserDetail : BaseNotificationObject
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private string _role;
        private string _location;

        private bool _isActive;

        #region Properties
        [JsonProperty("UserId")]
        public string UserId
        {
            get => _id;
            set => CompareSetAndNotify(ref _id, value);
        }

        [JsonProperty("FirstName")]
        [MaxLength(255, ErrorMessage = "First Name cannot be longer than 255 characters")]
        public string FirstName
        {
            get => _firstName;
            set => CompareSetAndNotify(ref _firstName, value);
        }

        [JsonProperty("LastName")]
        public string LastName
        {
            get => _lastName;
            set => CompareSetAndNotify(ref _lastName, value);
        }

        [JsonProperty("Role")]
        public string Role
        {
            get => _role;
            set => CompareSetAndNotify(ref _role, value);
        }

        [JsonProperty("Location")]
        public string Location
        {
            get => _location;
            set => CompareSetAndNotify(ref _location, value);
        }

        [JsonProperty("IsActive")]
        public bool IsActive
        {
            get => _isActive;
            set => CompareSetAndNotify(ref _isActive, value);
        }

        #endregion
    }
}
