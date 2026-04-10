using Microsoft.AspNetCore.Authorization;

namespace ELearning.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentsController(IUnitOfWrok unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWrok _unitOfWork = unitOfWork;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllContents()
        {
            var contents = await _unitOfWork.Contents.GetAll();

            if (!contents.Any())
                return NotFound("Contents Not Found");

            var contentsDto = contents.Select(ContentMapping.MapToDto).ToList();

            return Ok(contentsDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentById(int id)
        {
            var content = await _unitOfWork.Contents.GetById(id);

            if (content is null)
                return NotFound("Content Not Found");

            var contentDto = ContentMapping.MapToDto(content);

            return Ok(contentDto);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        public async Task<IActionResult> AddContent([FromBody] ContentCreateDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var FileName = await _unitOfWork.Contents.SaveFile(model.NewFile, model.FilePath);
            //var FileName = await _unitOfWork.Contents.SaveFile(model.NewFile);

            var content = ContentMapping.MapToEntity(model);
            content.FilePath = FileName;

            await _unitOfWork.Contents.Add(content);

            // var contentDto = ContentMapping.MapToDto(content);

            await _unitOfWork.CompleteAsync();

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(int id, [FromBody] ContentUpdateDtoRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mainContent = await _unitOfWork.Contents.GetById(id);

            if (mainContent is null)
                return NotFound("Content Not Found");

            var content = ContentMapping.MapToEntity(model);
            content.Id = id;

            var hasNewFile = model.NewFile != null;
            var oldFile = mainContent.FilePath;

            if (hasNewFile)
                content.FilePath = await _unitOfWork.Contents.SaveFile(model.NewFile!, model.FilePath!);

            var result = await _unitOfWork.Contents.Update(content);

            if (result is null)
            {
                ModelState.AddModelError("Error", "Problem When Updating");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }


            var effectedRows = await _unitOfWork.CompleteAsync();

            var updateFileResult = _unitOfWork.Contents.UpdateFile(effectedRows, hasNewFile, oldFile, content.FilePath);

            if (!updateFileResult)
            {
                ModelState.AddModelError("Error", "Problem When Updating File");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            return Ok(model);
        }

        [Authorize(Roles = "Admin, Creator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var isDeleted = false;

            var mainContent = await _unitOfWork.Contents.GetById(id);

            if (mainContent is null)
                return NotFound("Content Not Found");

            var result = await _unitOfWork.Contents.Delete(id);

            if (result is false)
            {
                ModelState.AddModelError("Error", "Problem When Deleting");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            var effectedRows = await _unitOfWork.CompleteAsync();

            var DeleteFileResult = _unitOfWork.Contents.DeleteFile(effectedRows, mainContent.FilePath);

            if (!DeleteFileResult)
            {
                ModelState.AddModelError("Error", "Problem When Deleting Image In Server");
                return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
            }

            return Ok(result);
        }
    }
}


