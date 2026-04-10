using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Core.Dtos.Response;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class CourseDetails
    {
        [Inject]
        public required IMainClientRepository<CourseDtoDetails> CourseServices { get; set; }

        IEnumerable<CourseDtoDetails> Courses { get; set; } = [];

        [Parameter]
        [SupplyParameterFromQuery(Name = "gradeId")]
        public string GradeId { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Courses = [];

                Courses = await CourseServices.GetAllData($"api/Courses/GetAllCoursesByGradeId?gradeId={GradeId}");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        public async Task DeleteCourse(int courseId)
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
                                "بالفعل لقد تم حذف الكورس",
                                SweetAlertIcon.Success
                        );

                    await CourseServices.DeleteData($"api/Courses/GetCourseById/{courseId}");

                    Courses = await CourseServices.GetAllData("api/Grades/GetAllGrades");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

    }
}
