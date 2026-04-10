using Azure;

namespace ELearning.Infrastructure.RepositoriesClient
{
    public class MainClientRepository<T>(HttpClient httpClient, NavigationManager navigationManager) : IMainClientRepository<T>
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly NavigationManager _navigationManager = navigationManager;

        public async Task<IEnumerable<T>> GetAllData(string ApiName)
        {
            var response = await _httpClient.GetAsync(ApiName);

            await GetErrors(response);

            //var Msg = await response.Content.ReadAsStringAsync();

            //if (!response.IsSuccessStatusCode)
            //    throw new Exception(Msg);

            return await _httpClient.GetFromJsonAsync<List<T>>(ApiName);
        }

        public async Task<T> GetSingleData(string ApiName)
        {
            var response = await _httpClient.GetAsync(ApiName);

            await GetErrors(response);

            return await _httpClient.GetFromJsonAsync<T>(ApiName);
        }

        public async Task<T> AddData(T entity, string ApiName)
        {
            var result = await _httpClient.PostAsJsonAsync<T>(ApiName, entity);

            await GetErrors(result);

            return entity;
        }

        public async Task<T> UpdateData(T entity, string ApiName)
        {
            var result = await _httpClient.PutAsJsonAsync<T>(ApiName, entity);

            await GetErrors(result);

            return entity;
        }

        public async Task<bool> DeleteData(string ApiName)
        {
            var result = await _httpClient.DeleteAsync(ApiName);

            await GetErrors(result);

            return true;
        }

        private async Task GetErrors(HttpResponseMessage Response)
        {
            if (!Response.IsSuccessStatusCode)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                {
                    _navigationManager.NavigateTo("/404");
                }
                else if (Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else if (Response.StatusCode == HttpStatusCode.Forbidden)
                {
                    _navigationManager.NavigateTo("/Unauthorized");
                }
                else
                {
                    _navigationManager.NavigateTo("/500");
                }

                var Msg = await Response.Content.ReadAsStringAsync();

                throw new Exception(Msg); // internal server error
            }
        }
    }
}
