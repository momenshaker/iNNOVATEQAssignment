using App.Core.Domain.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface IMediaService
    {
        Task<Result<int>> UploadMedia(IFormFile formFile, string path);
        Task<Result<string>> GetMedia(int id);
    }
}
