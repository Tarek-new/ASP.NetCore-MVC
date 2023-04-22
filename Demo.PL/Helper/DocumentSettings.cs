using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.IO;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1- Get Located Folder Path==> will be problem if there are images with same name

            var folderPath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",folderName);

            //2- Get FileName

            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";

            // Get FilePath
            var filePath=Path.Combine(folderPath, fileName);

            //Save File as STREAMS

            var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string folderName,string fileName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
            if(File.Exists(folderPath))
            {
                File.Delete(folderPath);
            }
        }
    }
}
