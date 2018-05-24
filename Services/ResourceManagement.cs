using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Services
{
    /// <summary>
    /// Class defiens methods used to manage blog resources, like files
    /// </summary>
    public class ResourceManagement : IResourceManagement
    {
        /// <summary>
        /// Method uploads user profile photo on server
        /// </summary>
        /// <param name="profilePhotoFile">Handle to file provided by user in profile update form</param>
        /// <param name="fileName">Photo file name builed based on user id</param>
        /// <returns>User profile photo file name</returns>
         public async Task<string> UploadProfilePhoto(IFormFile profilePhotoFile, string fileName)
         {
            var filePath = string.Empty;

            // prepare path for post image storage
            var uploadDirectoryPath = Path.Combine("wwwroot/resources/general");

            if(profilePhotoFile != null && profilePhotoFile.Length != 0)
            {
                // create directory for post
                if(!Directory.Exists(uploadDirectoryPath))
                    Directory.CreateDirectory(uploadDirectoryPath);
            

                filePath = Path.Combine(uploadDirectoryPath, fileName);
                
                if(System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePhotoFile.CopyToAsync(stream);
                }

            }

            filePath = filePath.Replace("wwwroot", "");

            return filePath;
         }
    }
}