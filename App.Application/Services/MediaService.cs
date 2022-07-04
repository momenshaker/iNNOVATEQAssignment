using App.Application.Interfaces;
using App.Core.Domain.Interfaces.ContextInterface;
using App.Core.Domain.Result;
using App.Core.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class MediaService : IMediaService
    {
        private IHostingEnvironment _environment;
        private IApplicationDbContext _applicationDbContext;


        public MediaService(IHostingEnvironment environment, IApplicationDbContext applicationDbContext)
        {
            _environment = environment;
            _applicationDbContext = applicationDbContext;
        }
        /// <summary>
        /// Upload Image And return Id
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="path"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<Result<int>> UploadMedia(IFormFile formFile, string path)
        {
            string uploads = _environment.WebRootPath + path;
            var Fileext = Path.GetExtension(formFile.FileName);
            var File = Guid.NewGuid() + Fileext;
            string filePath = Path.Combine(uploads, File);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            var mediaItem = new Core.Domain.Entities.Media() { MediaUrl = File };
            _applicationDbContext.Media.Add(mediaItem);
            await _applicationDbContext.SaveChangesAsync();
            var result = new Result<int>()
            {
                Data = mediaItem.Id,
                StatusCode = StatusCode.Success,
                SuccessMessage = "Image Uplaoded Successfully"
            };
            return result;

        }
        /// <summary>
        /// Get Media
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<string>> GetMedia(int id)
        {

            var MediaItem = await _applicationDbContext.Media.FindAsync(id);
            if (MediaItem == null)
            {
                var result = new Result<string>()
                {
                    Data = null,
                    StatusCode = StatusCode.BadRequest,
                    SuccessMessage = "Image Not Found"
                };
                return result;
            }
            else
            {
                var result = new Result<string>()
                {
                    Data = "Images/" + MediaItem.MediaUrl,
                    StatusCode = StatusCode.Success,
                    SuccessMessage = "Image Retrived Successfully"
                };
                return result;
            }


        }
    }
}
