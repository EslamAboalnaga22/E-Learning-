using ELearning.Core.Dtos;
using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages.Administration
{
    public partial class RoleCreateAndUpdate
    {
        [Inject]
        public required IMainClientRepository<RoleModel> RoleServices { get; set; }

        RoleModel Role { get; set; } = new();

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string RoleId { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(RoleId))
                Role = await RoleServices.GetSingleData($"api/Auth/GetRoleById/{RoleId}");
        }

        public async Task CreateAndUpdateRole()
        {
            try
            {
                if (!string.IsNullOrEmpty(RoleId))
                {
                    await RoleServices.UpdateData(Role, $"api/Auth/UpdateRole/{RoleId}");

                    NavigationManager.NavigateTo("Roles");
                }
                else
                {
                    await RoleServices.AddData(Role, "api/Auth/CreateRole");
                    NavigationManager.NavigateTo("Roles");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
