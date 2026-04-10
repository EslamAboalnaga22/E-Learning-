using Azure;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace ELearning.Infrastructure.RepositoriesClient
{
    public class AuthenticationRepository(NavigationManager navigationManager, HttpClient httpClient, AuthenticationStateProvider stateProvider, ILocalStorageService localStorage) : IAuthenticationRepository
    {
        public NavigationManager _navigationManager = navigationManager;
        private readonly HttpClient _httpClient = httpClient;
        private readonly AuthenticationStateProvider _stateProvider = stateProvider;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

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

        public async Task<AuthModel> RegisterUser(RegisterModel userRegisteration)
        {
            var result = await _httpClient.PostAsJsonAsync<RegisterModel>("api/Auth/Register", userRegisteration);

            if (!result.IsSuccessStatusCode)
            {
                var resultMsg = await result.Content.ReadAsStringAsync();

                var msg = JsonSerializer.Deserialize<AuthModel>(resultMsg, _jsonSerializerOptions);

                return msg;
            }

            return new AuthModel { IsAuthenticated = true };
        }

        public async Task<AuthModel> LogIn(LoginModel loginModel)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginModel);

            var resultMsg = await result.Content.ReadAsStringAsync();

            var msg = JsonSerializer.Deserialize<AuthModel>(resultMsg, _jsonSerializerOptions);

            if (!result.IsSuccessStatusCode)
                return msg;

            await _localStorage.SetItemAsync("authToken", msg.Token);

            ((AppAuthenticatoinStateProvider)_stateProvider).NoifyUserAuthentication(msg.Token);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", msg.Token);


            if (loginModel.RememberMe)
                await _localStorage.SetItemAsync("IsPersistentToken", "IsPersistent");
            else
                await _localStorage.RemoveItemAsync("IsPersistentToken");

            return new AuthModel { IsAuthenticated = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");

            await _localStorage.RemoveItemAsync("IsPersistentToken");

            ((AppAuthenticatoinStateProvider)_stateProvider).NoifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            var Response = await _httpClient.GetAsync("api/Users/GetAllUsers");

            await GetErrors(Response);

            return await _httpClient.GetFromJsonAsync<List<ApplicationUser>>("api/Users/GetAllUsers");
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var Response = await _httpClient.GetAsync($"api/Users/GetUserById/{userId}");

            await GetErrors(Response);

            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Users/GetUserById/{userId}");
        }

        public async Task<ApplicationUser> GetUserByName(string userName)
        {
            var Response = await _httpClient.GetAsync($"api/Users/GetUserByName/{userName}");

            await GetErrors(Response);

            return await _httpClient.GetFromJsonAsync<ApplicationUser>($"api/Users/GetUserByName/{userName}");
        }

        public async Task<UserRolesModel> GetUserWithRoles(string userId)
        {
            var Response = await _httpClient.GetAsync($"api/Users/GetUserWithRoles/{userId}");

            await GetErrors(Response);

            return await _httpClient.GetFromJsonAsync<UserRolesModel>($"api/Users/GetUserWithRoles/{userId}");
        }

        public async Task AddUserRole(UserRolesModel usersRoles)
        {
            var result = await _httpClient.PostAsJsonAsync<UserRolesModel>($"api/Auth/AssignRole", usersRoles);

            await GetErrors(result);
        }

        public async Task<AuthModel> ChangePassword(ChangePasswordModel changePassword)
        {
            var Response = await _httpClient.PutAsJsonAsync<ChangePasswordModel>($"api/Auth/ChangePassword", changePassword);

            await GetErrors(Response);

            if (!Response.IsSuccessStatusCode)
            {
                var resultMsg = await Response.Content.ReadAsStringAsync();

                var msg = JsonSerializer.Deserialize<AuthModel>(resultMsg, _jsonSerializerOptions);

                return new AuthModel { Message = msg!.Message };
            }

            return new AuthModel { IsAuthenticated = true };
        }

        public async Task ForgotPassword(ForgetPasswordModel forgotPassword)
        {
            var result = await _httpClient.PostAsJsonAsync<ForgetPasswordModel>("api/Auth/ForgetPassword", forgotPassword);

            await GetErrors(result);
        }

        public async Task ResetPassword(ResetPasswordModel resetPassword)
        {
            var result = await _httpClient.PutAsJsonAsync<ResetPasswordModel>($"api/Auth/ResetPassword", resetPassword);

            await GetErrors(result);
        }
    }
}
