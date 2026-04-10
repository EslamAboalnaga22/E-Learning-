using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Authentication
{
    public partial class Login
    {
        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        public LoginModel loginModel { get; set; } = new();

        [Inject]
        public required NavigationManager navigationManager { get; set; }

        public bool LoginErrors { get; set; }
        public string Errors { get; set; } = string.Empty;

        [Parameter]
        public bool RemeberMe { get; set; } = false;

        public async Task LoginAsync()
        {
            LoginErrors = false;

            loginModel.RememberMe = RemeberMe;

            var result = await AuthenticationService.LogIn(loginModel);

            if (!result.IsAuthenticated)
            {
                Errors = result.Message;
                LoginErrors = true;
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }

        private void CheckBoxChanged()
        {
            RemeberMe = !RemeberMe;
        }
    }
}
