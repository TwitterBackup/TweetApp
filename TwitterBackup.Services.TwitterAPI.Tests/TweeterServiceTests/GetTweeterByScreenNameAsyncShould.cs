using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI.Tests.TweeterServiceTests
{
    [TestClass]
    public class GetTweeterByScreenNameAsyncShould
    {
        [TestMethod]
        public async Task Return_Correct_Result_When_Called_With_Valid_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var expected = new Mock<GetTweeterDto>();
            jsonProviderMock.Setup(x => x.DeserializeObject<GetTweeterDto>(It.IsAny<string>())).Returns(expected.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var actual = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            Assert.AreSame(expected.Object, actual);
        }

        [TestMethod]
        public async Task Call_ApiClient_GetAsync_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            apiClientMock.Verify(x => x.GetAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()), Times.Once());
        }

        [TestMethod]
        public async Task Call_ApiClient_GetAsync_With_IAuthenticator_Passed()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            apiClientMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), authMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_With_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            jsonProviderMock.Verify(x => x.DeserializeObject<GetTweeterDto>(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_With_Responce_Content()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            var responceContent = "Test content";

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);
            responceMock.SetupGet(x => x.Content).Returns(responceContent);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            jsonProviderMock.Verify(x => x.DeserializeObject<GetTweeterDto>(responceContent), Times.Once());
        }

        [TestMethod]
        public async Task Return_Null_When_Responce_Status_Code_Is_Not_Ok()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            var statusCode = HttpStatusCode.NotFound;
            responceMock.SetupGet(x => x.StatusCode).Returns(statusCode);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var result = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            Assert.IsNull(result);
        }
    }
}
