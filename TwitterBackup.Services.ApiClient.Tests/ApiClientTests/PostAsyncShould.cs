using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient.Tests.ApiClientTests
{
    [TestClass]
    public class PostAsyncShould
    {
        [TestMethod]
        public async Task Return_IRestResponse_When_Called_With_Valid_Parameters()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var expectedResult = new Mock<IRestResponse>();

            clientMock.Setup(c => c.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(expectedResult.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var actual = await apiClient.PostAsync(fakeUri, fakeResource);

            Assert.AreSame(expectedResult.Object, actual);
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Null_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await apiClient.PostAsync(null, fakeResource));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Empty_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await apiClient.PostAsync(string.Empty, fakeResource));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_White_Space_BaseUri_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeResource = "/resource";

            await Assert.ThrowsExceptionAsync<ArgumentException>
                (async () => await apiClient.PostAsync("   ", fakeResource));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Null_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await apiClient.PostAsync(fakeUri, null));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_Empty_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await apiClient.PostAsync(fakeUri, string.Empty));
        }

        [TestMethod]
        public async Task Throw_ArgumentException_When_Called_With_White_Space_Resource_String()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";

            await Assert.ThrowsExceptionAsync<ArgumentException>(
                async () => await apiClient.PostAsync(fakeUri, "      "));
        }

        [TestMethod]
        public async Task Set_Client_BaseUri_Correct_With_Passed_Parramter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var expectedUrl = new Uri(fakeUri);

            uriFactoryMock.Setup(f => f.CreateUri(It.IsAny<string>())).Returns(expectedUrl);

            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            clientMock.VerifySet(x => x.BaseUrl = expectedUrl, Times.Once());
        }

        [TestMethod]
        public async Task Set_Client_BaseUri_Is_Called_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            clientMock.VerifySet(x => x.BaseUrl = It.IsAny<Uri>(), Times.Once());
        }

        [TestMethod]
        public async Task Call_Factory_With_Given_BaseUrl_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            uriFactoryMock.Verify(x => x.CreateUri(fakeUri), Times.Once());
        }

        [TestMethod]
        public async Task Set_Request_Resource_Correct_With_Passed_Parramter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Resource = fakeResource, Times.Once());
        }

        [TestMethod]
        public async Task Set_Request_Resource_Is_Called_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Resource = It.IsAny<string>(), Times.Once());
        }

        [TestMethod]
        public async Task Set_Request_Method_To_Post_Method()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Method = Method.POST, Times.Once());
        }

        [TestMethod]
        public async Task Set_Request_Method_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            requestMock.VerifySet(x => x.Method = It.IsAny<Method>(), Times.Once());
        }

        [TestMethod]
        public async Task Authenticator_Authenticate_Is_Called_When_It_Is_Passed_As_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();
            var authenticator = new Mock<IAuthenticator>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource, authenticator.Object);

            authenticator.Verify(x => x.Authenticate(It.IsAny<IRestClient>(), It.IsAny<IRestRequest>()), Times.Once());
        }

        [TestMethod]
        public async Task Authenticator_Authenticate_Is_Called_With_Give_Parameters_When_It_Is_Passed_As_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();
            var authenticator = new Mock<IAuthenticator>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource, authenticator.Object);

            authenticator.Verify(x => x.Authenticate(clientMock.Object, requestMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task Call_Client_Call_ExecuteTaskAsync_With_Passed_IRestRequest_Parameter()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            clientMock.Verify(x => x.ExecuteTaskAsync(requestMock.Object), Times.Once());
        }

        [TestMethod]
        public async Task Call_Client_Call_ExecuteTaskAsync_Once()
        {
            var clientMock = new Mock<IRestClient>();
            var requestMock = new Mock<IRestRequest>();
            var uriFactoryMock = new Mock<IUriFactory>();

            var apiClient = new ApiClient(clientMock.Object, requestMock.Object, uriFactoryMock.Object);

            var fakeUri = "https://api.something.com";
            var fakeResource = "/resource";

            var _ = await apiClient.PostAsync(fakeUri, fakeResource);

            clientMock.Verify(x => x.ExecuteTaskAsync(It.IsAny<IRestRequest>()), Times.Once());
        }
    }
}
