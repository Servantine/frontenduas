using System.Net.Http;
using uasfixnihbanget.Models;

namespace uasfixnihbanget
{
    public class Apiservices

    {
        private readonly HttpClient _httpClient;
        public string token;

        public Apiservices(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        private async Task<T> GetAsync<T>(string url)
        {
            EnsureBearerToken();
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
        private async Task PostAsync<T>(string url, T data)
        {
            EnsureBearerToken();
            await _httpClient.PostAsJsonAsync(url, data);
        }
        private void EnsureBearerToken()
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<string> LoginAsync(Login login)
        {
            var response = await _httpClient.PostAsJsonAsync("login", login);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

                // Validate if the token exists in the response
                if (loginResponse?.token != null)
                {
                    this.token = loginResponse.token;

                    // Add the token to the HttpClient headers for authenticated requests
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.token);

                    return token; // Return the token for further use if needed
                }
                else
                {
                    throw new Exception("Login successful, but token is missing in the response.");
                }
            }
            else
            {
                // Handle error response and throw detailed exception
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Login failed: {response.ReasonPhrase}. Details: {errorContent}");
            }
        }
    }
}
