using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CloudCustomer.Services.Config;

namespace CloudCustomer.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var HttpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersAPIOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(HttpClient, config);
            // Act
            await sut.GetAllUsers();

            // Assert
            // Verify HTTP request is made!
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
               );

        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalURL()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endpoint = "https://example.com";
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse, endpoint);

            var HttpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UsersAPIOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(HttpClient, config);

            // Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endpoint);
            // Assert
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get
                    && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
               );
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_RetuensListOfUsers()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var HttpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersAPIOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(HttpClient, config);
            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_RetuensEmptyListOfUsers()
        {
            // Arrange
           
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var HttpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersAPIOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(HttpClient, config);
            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_RetuensListOfUsersExpectedSize()
        {
            // Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponse);
            var HttpClient = new HttpClient(handlerMock.Object);
            var endpoint = "https://example.com";
            var config = Options.Create(new UsersAPIOptions
            {
                Endpoint = endpoint
            });
            var sut = new UsersService(HttpClient,config);

            // Act
            var result = await sut.GetAllUsers();

            // Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

 

    }
}
