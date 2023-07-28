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
    public class ExceptionalTests
    {
        private readonly ITestOutputHelper _output;
        private readonly PoliticalPartiesDbContext _politicalPartiesDbContext;

        private readonly IPoliticalPartyServices _politicalPartyServices;
        

        public readonly Mock<IPoliticalPartyRepository> politicalPartyservice = new Mock<IPoliticalPartyRepository>();
       

        private PoliticalParty _politicalParty;
       

        private readonly RegisterPoliticalPartyViewModel _registerPoliticalPartyViewModel;
       

        private static string type = "Exception";

        public ExceptionalTests(ITestOutputHelper output)
        {
            _politicalPartyServices = new PoliticalPartyServices(politicalPartyservice.Object,_politicalPartiesDbContext);
           
            _output = output;

            _politicalParty = new PoliticalParty
            {
                PoliticalPartyId = 1,
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

        /// <summary>
        /// Test to validate if political party id must be greater then 0 charactor
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task<bool> Testfor_Validate_ifInvalidPoliticalPartyIdIsPassed()
        {
            //Arrange
            bool res = false;
            var politicalPartyId = 0;
            string testName; string status;
            testName = CallAPI.GetCurrentMethodName();
            //Act
            try
            {
                politicalPartyservice.Setup(repo => repo.GetById(politicalPartyId)).ReturnsAsync(_politicalParty);
                var result = await _politicalPartyServices.GetById(politicalPartyId);
                if (result != null || result.PoliticalPartyId > 0)
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

    }
}
