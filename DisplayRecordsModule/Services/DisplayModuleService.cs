using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums;
using DisplayRecordsModule.Models;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Services.RestClient;

namespace DisplayRecordsModule.Services
{
    public class DisplayModuleService : IDisplayModuleService
    {
        private readonly ILog _log;
        private readonly IRestClient _restClient;
        protected readonly string AllRecords = EnumDescriptionSelector.EnumDescription(UrlExtensions.AllRecords);
        protected readonly string CreateRecord = EnumDescriptionSelector.EnumDescription(UrlExtensions.CreateRecord);

        public DisplayModuleService(ILog log, IRestClient restClient)
        {
            _log = log;
            _restClient = restClient;
        }

        public async Task<List<UserDetail>> GetUserDetailsAsync()
        {
            using (var restParams = new RestParams(_log, AllRecords, string.Empty))
            {
                try
                {
                    var jsonData = await _restClient.GetAsync(restParams);
                    var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
                    var res = JsonConvert.DeserializeObject<List<UserDetail>>(jsonData, settings);

                    return res;
                }
                catch (JsonException ex)
                {
                    _log.Error(ex);
                    return null;
                }
            }
        }

        public async Task<UserDetail> SaveUserAsync(UserDetail userDetail)
        {
            var contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy { OverrideSpecifiedNames = false } };
            //translate model to json content
            var userJson = JsonConvert.SerializeObject(userDetail, new JsonSerializerSettings { ContractResolver = contractResolver, Formatting = Formatting.Indented });

            using (var restParams = new RestParams(_log, CreateRecord, userJson))
            {
                try
                {
                    var jsonData = await _restClient.PostAsync(restParams);
                    var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MissingMemberHandling = MissingMemberHandling.Ignore };
                    var res = JsonConvert.DeserializeObject<UserDetail>(jsonData, settings);

                    return res;
                }
                catch (JsonException ex)
                {
                    _log.Error(ex);
                    return null;
                }
            }
        }
    }
}
