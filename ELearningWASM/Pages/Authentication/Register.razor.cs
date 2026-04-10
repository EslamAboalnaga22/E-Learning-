using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Authentication
{
    public partial class Register
    {
        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; } 

        public RegisterModel RegisterModel { get; set; } = new();

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        public bool RegisterErrors { get; set; }
        public string Errors { get; set; } = string.Empty;

        public async Task RegisterAsync()
        {
            RegisterErrors = false;

            var result = await AuthenticationService.RegisterUser(RegisterModel);

            if (!result.IsAuthenticated)
            {
                Errors = result.Message;
                RegisterErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
