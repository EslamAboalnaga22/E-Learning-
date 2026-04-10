using ELearning.Core.Dtos.Request;
using ELearning.Core.Dtos.Response;
using ELearning.Core.Entities;
using ELearning.Core.InterfacesClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ELearningWASM.Pages
{
    public partial class ContentCreate
    {
        [Inject]
        public required IMainClientRepository<ContentCreateDtoRequest> ContentServices { get; set; }
        [Inject]
        public required IMainClientRepository<LessonDtoDetails> LessonServices { get; set; }

        public ContentCreateDtoRequest Content { get; set; } = new();
        public IEnumerable<LessonDtoDetails> Lessons { get; set; } = [];

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string ErrorMessages { get; set; } = string.Empty;

        public required IBrowserFile SelectedFile;

        [Parameter]
        [SupplyParameterFromQuery(Name = "moduleTitle")]
        public string ModuleTitle { get; set; } = string.Empty;

        string FileUrl = string.Empty;

        public async Task Create()
        {
            try
            {
                Content.Type = SelectedFile.ContentType;
                Content.FilePath = SelectedFile.Name;

                using var ms = new MemoryStream(100 * 1024 * 1024);
                await SelectedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(ms);
                Content.NewFile = ms.ToArray();

                NavigationManager.NavigateTo("/Contents");

                await ContentServices.AddData(Content, "api/Contents/AddContent");

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

        protected override async Task OnInitializedAsync()
        {
            Lessons = await LessonServices.GetAllData("api/Lessons/GetAllLessons");
        }
    }
}

public static class FormFileExtensions
{
    public static IFormFile ToFormFile(this byte[] byteArray, string fileName, string contentType)
    {
        var stream = new MemoryStream(byteArray);
        return new FormFile(stream, 0, byteArray.Length, fileName, fileName)
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };
    }
}