using ELearning.Core.Dtos.Authentication;
using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Users
{
    public partial class UsersList
    {
        IEnumerable<ApplicationUser> Users = [];

        [Inject]
        public required IAuthenticationRepository AuthenticationService { get; set; }

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = [];

                Users = await AuthenticationService.GetAllUsers();
            }
            catch(Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
