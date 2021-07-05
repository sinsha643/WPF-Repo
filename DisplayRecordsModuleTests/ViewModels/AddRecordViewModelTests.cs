using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Models;
using DisplayRecordsModule.Services;
using DisplayRecordsModule.ViewModels;
using log4net;
using Microsoft.Practices.Prism.Events;
using Rhino.Mocks;
using Assert = NUnit.Framework.Assert;


namespace DisplayRecordsModuleTests.ViewModels
{
    [TestClass]
    public class AddRecordViewModelTests
    {
        private readonly AddRecordViewModel _addRecordsVm;

        public AddRecordViewModelTests()
        {
            var log = MockRepository.GenerateStub<ILog>();
            var service = MockRepository.GenerateStub<IDisplayModuleService>();
            var clientService = MockRepository.GenerateStub<IUserDetailCallbackClientService>();

            service.Stub(mock => mock.SaveUserAsync(Arg<UserDetail>.Is.Anything)).Return(Task.FromResult(GetUser()));
            _addRecordsVm = new AddRecordViewModel(service, log, clientService);
        }

        [TestMethod]
        public void UserDetail_OnSaveCommandExecute_IsSetCorrectly()
        {
            //Act
            _addRecordsVm.SaveCommand.Execute(null);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(_addRecordsVm.UserData);
                Assert.AreEqual("S1", _addRecordsVm.UserData.UserId);
                Assert.AreEqual("Alex", _addRecordsVm.UserData.FirstName);
                Assert.AreEqual("Philip", _addRecordsVm.UserData.LastName);
                Assert.AreEqual("LONDON", _addRecordsVm.UserData.Location);
                Assert.AreEqual("Read", _addRecordsVm.UserData.Role);
                Assert.IsTrue(_addRecordsVm.UserData.IsActive);
            });
        }


        private static UserDetail GetUser()
        {
            return new UserDetail
            {
                UserId = "S1",
                FirstName = "Alex",
                LastName = "Philip",
                Role = "Read",
                Location = "LONDON",
                IsActive = true
            };
        }
    }
}
