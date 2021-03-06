﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
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
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var expected = new Mock<TweeterDto>();
            jsonProviderMock.Setup(x => x.DeserializeObject<TweeterDto>(It.IsAny<string>())).Returns(expected.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var actual = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            Assert.AreSame(expected.Object, actual);
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Null_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.GetTweeterByScreenNameAsync(null));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Empty_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.GetTweeterByScreenNameAsync(string.Empty));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_White_Space_String_Parameter()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await tweeterService.GetTweeterByScreenNameAsync("       "));
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
            var responseMock = new Mock<IRestResponse>();

            responseMock.SetupGet(x => x.StatusCode).Returns(HttpStatusCode.OK);

            apiClientMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IAuthenticator>()))
                .ReturnsAsync(responseMock.Object);

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

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

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            jsonProviderMock.Verify(x => x.DeserializeObject<TweeterDto>(It.IsAny<string>()), Times.Once());
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

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var _ = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            jsonProviderMock.Verify(x => x.DeserializeObject<TweeterDto>(responseContent), Times.Once());
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

            var tweeterService = new TweeterApiService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            var screenName = "screen_name";

            var result = await tweeterService.GetTweeterByScreenNameAsync(screenName);

            Assert.IsNull(result);
        }
    }
}
