using RestSharp;
using System;
using System.Security.Cryptography;
using System.Text;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient
{
    public class TwitterAuthenticator : IOAuth1Authenticator
    {
        private const string ClientNullExceptionMessage = "Could not authenticate null Client";
        private const string RequestNullExceptionMessage = "Could not authenticate null Request";

        private const string BaseFormat =
        "oauth_consumer_key={0}&" +
        "oauth_nonce={1}&" +
        "oauth_signature_method={2}&" +
        "oauth_timestamp={3}&" +
        "oauth_token={4}&" +
        "oauth_version={5}";

        private const string HeaderFormat =
        "OAuth " +
        "oauth_consumer_key=\"{0}\", " +
        "oauth_nonce=\"{1}\", " +
        "oauth_signature=\"{2}\", " +
        "oauth_signature_method=\"{3}\", " +
        "oauth_timestamp=\"{4}\", " +
        "oauth_token=\"{5}\", " +
        "oauth_version=\"{6}\"";

        private const string HeaderKey = "Authorization";

        private readonly DateTime unixDateTimeStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public string ОauthToken { get; set; }

        public string OauthTokenSecret { get; set; }

        public string OauthConsumerKey { get; set; }

        public string OauthConsumerSecret { get; set; }

        public string OauthVersion { get; set; } = "1.0";

        public string OauthSignatureMethod { get; set; } = "HMAC-SHA1";

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (client == null)
            {
                throw new ArgumentNullException(ClientNullExceptionMessage);
            }

            if (request == null)
            {
                throw new ArgumentException(RequestNullExceptionMessage);
            }

            var oAuthNonce = this.GenerateOAuthNonce();
            var oAuthTimeStamp = this.CalculateOAuthTimeStamp();

            var baseFormat = BaseFormat;

            var baseString = string.Format(baseFormat,
                this.OauthConsumerKey,
                oAuthNonce,
                this.OauthSignatureMethod,
                oAuthTimeStamp,
                this.ОauthToken,
                this.OauthVersion
            );

            var resource = request.Resource;

            var parametersStartIndex = resource?.IndexOf("?");

            if (parametersStartIndex != null)
            {
                var parameters = resource.Split("?")[1].Split("&");

                foreach (var parameter in parameters)
                {
                    var sides = parameter.Split("=");

                    if (sides.Length == 2)
                    {
                        baseString += $"&{sides[0]}={Uri.EscapeDataString(sides[1])}";
                    }
                }
            }

            var method = request.Method.ToString().ToUpper();

            var fullUrl = client.BaseUrl + request.Resource;
            var resourceUrl = fullUrl.Split("?")[0];

            baseString = string.Concat(method + "&", Uri.EscapeDataString(resourceUrl), "&", Uri.EscapeDataString(baseString));

            var compositeKey = this.GetCompositKey();

            var oAuthSignature = this.OAuthSignature(baseString, compositeKey);

            var authHeader = string.Format(HeaderFormat,
                Uri.EscapeDataString(this.OauthConsumerKey),
                Uri.EscapeDataString(oAuthNonce),
                Uri.EscapeDataString(oAuthSignature),
                Uri.EscapeDataString(this.OauthSignatureMethod),
                Uri.EscapeDataString(oAuthTimeStamp),
                Uri.EscapeDataString(this.ОauthToken),
                Uri.EscapeDataString(this.OauthVersion)
            );

            request.AddHeader(HeaderKey, authHeader);
        }

        private string GenerateOAuthNonce()
        {
            var oAuthNonce = Guid.NewGuid().ToString("N");
            return oAuthNonce;
        }

        private string CalculateOAuthTimeStamp()
        {
            var timeSpan = DateTime.UtcNow - this.unixDateTimeStart;

            var oAuthTimestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            return oAuthTimestamp;
        }

        private string GetCompositKey()
        {
            var compositeKey = string.Concat(
                Uri.EscapeDataString(this.OauthConsumerSecret),
                "&",
                Uri.EscapeDataString(this.OauthTokenSecret));

            return compositeKey;
        }

        private string OAuthSignature(string baseString, string compositeKey)
        {
            string oAuthSignature;

            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(compositeKey)))
            {
                oAuthSignature = Convert.ToBase64String(
                    hasher.ComputeHash(Encoding.ASCII.GetBytes(baseString)));
            }

            return oAuthSignature;
        }
    }
}
