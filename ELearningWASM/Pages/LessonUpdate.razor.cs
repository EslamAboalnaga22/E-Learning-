using ELearning.Core.Dtos.Request;
using ELearning.Core.Dtos.Response;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class LessonUpdate
    {
        [Inject]
        public required IMainClientRepository<LessonDtoRequest> LessonServices { get; set; }

        [Inject]
        public required IMainClientRepository<ModuleDtoDetails> ModuleServices { get; set; }
        public LessonDtoRequest Lesson { get; set; } = new();
        public IEnumerable<ModuleDtoDetails> Modules = [];

        [Parameter]
        public string LessonId { get; set; } = string.Empty;

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "moduleTitle")]
        public string ModuleTitle { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        public string ResultTitle { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Lesson = await LessonServices.GetSingleData($"api/Lessons/GetLessonById/{LessonId}");

            Modules = await ModuleServices.GetAllData("api/Modules/GetAllModules");
        }

        public async Task UpdateLesson()
        {
            try
            {
                await LessonServices.UpdateData(Lesson, $"api/Lessons/UpdateLesson/{LessonId}");
                NavigationManager.NavigateTo($"Lessons?moduleTitle={ModuleTitle}");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
