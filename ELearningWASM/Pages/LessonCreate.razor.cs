using ELearning.Core.Dtos.Request;
using ELearning.Core.Dtos;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using ELearning.Core.Dtos.Response;
using ELearning.Core.Entities;

namespace ELearningWASM.Pages
{
    public partial class LessonCreate
    {
        [Inject]
        public required IMainClientRepository<LessonDtoRequest> LessonServices { get; set; }

        [Inject]
        public required IMainClientRepository<ModuleDtoDetails> ModuleServices { get; set; }
        public LessonDtoRequest Lesson { get; set; } = new();
        public IEnumerable<ModuleDtoDetails> Modules = [];

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "moduleTitle")]
        public string ModuleTitle { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Modules = await ModuleServices.GetAllData("api/Modules/GetAllModules");
        }

        public async Task CreateLesson()
        {
            try
            {
                await LessonServices.AddData(Lesson, "api/Lessons/AddLesson");
                NavigationManager.NavigateTo($"Lessons?moduleTitle={ModuleTitle}");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
