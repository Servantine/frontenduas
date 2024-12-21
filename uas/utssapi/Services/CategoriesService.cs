using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using utssapi.Models;

namespace utssapi.Services
{
    public class CategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InB1dHJpYXZhbnRpbiIsInJvbGUiOiJhZG1pbiIsIm5iZiI6MTczNDc2MDg2OSwiZXhwIjoxNzM0NzYyNjY5LCJpYXQiOjE3MzQ3NjA4Njl9._HzsCd7mw4-mpcLxhbsikusaz1rrFM3_eizd1lzDjPw");
        }

        public async Task<List<Categories>> GetCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Categories>>("categories");
        }

        public async Task<Categories> GetCategoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Categories>($"categories/{id}");
        }

        public async Task UpdateCategoryAsync(Categories categories)
        {
            await _httpClient.PutAsJsonAsync($"categories/{categories.categoryId}", categories);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"categories/{id}");
        }


        public async Task<Categories> CreateCategoryAsync(Categories newCategory)
        {
            var response = await _httpClient.PostAsJsonAsync("categories", newCategory);

            if (response.IsSuccessStatusCode)
            {

                return await response.Content.ReadFromJsonAsync<Categories>();
            }
            else
            {

                throw new HttpRequestException($"Gagal menambahkan kategori. Status code: {response.StatusCode}");
            }
        }
    }
}