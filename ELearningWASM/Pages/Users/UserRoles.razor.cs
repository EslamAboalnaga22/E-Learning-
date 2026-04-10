using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class UserRoles
    {
        UserRolesModel UserRolesModel = new();

        List<SelectedRolesModel> RolesList = [];
        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserId { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            UserRolesModel = new UserRolesModel();

            UserRolesModel = await AuthenticationService.GetUserWithRoles(UserId);
        }

        private async Task Save()
        {
            await AuthenticationService.AddUserRole(UserRolesModel);

            NavigationManager.NavigateTo("/UsersList");
        }
    }
}
