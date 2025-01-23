using ConstrutoraDesbravador.Business.Models;
using Newtonsoft.Json;

namespace ConstrutoraDesbravador.Business.Services
{
    public class RandomUserService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<User>> GetRandomUsersAsync(int count = 5)
        {
            var response = await _httpClient.GetAsync($"?results={count}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var randomUserResponse = JsonConvert.DeserializeObject<RandomUserResponse>(content);

            return randomUserResponse.Results;
        }
    }
}
