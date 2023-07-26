using Moq;
using PoliticalParties.BusinessLayer.Interfaces;
using PoliticalParties.BusinessLayer.Services;
using PoliticalParties.BusinessLayer.Services.Repository;
using PoliticalParties.BusinessLayer.ViewModels;
using PoliticalParties.DataLayer;
using PoliticalParties.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PoliticalParties.Tests.TestCases
{
    public class FunctionalTests
    {
        private readonly ITestOutputHelper _output;
        private readonly PoliticalPartiesDbContext _politicalPartiesDbContext;

        private readonly IPoliticalPartyServices _politicalPartyServices;
      

        public readonly Mock<IPoliticalPartyRepository> politicalPartyservice = new Mock<IPoliticalPartyRepository>();
        

        private PoliticalParty _politicalParty;
      

        private readonly RegisterPoliticalPartyViewModel _registerPoliticalPartyViewModel;
       

        private static string type = "Functional";

        public FunctionalTests(ITestOutputHelper output)
        {
            _politicalPartyServices = new PoliticalPartyServices(politicalPartyservice.Object,_politicalPartiesDbContext);
           
            _output = output;

            _politicalParty = new PoliticalParty
            {
                PoliticalPartyId = 9,
                Name = "Party1",
                Founder = "abc",
                IsDeleted = false
            };
           

            _registerPoliticalPartyViewModel = new RegisterPoliticalPartyViewModel
            {
                PoliticalPartyId = 1,
                Name = "Party1",
                Founder = "abc",
                IsDeleted = false
            };
          
        }

        #region PoliticalParty
        /// <summary>
        /// Test to register new PoliticalParty
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Register_PoliticalParty()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                politicalPartyservice.Setup(repos => repos.Create(_politicalParty)).ReturnsAsync(_politicalParty);
                var result = await _politicalPartyServices.Create(_politicalParty);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

        /// <summary>
        /// Using the below test method Update Political Party by using Political Party Id.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Update_PoliticalParty()
        {
            //Arrange
            bool res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            var _updatePoliticalParty = new RegisterPoliticalPartyViewModel()
            {
                PoliticalPartyId = 1,
                Name = "product1",
                IsDeleted = false
            };
            //Act
            try
            {
                politicalPartyservice.Setup(repos => repos.Update(_updatePoliticalParty)).ReturnsAsync(_politicalParty);
                var result = await _politicalPartyServices.Update(_updatePoliticalParty);
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");

            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }


        /// <summary>
        /// Test to list all PoliticalParty 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_ListAll_PoliticalParties()
        {
            //Arrange
            var res = false;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                politicalPartyservice.Setup(repos => repos.GetAll());
                var result = await _politicalPartyServices.GetAll();
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }

        /// <summary>
        /// Test to find Political Party by Political Party Id
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_FindPoliticalPartyById()
        {
            //Arrange
            var res = false;
            int politicalPartyId = 1;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Action
            try
            {
                politicalPartyservice.Setup(repos => repos.GetById(politicalPartyId)).ReturnsAsync(_politicalParty); ;
                var result = await _politicalPartyServices.GetById(politicalPartyId);
                //Assertion
                if (result != null)
                {
                    res = true;
                }
            }
            catch (Exception)
            {
                //Assert
                //final result save in text file if exception raised
                status = Convert.ToString(res);
                _output.WriteLine(testName + ":Failed");
                await CallAPI.saveTestResult(testName, status, type);
                return false;
            }
            //final result save in text file, Call rest API to save test result
            status = Convert.ToString(res);
            if (res == true)
            {
                _output.WriteLine(testName + ":Passed");
            }
            else
            {
                _output.WriteLine(testName + ":Failed");
            }
            await CallAPI.saveTestResult(testName, status, type);
            return res;
        }
        #endregion

      


    }
}


