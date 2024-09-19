using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVCFristApp.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile formFile, string folderName)
        {
            // 01: Get the path to the folder
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

            // 02: Ensure the folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 03: Generate a unique filename
            string fileName = $"{Guid.NewGuid()}_{formFile.FileName}";

            // 04: Get the full file path (FolderPath + FileName)
            string filePath = Path.Combine(folderPath, fileName);

            try
            {
                // 05: Save the file as a stream
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                throw new Exception("File upload failed.", ex);
            }

            // 06: Return the file name to be saved in the database
            return fileName;
        }
        public static void DeleteFile(string fileName, string folderName)
        {
            // 01: Get the full file path
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName, fileName);

            // 02: Check if the file exists before trying to delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath); // 03: Delete the file if it exists
            }
            else
            {
                throw new FileNotFoundException("The file does not exist.", fileName);
            }
        }

    }
}
