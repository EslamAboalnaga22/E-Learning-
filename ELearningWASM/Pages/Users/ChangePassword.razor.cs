using ELearning.Core.Dtos.Authentication;
using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class ChangePassword
    {
        public ApplicationUser user = new();

        public ChangePasswordModel ChangePasswordModel = new();

        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; } = string.Empty;

        public bool ChangePasswordErrors { get; set; }

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            user = new ApplicationUser();

            user = await AuthenticationService.GetUserById(UserId);
        }

        public async Task SaveChanges()
        {
            ChangePasswordErrors = false;

            ChangePasswordModel.Email = user.Email!;

            var result = await AuthenticationService.ChangePassword(ChangePasswordModel);

            if (!result.IsAuthenticated)
            {
                ErrorMessages = result.Message;

                ChangePasswordErrors = true;
            }
            else
            {
                await AuthenticationService.Logout();

                NavigationManager.NavigateTo("/");
            }
        }
    }
}
