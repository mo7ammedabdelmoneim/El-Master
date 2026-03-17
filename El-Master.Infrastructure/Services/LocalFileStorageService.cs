using El_Master.Application.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace El_Master.Infrastructure.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment env;

        public LocalFileStorageService(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public async Task<string> UploadVideoAsync(IFormFile file)
        {
            var videosPath = Path.Combine(env.WebRootPath, "uploads", "videos");

            if (!Directory.Exists(videosPath))
                Directory.CreateDirectory(videosPath);

            var extension = Path.GetExtension(file.FileName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var fullPath = Path.Combine(videosPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);

            await file.CopyToAsync(stream);

            return Path.Combine("uploads/videos", fileName).Replace("\\", "/");
        }

        public async Task<List<string>> UploadAttachmentsAsync(List<IFormFile> files)
        {
            var attachmentsPath = Path.Combine(env.WebRootPath, "uploads", "attachments");

            if (!Directory.Exists(attachmentsPath))
                Directory.CreateDirectory(attachmentsPath);

            var paths = new List<string>();

            foreach (var file in files)
            {
                var extension = Path.GetExtension(file.FileName);

                var fileName = $"{Guid.NewGuid()}{extension}";

                var fullPath = Path.Combine(attachmentsPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);

                await file.CopyToAsync(stream);

                paths.Add(Path.Combine("uploads/attachments", fileName).Replace("\\", "/"));
            }

            return paths;
        }

        public void DeleteFile(string path)
        {
            var fullPath = Path.Combine(env.WebRootPath, path);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
