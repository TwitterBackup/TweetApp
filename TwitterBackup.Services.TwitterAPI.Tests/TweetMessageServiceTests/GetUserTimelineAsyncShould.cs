using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI.Tests.TweetMessageServiceTests
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
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var expected = new List<TweetFromTwitterDto>() { new TweetFromTwitterDto() };
            jsonProviderMock.Setup(x => x.DeserializeObject<IEnumerable<TweetFromTwitterDto>>(It.IsAny<string>())).Returns(expected);

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var actual = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public async Task Return_Null_When__Responce_Content_Is_Empty()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var responceList = new List<TweetFromTwitterDto>();
            jsonProviderMock.Setup(x => x.DeserializeObject<IEnumerable<TweetFromTwitterDto>>(It.IsAny<string>())).Returns(responceList);

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var actual = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            Assert.AreSame(null, actual);
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

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await tweetMessageService.GetUserTimelineAsync(tweeterName);

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

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            apiClientMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), authMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task JsonProvider_DeserializeObject_Once()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();
            var responceMock = new Mock<IRestResponse>();

            responceMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responceMock.Object);

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var _ = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            jsonProviderMock.Verify(x => x.DeserializeObject<IEnumerable<TweetFromTwitterDto>>(It.IsAny<string>()), Times.Once());
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

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweet_name";

            var _ = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            jsonProviderMock.Verify(x => x.DeserializeObject<IEnumerable<TweetFromTwitterDto>>(responceContent), Times.Once());
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

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var tweeterName = "tweeter_name";

            var result = await tweetMessageService.GetUserTimelineAsync(tweeterName);

            Assert.IsNull(result);
        }
    }
}
