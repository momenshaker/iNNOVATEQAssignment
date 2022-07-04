using App.Application.Interfaces;
using App.Core.Domain.Result;
using iNNOVATEQAssignment.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iNNOVATEQAssignment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaServices;
        public MediaController(IMediaService mediaServices)
        {
            _mediaServices = mediaServices;
        }

        /// <summary>
        /// Post File to upload it into directory
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                var result = await _mediaServices.UploadMedia(file, "\\Images\\");
                result.StatusCode = App.Core.Enums.StatusCode.Success;
                return result.ConvertToActionResult(this);
            }
            else
            {
                var result = new Result<int>(0);
                result.StatusCode = App.Core.Enums.StatusCode.BadRequest;
                result.ErrorMessages = new List<string>() { "No File Found" };
                return result.ConvertToActionResult(this);
            }
        }
        /// <summary>
        /// Get Images by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetImage(int? id)
        {
            if (id != null)
            {
                var result = await _mediaServices.GetMedia((int)id);
                result.StatusCode = App.Core.Enums.StatusCode.Success;
                return result.ConvertToActionResult(this);
            }
            else
            {
                var result = new Result<int>(0);
                result.StatusCode = App.Core.Enums.StatusCode.BadRequest;
                result.ErrorMessages = new List<string>() { "No File Found" };
                return result.ConvertToActionResult(this);

            }

        }
    }
}
