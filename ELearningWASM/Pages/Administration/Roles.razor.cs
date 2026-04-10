using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Core.Dtos.Authentication;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ELearningWASM.Pages.Administration
{
    public partial class Roles
    {
        private IEnumerable<RoleModel> RolesList = [];

        [Inject]
        public required IMainClientRepository<RoleModel> RolesServices { get; set; }

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                RolesList = [];

                RolesList = await RolesServices.GetAllData("api/Auth/GetAllRoles");
            }
            catch(Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var currRole = RolesList.FirstOrDefault(x => x.Id == id);

                var result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "هـل أنـت مـتـأكـد مـن الحـذف؟",
                    Text = "سوف تكون غير قادر على الرجوع في قرارك",
                    Icon = "warning",
                    ShowCancelButton = true,
                    ConfirmButtonColor = "#3085d6",
                    CancelButtonColor = "#d33",
                    ConfirmButtonText = "نعم، احذفه الآن",
                    CancelButtonText = "لا تحذف"
                });

                if (!string.IsNullOrEmpty(result.Value))
                {
                    await Swal.FireAsync("تـم الـحـذف",
                                "بالفعل لقد تم حذف المسئولية",
                                SweetAlertIcon.Success
                        );

                    await RolesServices.DeleteData($"api/Auth/DeleteRole/{id}");

                    RolesList = await RolesServices.GetAllData("api/Auth/GetAllRoles");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
