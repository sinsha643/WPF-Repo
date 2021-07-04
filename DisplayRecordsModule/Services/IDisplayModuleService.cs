using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DisplayRecordsModule.Models;

namespace DisplayRecordsModule.Services
{
    public interface IDisplayModuleService
    {
        Task<List<UserDetail>> GetUserDetailsAsync();
        Task<UserDetail> SaveUserAsync(UserDetail userDetail);
    }
}
