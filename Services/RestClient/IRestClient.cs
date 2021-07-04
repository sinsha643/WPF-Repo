using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Services.RestClient
{
    public interface IRestClient
    {
        Task<string> GetAsync(RestParams restParams);
        Task<string> PostAsync(RestParams restParams);
    }
}
