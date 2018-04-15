using System;

namespace TwitterBackup.Services.ApiClient.Contracts
{
    public interface IUriFactory
    {
        Uri CreateUri(string baseUri);
    }
}
