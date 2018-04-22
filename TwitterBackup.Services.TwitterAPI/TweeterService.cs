using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{
    public class TweeterService : ITweeterService
    {
        private const string BaseUrl = "https://api.twitter.com";
        private const string ResourceFormat = "1.1/users/{0}.json?{1}={2}";

        private readonly IApiClient restApiClient;
        private readonly ITwitterAuthenticator authenticator;
        private readonly IJsonProvider jsonProvider;

        public TweeterService(IApiClient restApiClient, ITwitterAuthenticator authenticator, IJsonProvider jsonProvider)
        {
            this.restApiClient = restApiClient ?? throw new ArgumentNullException();
            this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            this.jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        public async Task<TweeterDto> GetTweeterByScreenNameAsync(string tweeterName)
        {
            var resource = string.Format(ResourceFormat, "show", "screen_name", tweeterName);

            var result = await this.CallApiClientGetAsync<TweeterDto>(resource);

            return result;
        }

        public async Task<IEnumerable<TweeterDto>> SearchTweetersAsync(string searchCriteria)
        {
            var resource = string.Format(ResourceFormat, "search", "q", searchCriteria);

            var result = await this.CallApiClientGetAsync<IEnumerable<TweeterDto>>(resource);

            return result;
        }

        private async Task<T> CallApiClientGetAsync<T>(string resource) where T : class
        {
            var responce = await this.restApiClient.GetAsync(BaseUrl, resource, this.authenticator);

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                return this.jsonProvider.DeserializeObject<T>(responce.Content);
            }

            return null;
        }
    }
}
