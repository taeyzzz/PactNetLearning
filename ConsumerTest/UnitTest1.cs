using System;
using System.Collections.Generic;
using ConsoleApp1;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;

namespace ConsumerTest
{
    public class UnitTest1: IClassFixture<ConsumerPactClassFixture>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public UnitTest1(ConsumerPactClassFixture fixture)
        {
            _mockProviderService = fixture.MockProviderService;
            _mockProviderService.ClearInteractions(); //NOTE: Clears any previously registered interactions before the test is run
            _mockProviderServiceBaseUri = fixture.MockProviderServiceBaseUri;
        }
        
        [Fact]
        public void Test1()
        {
            var validResponse = "{\"data\":[1,2,3,4,5]}";
            _mockProviderService.Given("There is data")
                .UponReceiving("Processing person data")
                .With(new ProviderServiceRequest 
                {
                    Method = HttpVerb.Get,
                    Path = "/Person"
                })
                .WillRespondWith(new ProviderServiceResponse {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new 
                    {
                        data = new List<int>{1,2,3,4,5}
                    }
                });
            
            var result = ConsumerApiClient.ValidateDateTimeUsingProviderApi(_mockProviderServiceBaseUri).GetAwaiter().GetResult();
            var resultContentText = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Assert.Equal(validResponse, resultContentText);
        }
    }
}