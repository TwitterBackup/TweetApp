using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient
{
    public class ApiClient : IApiClient
    {
        private const string BaseUrlExceptionMessage = "Base url cannot be null or whitespace.";
        private const string ResourceExceptionMessage = "Resource cannot be null or whitespace.";

        private readonly IRestClient client;
        private readonly IRestRequest request;
        private readonly IUriFactory uriFactory;

        public ApiClient(IRestClient client, IRestRequest request, IUriFactory uriFactory)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.uriFactory = uriFactory ?? throw new ArgumentNullException(nameof(uriFactory));
        }

        public IRestResponse Get(string baseUri, string resource, IAuthenticator authenticator = null)
        {
            this.ValidateFullUrl(baseUri, resource);

            this.client.BaseUrl = this.uriFactory.CreateUri(baseUri);

            this.request.Resource = resource;
            this.request.Method = Method.GET;

            authenticator?.Authenticate(this.client, this.request);

            var response = this.client.Execute(this.request);

            return response;
        }

        public async Task<IRestResponse> GetAsync(string baseUri, string resource, IAuthenticator authenticator = null)
        {
            this.ValidateFullUrl(baseUri, resource);

            this.client.BaseUrl = this.uriFactory.CreateUri(baseUri);

            this.request.Resource = resource;
            this.request.Method = Method.GET;

            authenticator?.Authenticate(this.client, this.request);

            var response = await this.client.ExecuteTaskAsync(this.request);

            return response;
        }

        public IRestResponse Post(string baseUri, string resource, IAuthenticator authenticator = null)
        {
            this.ValidateFullUrl(baseUri, resource);

            this.client.BaseUrl = this.uriFactory.CreateUri(baseUri);

            this.request.Resource = resource;
            this.request.Method = Method.POST;

            authenticator?.Authenticate(this.client, this.request);

            var responce = this.client.Execute(this.request);

            return responce;
        }

        public async Task<IRestResponse> PostAsync(string baseUri, string resource, IAuthenticator authenticator = null)
        {
            this.ValidateFullUrl(baseUri, resource);

            this.client.BaseUrl = this.uriFactory.CreateUri(baseUri);

            this.request.Resource = resource;
            this.request.Method = Method.POST;

            authenticator?.Authenticate(this.client, this.request);

            var responce = await this.client.ExecuteTaskAsync(this.request);

            return responce;
        }

        private void ValidateFullUrl(string baseUri, string resource)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentException(BaseUrlExceptionMessage);
            }

            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException(ResourceExceptionMessage);
            }
        }
    }
}
