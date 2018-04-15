using RestSharp.Authenticators;

namespace TwitterBackup.Services.ApiClient.Contracts
{
    public interface ITwitterAuthenticator : IAuthenticator
    {
        string ОauthToken { get; set; }

        string OauthTokenSecret { get; set; }

        string OauthConsumerKey { get; set; }

        string OauthConsumerSecret { get; set; }

        string OauthVersion { get; set; }

        string OauthSignatureMethod { get; set; }
    }
}
