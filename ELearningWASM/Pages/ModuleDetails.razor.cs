using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Core.Dtos.Response;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class ModuleDetails
    {
        [Inject]
        public required IMainClientRepository<ModuleDtoDetails> ModuleServices { get; set; }

        IEnumerable<ModuleDtoDetails> Modules { get; set; } = [];

        [Parameter]
        [SupplyParameterFromQuery(Name = "descriptionCourse")]
        public string CourseDescription { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;
    
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Modules = [];

                Modules = await ModuleServices.GetAllData($"api/Modules/GetAllModulesByCourseDescription?descriptionCourse={CourseDescription}");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        public async Task DeleteModule(int ModuleId)
        {
            try
            {
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
                                "بالفعل لقد تم حذف النموذج",
                                SweetAlertIcon.Success
                        );

                    await ModuleServices.DeleteData($"api/Modules/GetModuleById/{ModuleId}");

                    Modules = await ModuleServices.GetAllData("api/Modules/GetAllModules");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
