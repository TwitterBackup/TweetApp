using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TwitterBackup.DTO.Tweets;
using TwitterBackup.Infrastructure.Providers.Contracts;
using TwitterBackup.Services.ApiClient.Contracts;
using TwitterBackup.Services.TwitterAPI.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{
    public class TweetApiService : ITweetApiService
    {
        private const string BaseUrl = "https://api.twitter.com";

        private readonly IApiClient restApiClient;
        private readonly ITwitterAuthenticator authenticator;
        private readonly IJsonProvider jsonProvider;

        public TweetApiService(IApiClient restApiClient, ITwitterAuthenticator authenticator, IJsonProvider jsonProvider)
        {
            this.restApiClient = restApiClient ?? throw new ArgumentNullException();
            this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            this.jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        public async Task<ApiTweetDto> GetTweetByIdAsync(string tweetId)
        {
            this.ValidateStringForNullOrWhiteSpace(tweetId, "Tweet id cannot be null or white space.");

            var resource = $"1.1/statuses/show.json?id={tweetId}";

            var result = await this.CallApiClientGetAsync<ApiTweetDto>(resource);

            return result;
        }

        public async Task<IEnumerable<ApiTweetDto>> GetUserTimelineAsync(string tweeterId)
        {
            this.ValidateStringForNullOrWhiteSpace(tweeterId, "Tweeter id cannot be null or white space.");

            tweeterId = Uri.EscapeDataString(tweeterId);

            var resource = $"1.1/statuses/user_timeline.json?user_id={tweeterId}";

            var tweets = await this.CallApiClientGetAsync<IEnumerable<ApiTweetDto>>(resource);

            if (tweets == null || !tweets.Any())
            {
                return null;
            }

            return tweets;
        }

        public async Task<IEnumerable<ApiTweetDto>> SearchTweetsAsync(string searchCriteria)
        {
            this.ValidateStringForNullOrWhiteSpace(searchCriteria, "Search string cannot be null or white space.");

            searchCriteria = Uri.EscapeDataString(searchCriteria);

            var resource = $"1.1/search/tweets.json?q={searchCriteria}";

            var tweets = await this.CallApiClientGetAsync<IEnumerable<ApiTweetDto>>(resource);

            if (tweets == null || !tweets.Any())
            {
                return null;
            }

            return tweets;
        }

        public async Task<RetweetResultDto> RetweetAsync(string tweetId)
        {
            this.ValidateStringForNullOrWhiteSpace(tweetId, "Tweet id cannot be null or white space.");

            var resource = $"1.1/statuses/retweet/{tweetId}.json";

            var response = await this.restApiClient.PostAsync(BaseUrl, resource, this.authenticator);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return this.jsonProvider.DeserializeObject<RetweetResultDto>(response.Content);
            }

            return null;
        }

        private async Task<T> CallApiClientGetAsync<T>(string resource) where T : class
        {
            var response = await this.restApiClient.GetAsync(BaseUrl, resource, this.authenticator);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return this.jsonProvider.DeserializeObject<T>(response.Content);
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
