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
    public class TweetMessageService : ITweetMessageService
    {
        private const string BaseUrl = "https://api.twitter.com";

        private readonly IApiClient restApiClient;
        private readonly ITwitterAuthenticator authenticator;
        private readonly IJsonProvider jsonProvider;

        public TweetMessageService(IApiClient restApiClient, ITwitterAuthenticator authenticator, IJsonProvider jsonProvider)
        {
            this.restApiClient = restApiClient ?? throw new ArgumentNullException();
            this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
            this.jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));
        }

        public async Task<TweetFromTwitterDto> GetTweetById(string tweetId)
        {
            var resource = $"1.1/statuses/show.json?id={tweetId}";

            var result = await this.CallApiClientGetAsync<TweetFromTwitterDto>(resource);

            return result;
        }

        public async Task<IEnumerable<TweetFromTwitterDto>> GetUserTimelineAsync(string tweeterName)
        {
            tweeterName = Uri.EscapeDataString(tweeterName);

            var resource = $"1.1/statuses/user_timeline.json?screen_name={tweeterName}";

            var tweets = await this.CallApiClientGetAsync<IEnumerable<TweetFromTwitterDto>>(resource);

            if (tweets == null || !tweets.Any())
            {
                return null;
            }

            return tweets;
        }

        public async Task<IEnumerable<TweetFromTwitterDto>> SearchTweetsAsync(string searchCriteria)
        {
            searchCriteria = Uri.EscapeDataString(searchCriteria);

            var resource = $"1.1/search/tweets.json?q={searchCriteria}";

            var tweets = await this.CallApiClientGetAsync<IEnumerable<TweetFromTwitterDto>>(resource);

            if (tweets == null || !tweets.Any())
            {
                return null;
            }

            return tweets;
        }

        public async Task<RetweetResultDto> RetweetAsync(string tweetId)
        {
            var resource = $"/1.1/statuses/retweet/{tweetId}.json";

            var response = await this.restApiClient.PostAsync(BaseUrl, resource, this.authenticator);

            return this.jsonProvider.DeserializeObject<RetweetResultDto>(response.Content);
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
    }
}
