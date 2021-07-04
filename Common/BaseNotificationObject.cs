using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BaseNotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region CompareSetAndNotify

        /// <summary>
        /// CompareSetAndNotify
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        protected bool CompareSetAndNotify<T>(ref T field, T value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            field = value;
            NotifyPropertyChangedSpecific();
            return true;
        }

        #endregion

        #region Raising property change events

        /// <summary>
        /// Notify Property Changed Specific
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChangedSpecific(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            OnPropertyChanged(propertyName);
        }

        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        { }

    }
}
