using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Core.Dtos.Response;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ELearningWASM.Pages
{
    public partial class LessonsAndContentDetails
    {
        [Inject]
        public required IMainClientRepository<LessonDtoDetails> LessonServices { get; set; }
        [Inject]
        public required IMainClientRepository<ContentDtoDetails> ContentServices { get; set; }

        IEnumerable<LessonDtoDetails> Lessons { get; set; } = [];
        IEnumerable<ContentDtoDetails> Contents { get; set; } = [];

        [Parameter]
        [SupplyParameterFromQuery(Name = "moduleTitle")]
        public string ModuleTitle { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        private bool IsActive = false;
        private void ActiveChanged()
        {
            IsActive = !IsActive;
        }
        protected override async Task OnInitializedAsync()
        {
            try {
                Lessons = [];

                Lessons = await LessonServices.GetAllData($"api/Lessons/GetAllLessonsByModuleTitle?moduleTitle={ModuleTitle}");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        public async Task DeleteLesson(int lessonId)
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
                                "بالفعل لقد تم حذف hg",
                                SweetAlertIcon.Success
                        );

                    await LessonServices.DeleteData($"api/Lessons/DeleteLesson/{lessonId}");

                    Lessons = await LessonServices.GetAllData("api/Lessons/GetAllLessons");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
        public async Task DeleteContent(int contentId)
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

                    await ContentServices.DeleteData($"api/Contents/DeleteContent/{contentId}");

                    Lessons = await LessonServices.GetAllData("api/Lessons/GetAllLessons");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
