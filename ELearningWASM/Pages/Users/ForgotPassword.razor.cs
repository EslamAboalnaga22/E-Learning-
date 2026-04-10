using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class ForgotPassword
    {
        public ForgetPasswordModel ForgetPasswordModel = new();

        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string SendMessage { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        public async Task Send()
        {
            ForgetPasswordModel.WebLink = NavigationManager.BaseUri + "ResetPassword";

            await AuthenticationService.ForgotPassword(ForgetPasswordModel);
            SendMessage = "سوف يتم ارسال اللينك الخاص بك على البريد الإلكتروني";
        }
    }
}
