using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum UrlExtensions
    {
        /// <summary>
        /// UserDetails url extensions
        /// </summary>

        #region UserDetails url extensions

        [Description("User/GetAll")] AllRecords,

        [Description("User/Create")] CreateRecord,
        #endregion

    }
}
