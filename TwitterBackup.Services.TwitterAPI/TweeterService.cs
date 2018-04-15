using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using TwitterBackup.DTO.Tweeters;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.TwitterAPI
{


    public class TweeterService : ITweeterService
    {
        private const string baseUrl = "https://api.twitter.com";
        private const string ResourceFormat = "1.1/users/{0}.json?{1}={2}";


        private readonly IApiClient restApiClient;
        private readonly ITwitterAuthenticator authenticator;

        public TweeterService(IApiClient restApiClient, ITwitterAuthenticator authenticator)
        {
            this.restApiClient = restApiClient ?? throw new ArgumentNullException();
            this.authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
        }


        public GetTweeterDto GetTwitterByScreenName(string tweeterName)
        {
            var resource = string.Format(ResourceFormat, "show", "screen_name", tweeterName);

            restApiClient.Authenticator = this.authenticator;

            var response = restApiClient.Get(baseUrl, resource);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GetTweeterDto>(response.Content);
            }

            var invalidResponseCode = response.StatusCode.ToString();

            throw new ArgumentException(invalidResponseCode);
        }

        public IEnumerable<GetTweeterDto> SearchTweeters(string searchCriteria)
        {
            var resource = string.Format(ResourceFormat, "search", "q", searchCriteria);

            restApiClient.Authenticator = this.authenticator;

            var response = restApiClient.Get(baseUrl, resource);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<GetTweeterDto>>(response.Content);
            }

            var invalidResponseCode = response.StatusCode.ToString();

            throw new ArgumentException(invalidResponseCode);
        }
    }

}

