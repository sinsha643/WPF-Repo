using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DisplayRecordsModule.Factories;
using DisplayRecordsModule.Interfaces.Views;
using DisplayRecordsModule.Models;
using DisplayRecordsModule.Services;
using DisplayRecordsModule.ViewModels;
using log4net;
using Microsoft.Practices.Prism.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Services;
using Services.RestClient;

namespace DisplayRecordsModuleTests.ViewModels
{
    [TestClass]
    public class DisplayRecordsViewModelTest
    {
        private readonly DisplayRecordsViewModel _displayRecordsVm;
        private readonly IWindowService _windowService;

        public DisplayRecordsViewModelTest()
        {
            _windowService = MockRepository.GenerateStub<IWindowService>();
            var log = MockRepository.GenerateStub<ILog>();
            var factory = MockRepository.GenerateStub<IAddViewModelFactory>();
            var service = MockRepository.GenerateStub<IDisplayModuleService>();
            service.Stub(mock => mock.GetUserDetailsAsync()).Return(Task.FromResult(GetUsers()));
            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();
            _displayRecordsVm = new DisplayRecordsViewModel(_windowService, factory, service, log);
        }

        [TestMethod]
        public void ShowDialog_OnAddCommandExecute_IsCalled()
        {
            //Act
            _displayRecordsVm.AddCommand.Execute(null);

            //Assert
            _windowService.AssertWasCalled(mock => mock.ShowWindow(Arg<IAddRecordView>.Is.Anything));
        }


        [TestMethod]
        public void Search_OnLoad_IsCalledAndExecuted()
        {
            //Assert
            Assert.AreEqual(4, _displayRecordsVm.UserDetails.Count);
        }


        [TestMethod]
        public void SearchCommand_CanExecuteWhenBusyIsTrue_IsSetFalse()
        {
            //Act
            _displayRecordsVm.IsBusy = true;

            //Assert
            Assert.IsFalse(_displayRecordsVm.SearchCommand.CanExecute(null));
        }

        [TestMethod]
        public void SearchCommand_CanExecuteWhenBusyIsFalse_IsSetTrue()
        {
            //Act
            _displayRecordsVm.IsBusy = false;

            //Assert
            Assert.IsTrue(_displayRecordsVm.SearchCommand.CanExecute(null));
        }

        [TestMethod]
        public void UserDetails_OnSearchExecute_IsSetCorrectly()
        {
            //Act
            _displayRecordsVm.SearchCommand.Execute(null);

            //Assert
            Assert.AreEqual(4, _displayRecordsVm.UserDetails.Count);
        }

        [TestMethod]
        public void IsBusy_AfterSearchExecute_IsSetToFalse()
        {
            //Arrange
            _displayRecordsVm.IsBusy = true;
            //Act
            _displayRecordsVm.SearchCommand.Execute(null);

            //Assert
            Assert.IsFalse(_displayRecordsVm.IsBusy);
        }

        private List<UserDetail> GetUsers()
        {
            var listOfData = new List<UserDetail>();
            listOfData.Add(new UserDetail { UserId = "S1", FirstName = "Alex", LastName = "Philip", Role = "Read", Location = "LONDON", IsActive = true });
            listOfData.Add(new UserDetail { UserId = "S2", FirstName = "Linda", LastName = "Al", Role = "Write", Location = "CHICAGO", IsActive = true });
            listOfData.Add(new UserDetail { UserId = "S3", FirstName = "Joy", LastName = "Phil", Role = "Admin", Location = "SINGAPORE", IsActive = false });
            listOfData.Add(new UserDetail { UserId = "S4", FirstName = "Rachel", LastName = "Roy", Role = "Read", Location = "LONDON", IsActive = true });

            return listOfData;
        }
    }
}
