namespace TwitterBackup.Infrastructure.Providers.Contracts
{
    public interface IJsonProvider
    {
        T DeserializeObject<T>(string json);
    }
}
