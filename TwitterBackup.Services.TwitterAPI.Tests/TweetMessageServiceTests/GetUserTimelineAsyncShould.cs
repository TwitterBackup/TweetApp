using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI.Tests.TweetApiServiceTests
{
    [TestClass]
    public class GetUserTimelineAsyncShould
    {
        [TestMethod]
        public async Task Return_Correct_Result_When_Called_With_Valid_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var expected = new List<ApiTweetDto>() { new ApiTweetDto() };
            jsonProviderMock.Setup(x => x.DeserializeObject<IEnumerable<ApiTweetDto>>(It.IsAny<string>())).Returns(expected);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var actual = await TweetApiService.GetUserTimelineAsync(tweeterName);

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
                async () => await tweeterService.GetUserTimelineAsync(null));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Empty_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.GetUserTimelineAsync(string.Empty));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_White_Space_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.GetUserTimelineAsync("       "));
        }

        [TestMethod]
        public async Task Return_Null_When__response_Content_Is_Empty()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var responseList = new List<ApiTweetDto>();
            jsonProviderMock.Setup(x => x.DeserializeObject<IEnumerable<ApiTweetDto>>(It.IsAny<string>())).Returns(responseList);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var actual = await TweetApiService.GetUserTimelineAsync(tweeterName);

            Assert.AreSame(null, actual);
        }

        [TestMethod]
        public async Task Call_ApiClient_GetAsync_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await TweetApiService.GetUserTimelineAsync(tweeterName);

            apiClientMock.Verify(x => x.GetAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()), Times.Once());
        }

        [TestMethod]
        public async Task Call_ApiClient_GetAsync_With_IAuthenticator_Passed()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await TweetApiService.GetUserTimelineAsync(tweeterName);

            apiClientMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), authMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await TweetApiService.GetUserTimelineAsync(tweeterName);

            jsonProviderMock.Verify(x => x.DeserializeObject<IEnumerable<ApiTweetDto>>(It.IsAny<string>()), Times.Once());
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

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweet_name";

            var _ = await TweetApiService.GetUserTimelineAsync(tweeterName);

            jsonProviderMock.Verify(x => x.DeserializeObject<IEnumerable<ApiTweetDto>>(responseContent), Times.Once());
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

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var TweetApiService = new TweetApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var result = await TweetApiService.GetUserTimelineAsync(tweeterName);

            Assert.IsNull(result);
        }
    }
}
