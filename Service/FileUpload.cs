using Hotel.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly NavigationManager nav;
        public FileUpload(IWebHostEnvironment webHostEnvironment, NavigationManager nav)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.nav = nav;
        }

        public bool DeleteFile(string fileName)
        {
           
            try
            {
                var path = $"{webHostEnvironment.WebRootPath}\\RoomImages\\{fileName}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            try
            {
                FileInfo FileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + FileInfo.Extension;
                var folderDirectory = $"{webHostEnvironment.WebRootPath}\\RoomImages";
                var path = Path.Combine(webHostEnvironment.WebRootPath,"RoomImages",fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream(6300000).CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using(var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    
                    memoryStream.WriteTo(fs);
                    
                }

                var fullPath = $"{nav.BaseUri}RoomImages/{fileName}";
                return fullPath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
