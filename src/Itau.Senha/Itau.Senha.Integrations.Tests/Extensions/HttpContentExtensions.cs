using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Itau.Senha.Integrations.Tests.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> GetContentAsync<T>(this HttpContent httpContent)
            where T : class
        {
            var content = await httpContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<HttpResponseMessage> PostAsync<T>(this HttpClient httpClient, string relativePath, T body)
        {
            var json = JsonConvert.SerializeObject(body);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(relativePath, content);
            return response;
        }
    }
}
