using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common;
using DisplayRecordsModule.Models;
using Microsoft.Practices.Prism.Commands;

namespace DisplayRecordsModule.ViewModels
{
    public class DisplayRecordsViewModel : BaseNotificationObject
    {
        protected ObservableCollection<UserDetail> _items;
        private bool _isBusy;

        public DisplayRecordsViewModel()
        {
            SearchCommand = new DelegateCommand(Search, () => !IsBusy);
            Search();
        }

        public ObservableCollection<UserDetail> Items
        {
            get => _items;
            set => CompareSetAndNotify(ref _items, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => CompareSetAndNotify(ref _isBusy, value);
        }

        #region Commands
        public ICommand SearchCommand { get; }
        #endregion

        public void Search()
        {
            try
            {
                IsBusy = true;

                //dummy data
                var listOfData = new ObservableCollection<UserDetail>();
                listOfData.Add(new UserDetail { UserId = "S1", FirstName = "Alex", LastName = "Philip", Role = "Read", Location = "LONDON", IsActive = true });
                listOfData.Add(new UserDetail { UserId = "S2", FirstName = "Linda", LastName = "Al", Role = "Write", Location = "CHICAGO", IsActive = true });
                listOfData.Add(new UserDetail { UserId = "S3", FirstName = "Joy", LastName = "Phil", Role = "Admin", Location = "SINGAPORE", IsActive = false });
                listOfData.Add(new UserDetail { UserId = "S4", FirstName = "Rachel", LastName = "Roy", Role = "Read", Location = "LONDON", IsActive = true });

                Items = listOfData;
            }
            catch (Exception ex)
            {
                LostServerConnection(ex);
            }
        }

        private void LostServerConnection(Exception ex)
        {
            IsBusy = false;
        }
    }
}
