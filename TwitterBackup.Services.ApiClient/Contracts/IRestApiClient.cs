﻿using RestSharp;
using RestSharp.Authenticators;

namespace TwitterBackup.Services.ApiClient.Contracts
{
    public interface IRestApiClient
    {
        IAuthenticator Authenticator { get; set; }

        IRestResponse Get(string baseUri, string resource);

        IRestResponse Post(string baseUri, string resource);
    }
}
