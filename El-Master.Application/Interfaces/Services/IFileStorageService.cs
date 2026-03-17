using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Master.Application.Interfaces.Services
{
    public interface IFileStorageService
    {
        Task<string> UploadVideoAsync(IFormFile file);

        Task<List<string>> UploadAttachmentsAsync(List<IFormFile> files);

        void DeleteFile(string path);
    }
}
