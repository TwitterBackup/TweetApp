using System;
using TwitterBackup.Services.ApiClient.Contracts;

namespace TwitterBackup.Services.ApiClient
{
    public class UriFactory : IUriFactory
    {
        public Uri CreateUri(string baseUri)
        {
            return new Uri(baseUri);
        }
    }
}
