using ELearning.Core.Dtos;
using ELearning.Core.Dtos.Request;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class CourseCreateAndUpdate
    {
        [Inject]
        public required IMainClientRepository<CourseDtoRequest> CourseServices { get; set; }

        [Inject]
        public required IMainClientRepository<GradeDto> GradeServices { get; set; }
        public CourseDtoRequest Course { get; set; } = new();
        public IEnumerable<GradeDto> Grades = [];


        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string CourseId { get; set; } = string.Empty;

        [Parameter]
        [SupplyParameterFromQuery(Name = "gradeId")]
        public string GradeId { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(CourseId))
                Course = await CourseServices.GetSingleData($"api/Courses/GetCourseById/{CourseId}");

            Grades = await GradeServices.GetAllData("api/Grades/GetAllGrades");
        }

        public async Task CreateAndUpdateCourse()
        {
            try
            {
                if (!string.IsNullOrEmpty(CourseId))
                {
                    await CourseServices.UpdateData(Course, $"api/Courses/UpdateCourse/{CourseId}");

                    NavigationManager.NavigateTo($"Courses?gradeId={GradeId}");
                }
                else
                {
                    await CourseServices.AddData(Course, "api/Courses/AddCourse");
                    NavigationManager.NavigateTo("Courses?gradeId={GradeId}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
