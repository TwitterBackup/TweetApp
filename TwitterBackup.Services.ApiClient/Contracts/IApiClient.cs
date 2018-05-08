using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace TwitterBackup.Services.ApiClient.Contracts
{
    public interface IApiClient
    {
        IRestResponse Get(string baseUri, string resource, IAuthenticator authenticator = null);

        Task<IRestResponse> GetAsync(string baseUri, string resource, IAuthenticator authenticator = null);

        IRestResponse Post(string baseUri, string resource, IAuthenticator authenticator = null);

        Task<IRestResponse> PostAsync(string baseUri, string resource, IAuthenticator authenticator = null);
    }
}
