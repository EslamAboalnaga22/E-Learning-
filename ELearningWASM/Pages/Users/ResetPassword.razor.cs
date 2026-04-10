using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class ResetPassword
    {
        public ResetPasswordModel ResetPasswordModel = new();

        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "email")]
        public string Email { get; set; } = string.Empty;

        [Parameter]
        [SupplyParameterFromQuery(Name = "token")]
        public string Token { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        public async Task Reset()
        {
            ResetPasswordModel.Email = Email;
            ResetPasswordModel.Token = Token.Replace(" ","+");

            await AuthenticationService.ResetPassword(ResetPasswordModel);

            NavigationManager.NavigateTo("/Login");
        }
    }
}
