using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Infrastructure.RepositoriesClient
{
    public class AppAuthenticatoinStateProvider(HttpClient httpClient, ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationState _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorage.GetItemAsync<string>("IsPersistentToken") != "IsPersistent")
            {
                await _localStorage.RemoveItemAsync("authToken");
            }

            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
                return _anonymous;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }
        public void NoifyUserAuthentication(string name)
        {
            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(name), "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NoifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
