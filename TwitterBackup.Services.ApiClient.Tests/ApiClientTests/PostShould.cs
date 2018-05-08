using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient.Tests.ApiClientTests
{
    [TestClass]
    public class PostShould
    {
        [TestMethod]
        public void Return_IRestResponse_When_Called_With_Valid_Parameters()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var expectedResult = new Mock<IRestResponse>();

            clientMock.Setup(c => c.Execute(It.IsAny<IRestRequest>())).Returns(expectedResult.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var actual = apiClient.Post(fakeUri, fakeResource);

            Assert.AreSame(expectedResult.Object, actual);
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Null_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post(null, fakeResource));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Empty_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post(string.Empty, fakeResource));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_White_Space_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post("   ", fakeResource));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Null_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post(fakeUri, null));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_Empty_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post(fakeUri, string.Empty));
        }

        [TestMethod]
        public void Throw_ArgumentException_When_Called_With_White_Space_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            Assert.ThrowsException<ArgumentException>(() => apiClient.Post(fakeUri, "      "));
        }

        [TestMethod]
        public void Set_Client_BaseUri_Correct_With_Passed_Parramter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var expectedUrl = new Uri(fakeUri);

            uriFactoryMock.Setup(f => f.CreateUri(It.IsAny<string>())).Returns(expectedUrl);

            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            clientMock.VerifySet(x => x.BaseUrl = expectedUrl, Times.Once());
        }

        [TestMethod]
        public void Set_Client_BaseUri_Is_Called_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            clientMock.VerifySet(x => x.BaseUrl = It.IsAny<Uri>(), Times.Once());
        }

        [TestMethod]
        public void Call_Factory_With_Given_BaseUrl_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            uriFactoryMock.Verify(x => x.CreateUri(fakeUri), Times.Once());
        }

        [TestMethod]
        public void Set_Request_Resource_Correct_With_Passed_Parramter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Resource = fakeResource, Times.Once());
        }

        [TestMethod]
        public void Set_Request_Method_To_Post_Method()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Method = Method.POST, Times.Once());
        }

        [TestMethod]
        public void Set_Request_Method_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Method = It.IsAny<Method>(), Times.Once());
        }

        [TestMethod]
        public void Set_Request_Resource_Is_Called_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Resource = It.IsAny<string>(), Times.Once());
        }

        [TestMethod]
        public void Authenticator_Authenticate_Is_Called_When_It_Is_Passed_As_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();
            var authenticator = new Mock<IAuthenticator>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource, authenticator.Object);

            authenticator.Verify(x => x.Authenticate(It.IsAny<IRestClient>(), It.IsAny<IRestRequest>()), Times.Once());
        }

        [TestMethod]
        public void Authenticator_Authenticate_Is_Called_With_Give_Parameters_When_It_Is_Passed_As_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();
            var authenticator = new Mock<IAuthenticator>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource, authenticator.Object);

            authenticator.Verify(x => x.Authenticate(clientMock.Object, requestMock.Object), Times.Once());
        }

        [TestMethod]
        public void Call_Client_Call_Execute_With_Passed_IRestRequest_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            clientMock.Verify(x => x.Execute(requestMock.Object), Times.Once());
        }

        [TestMethod]
        public void Call_Client_Call_Execute_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = apiClient.Post(fakeUri, fakeResource);

            clientMock.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once());
        }
    }
}
