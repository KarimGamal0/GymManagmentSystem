using GymManagmentBLL.Service.Interfaces.AttachmentService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Service.Classes.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long MaxFile = 5 * 1024 * 1024; //5MB
        private readonly IWebHostEnvironment _webHost;

        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName == null || file == null || file.Length == 0) return null;
                //check file size
                if (file.Length > MaxFile) return null;
                //check extension
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension)) return null;
                //Get located file path
                var folderPath = Path.Combine(_webHost.WebRootPath, "images", folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //create unique name for the file
                var fileName = Guid.NewGuid().ToString() + extension;
                //wwwroot/images/members/45645465154321.jpg
                var filePath = Path.Combine(folderPath, fileName);

                //Create file stream to upload bit by bit
                using var fileStream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(fileStream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload file to folder = {folderName}");
                return null;
            }

        }
        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(folderName)) return false;
                var fullpath = Path.Combine(_webHost.WebRootPath, "images", folderName, fileName);
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file with name {fileName}:{ex}");
                return false;
            }
        }

    }
}
