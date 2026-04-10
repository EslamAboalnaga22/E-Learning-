using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Inject]
        public required NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();

            navigationManager.NavigateTo("/");
        }
    }
}
