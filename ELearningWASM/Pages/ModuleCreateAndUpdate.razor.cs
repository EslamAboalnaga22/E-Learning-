using ELearning.Core.Dtos.Request;
using ELearning.Core.Dtos;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using ELearning.Core.Dtos.Response;

namespace ELearningWASM.Pages
{
    public partial class ModuleCreateAndUpdate
    {
        [Inject]
        public required IMainClientRepository<ModuleDtoRequest> ModuleServices { get; set; }
        public ModuleDtoRequest Module { get; set; } = new();


        [Inject]
        public required IMainClientRepository<CourseDtoDetails> CourseServices { get; set; }

        public IEnumerable<CourseDtoDetails> Courses = [];


        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ModuleId { get; set; } = string.Empty;

        [Parameter]
        [SupplyParameterFromQuery(Name = "descriptionCourse")]
        public string CourseDescription { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ModuleId))
                Module = await ModuleServices.GetSingleData($"api/Modules/GetModuleById/{ModuleId}");

            Courses = await CourseServices.GetAllData("api/Courses/GetAllCourses");
        }

        public async Task CreateAndUpdateModule()
        {
            try
            {
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    await ModuleServices.UpdateData(Module, $"api/Modules/UpdateModule/{ModuleId}");

                    NavigationManager.NavigateTo($"Modules?descriptionCourse={CourseDescription}");
                }
                else
                {
                    await ModuleServices.AddData(Module, "api/Modules/AddModule");
                    NavigationManager.NavigateTo($"Modules?descriptionCourse={CourseDescription}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
