using ELearning.Core.Dtos.Request;
using ELearning.Core.Dtos.Response;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace ELearningWASM.Pages
{
    public partial class ContentUpdate
    {
        [Inject]
        public required IMainClientRepository<ContentUpdateDtoRequest> ContentServices { get; set; }

        [Inject]
        public required IMainClientRepository<LessonDtoDetails> LessonServices { get; set; }
        public ContentUpdateDtoRequest Content { get; set; } = new();
        public IEnumerable<LessonDtoDetails> Lessons = [];

        [Parameter]
        public string ContentId { get; set; } = string.Empty;

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        public required IBrowserFile SelectedFile;

        [Parameter]
        [SupplyParameterFromQuery(Name = "moduleTitle")]
        public string ModuleTitle { get; set; } = string.Empty;

        string FileUrl = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            Content = await ContentServices.GetSingleData($"api/Contents/GetContentById/{ContentId}");

            Lessons = await LessonServices.GetAllData("api/Lessons/GetAllLessons");
        }

        public async Task UpdateContent()
        {
            try
            {
                if (SelectedFile is not null)
                {
                    Content.Type = SelectedFile.ContentType;
                    Content.FilePath = SelectedFile.Name;

                    using var ms = new MemoryStream(100 * 1024 * 1024);
                    await SelectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(ms);
                    Content.NewFile = ms.ToArray();
                }

                await ContentServices.UpdateData(Content, $"api/Contents/UpdateContent/{ContentId}");

                NavigationManager.NavigateTo("/Contents");
            }
            catch (Exception ex)
            {
                ErrorMessages = ex.Message;
            }
        }

        private async Task OnFileSelection(InputFileChangeEventArgs e)
        {
            SelectedFile = e.File;

            var buffer = new byte[SelectedFile.Size];

            await SelectedFile.OpenReadStream().ReadAsync(buffer);

            string imgType = SelectedFile.ContentType;

            FileUrl = $"data:{imgType};Base64,{Convert.ToBase64String(buffer)}";

            this.StateHasChanged();
        }
    }
}
