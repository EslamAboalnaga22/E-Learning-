using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class Profile
    {
        ApplicationUser user = new();

        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Parameter]
        public string userName { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            user = new ApplicationUser();

            user = await AuthenticationService.GetUserByName(userName);
        }
    }
}
