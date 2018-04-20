using Newtonsoft.Json;
using TwitterBackup.Infrastructure.Providers.Contracts;

namespace TwitterBackup.Infrastructure.Providers
{
    public class JsonProvider : IJsonProvider
    {
        public T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
