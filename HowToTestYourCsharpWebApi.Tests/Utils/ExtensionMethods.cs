using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HowToTestYourCsharpWebApi.Tests.Utils
{
    public static class ExtensionMethods
    {
        public static Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
        {
            return client.GetFromJsonAsync<T>(requestUri);
        }
    }
}
