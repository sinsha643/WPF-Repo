using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using DisplayRecordsModule.Models;
using DisplayRecordsModule.Services;
using log4net;
using Microsoft.Practices.Prism.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Services;
using Services.RestClient;
using Assert = NUnit.Framework.Assert;

namespace DisplayRecordsModuleTests.Services
{
    [TestClass]
    public class DisplayModuleServiceTest
    {
        private readonly IHttpClientAdapter _httpClientAdapter;
        private readonly DisplayModuleService _dataService;

        public DisplayModuleServiceTest()
        {
            var log = MockRepository.GenerateStub<ILog>();
            _httpClientAdapter = MockRepository.GenerateStub<IHttpClientAdapter>();
            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            IRestClient restClient = new RestClientImpl(log, eventAggregator, _httpClientAdapter);
            _dataService = new DisplayModuleService(log, restClient);
        }

        [TestMethod]
        public void GetUserDetailsAsync_WhenCalled_GivesExpectedOutput()
        {
            //Arrange
            var allUsers = JsonResponseForGet();
            var resp = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(allUsers) };
            var vRequest = new HttpRequestMessage(HttpMethod.Get, "User");
            vRequest.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            _httpClientAdapter.Stub(mock => mock.SendAsync(vRequest)).IgnoreArguments()
                .Return(Task.FromResult(resp));
            //Act
            var response = _dataService.GetUserDetailsAsync().Result;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(response);
                Assert.AreEqual(2, response.Count);
                Assert.IsNotNull(response[0].FirstName);
                Assert.IsNotNull(response[0].LastName);
                Assert.IsNotNull(response[1].FirstName);
                Assert.IsNotNull(response[1].LastName);
                //add all properties here
            });
        }

        [TestMethod]
        public void SaveUserDetail_WhenCalled_ReturnExpectedResult()
        {
            //Arrange
            string saveExtension = EnumDescriptionSelector.EnumDescription(UrlExtensions.AllRecords);
            UserDetail userDetail = new UserDetail()
            { UserId = "1", FirstName = "Tom", LastName = "Cruise", IsActive = true, Location = "New York" };
            string savedUser = JsonResponseForSave();
            var resp = new HttpResponseMessage()
            { StatusCode = System.Net.HttpStatusCode.OK, Content = new StringContent(savedUser) };
            var postContent = new StringContent(savedUser, Encoding.UTF8, "application/json");
            _httpClientAdapter.Stub(mock => mock.PostAsync(saveExtension, postContent)).IgnoreArguments()
                .Return(Task.FromResult(resp));

            //Act
            var response = _dataService.SaveUserAsync(userDetail).Result;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Tom", response.FirstName);
                Assert.AreEqual("Cruise", response.LastName);
                Assert.AreEqual("1", response.UserId);
                Assert.AreEqual("New York", response.Location);
                Assert.IsTrue(response.IsActive);
            });
        }

        private static string JsonResponseForGet()
        {
            return "[{\"userId\": \"1\",\"firstName\": \"Tom\",\"lastName\": \"Cruise\",\"role\": \"Actor\",\"location\": \"New York\",\"isActive\": true},{\"userId\": \"2\",\"firstName\": \"Chris\",\"lastName\": \"Pratt\",\"role\": \"Actor\",\"location\": \"Virginia\",\"isActive\": true}]";
        }

        private static string JsonResponseForSave()
        {
            return "{\"userId\": \"1\",\"firstName\": \"Tom\",\"lastName\": \"Cruise\",\"role\": \"Actor\",\"location\": \"New York\",\"isActive\": true}";
        }

    }
}
