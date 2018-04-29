using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI.Tests.TweetMessageServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void Create_TweeterService_When_Called_With_Valid_Parameters()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            var tweetMessageService = new TweetMessageService(apiClientMock.Object, authMock.Object, jsonProviderMock.Object);

            Assert.IsNotNull(tweetMessageService);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IApiClient()
        {
            var authMock = new Mock<ITwitterAuthenticator>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(null, authMock.Object, jsonProviderMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITwitterAuthenticator()
        {
            var apiClientMock = new Mock<IApiClient>();
            var jsonProviderMock = new Mock<IJsonProvider>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(apiClientMock.Object, null, jsonProviderMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IJsonProvider()
        {
            var apiClientMock = new Mock<IApiClient>();
            var authMock = new Mock<ITwitterAuthenticator>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(apiClientMock.Object, authMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_All_Null_Parameters()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                  new TweetMessageService(null, null, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_ITwitterAuthenticator_And_IJsonProvider()
        {
            var apiClientMock = new Mock<IApiClient>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(apiClientMock.Object, null, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IApiClient_And_IJsonProvider()
        {
            var authMock = new Mock<ITwitterAuthenticator>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(null, authMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IApiClient_And_ITwitterAuthenticator()
        {
            var jsonProviderMock = new Mock<IJsonProvider>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new TweetMessageService(null, null, jsonProviderMock.Object));
        }
    }
}
