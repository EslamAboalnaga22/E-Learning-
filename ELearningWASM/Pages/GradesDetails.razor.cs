using CurrieTechnologies.Razor.SweetAlert2;
using ELearning.Core.Dtos;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;

namespace ELearningWASM.Pages
{
    public partial class GradesDetails
    {

        [Inject]
        public required IMainClientRepository<GradeDto> GradeServices { get; set; }

        IEnumerable<GradeDto> Grades { get; set; } = [];

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Grades = [];

                Grades = await GradeServices.GetAllData("api/Grades/GetAllGrades");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        public async Task DeleteGrade(int gradeId)
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
                                "بالفعل لقد تم حذف الصف",
                                SweetAlertIcon.Success
                        );

                    await GradeServices.DeleteData($"api/Grades/DeleteGrade/{gradeId}");

                    Grades = await GradeServices.GetAllData("api/Grades/GetAllGrades");
                }
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

    }
}
