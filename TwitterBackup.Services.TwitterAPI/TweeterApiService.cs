using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{
    public class TweeterApiService : ITweeterApiService
    {
        private const string BaseUrl = "https://api.twitter.com";
        private const string ResourceFormat = "1.1/users/{0}.json?{1}={2}";

        private readonly IApiClient restApiClient;
        private readonly ITwitterAuthenticator authenticator;
        private readonly IJsonProvider jsonProvider;

        public TweeterApiService(IApiClient restApiClient, ITwitterAuthenticator authenticator, IJsonProvider jsonProvider)
        {
            this.restApiClient = restApiClient ?? throw new ArgumentNullException();
            this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            this.jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        public async Task<TweeterDto> GetTweeterByScreenNameAsync(string tweeterName)
        {
            this.ValidateStringForNullOrWhiteSpace(tweeterName, "Tweeter name cannot be null or white space.");

            tweeterName = Uri.EscapeDataString(tweeterName);

            var resource = string.Format(ResourceFormat, "show", "screen_name", tweeterName);

            var result = await this.CallApiClientGetAsync<TweeterDto>(resource);

            return result;
        }

        public async Task<IEnumerable<TweeterDto>> SearchTweetersAsync(string searchCriteria)
        {
            this.ValidateStringForNullOrWhiteSpace(searchCriteria, "Search string cannot be null or white space.");

            searchCriteria = Uri.EscapeDataString(searchCriteria);

            var resource = string.Format(ResourceFormat, "search", "q", searchCriteria);

            var result = await this.CallApiClientGetAsync<IEnumerable<TweeterDto>>(resource);

            if (result == null || !result.Any())
            {
                return null;
            }

            return result;
        }

        private async Task<T> CallApiClientGetAsync<T>(string resource) where T : class
        {
            var response = await this.restApiClient.GetAsync(BaseUrl, resource, this.authenticator);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resultFromApi = this.jsonProvider.DeserializeObject<T>(response.Content);

                return resultFromApi;
            }

            return null;
        }

        private void ValidateStringForNullOrWhiteSpace(string str, string errorMessage = "Parameter cannot be null or white space.")
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
