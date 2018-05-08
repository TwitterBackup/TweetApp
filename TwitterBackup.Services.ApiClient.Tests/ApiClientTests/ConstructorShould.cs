using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using System;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient.Tests.ApiClientTests
{
    [TestClass]
    public class ConstructorShould
    {
        [TestMethod]
        public void Create_ApiClient_When_Called_With_Valid_Parameters()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            Assert.IsNotNull(apiClient);
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IRestClient()
        {
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(null, requestMock.Object, uriFactoryMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IRestRequest()
        {
            var clientMock = new Mock<IRestClient>();
            var uriFactoryMock = new Mock<IUriFactory>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(clientMock.Object, null, uriFactoryMock.Object));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IUriFactory()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(clientMock.Object, requestMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_All_Null_Parameters()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                  new ApiClient(null, null, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IRestRequest_And_IUriFactory()
        {
            var clientMock = new Mock<IRestClient>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(clientMock.Object, null, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IRestClient_And_IUriFactory()
        {
            var requestMock = new Mock<IRestRequest>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(null, requestMock.Object, null));
        }

        [TestMethod]
        public void Throws_ArgumentNullException_When_Called_With_Null_IRestClient_And_IRestRequest()
        {
            var uriFactoryMock = new Mock<IUriFactory>();

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ApiClient(null, null, uriFactoryMock.Object));
        }
    }
}
