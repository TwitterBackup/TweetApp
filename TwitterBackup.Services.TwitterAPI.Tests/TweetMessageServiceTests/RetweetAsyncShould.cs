using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI.Tests.TweetApiServiceTests
{
    [TestClass]
    public class RetweetAsyncShould
    {
        [TestMethod]
        public async Task Return_Correct_Result_When_Called_With_Valid_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var expected = new RetweetResultDto();
            jsonProviderMock.Setup(x => x.DeserializeObject<RetweetResultDto>(It.IsAny<string>())).Returns(expected);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "1234567890";

            var actual = await TweetApiService.RetweetAsync(tweetId);

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Null_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.RetweetAsync(null));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Empty_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.RetweetAsync(string.Empty));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_White_Space_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.RetweetAsync("       "));
        }

        [TestMethod]
        public async Task Call_ApiClient_PostAsync_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "1234567890";

            var _ = await TweetApiService.RetweetAsync(tweetId);

            apiClientMock.Verify(x => x.PostAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()), Times.Once());
        }

        [TestMethod]
        public async Task Call_ApiClient_PostAsync_With_IAuthenticator_Passed()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "1234567890";

            var _ = await TweetApiService.RetweetAsync(tweetId);

            apiClientMock.Verify(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), authMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "1234567890";

            var _ = await TweetApiService.RetweetAsync(tweetId);

            jsonProviderMock.Verify(x => x.DeserializeObject<RetweetResultDto>(It.IsAny<string>()), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_With_response_Content()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            var responseContent = "Test content";

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);
            responseMock.SetupGet(x => x.Content).Returns(responseContent);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "1234567890";

            var _ = await TweetApiService.RetweetAsync(tweetId);

            jsonProviderMock.Verify(x => x.DeserializeObject<RetweetResultDto>(responseContent), Times.Once());
        }

        [TestMethod]
        public async Task Return_Null_When_response_Status_Code_Is_Not_Ok()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            var statusCode = HttpStatusCode.NotFound;
            responseMock.SetupGet(x => x.StatusCode).Returns(statusCode);

            apiClientMock.Setup(x => x.PostAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweetId = "123456790";

            var result = await TweetApiService.RetweetAsync(tweetId);

            Assert.IsNull(result);
        }
    }
}
