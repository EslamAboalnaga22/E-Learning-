using ELearning.Core.Dtos;
using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class GradeCreateAndUpdate
    {
        [Inject]
        public required IMainClientRepository<GradeDto> GradeServices { get; set; }

        GradeDto Grade { get; set; } = new();

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string gradeId { get; set; } = string.Empty;

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if(!string.IsNullOrEmpty(gradeId)) 
                Grade = await GradeServices.GetSingleData($"api/Grades/GetGradeById/{gradeId}");
        }

        public async Task CreateAndUpdateGrade()
        {
            try
            {
                if (!string.IsNullOrEmpty(gradeId))
                {
                    await GradeServices.UpdateData(Grade, $"api/Grades/UpdateGrade/{gradeId}");

                    NavigationManager.NavigateTo("Grades");
                }
                else
                {
                    await GradeServices.AddData(Grade, "api/Grades/AddGrade");
                    NavigationManager.NavigateTo("Grades");
                }
            }
            catch(Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }
    }
}
