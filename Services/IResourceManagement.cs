using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Services
{
    /// <summary>
    /// Interface defiens methods used to manage blog resources, like files
    /// </summary>
    public interface IResourceManagement
    {
        /// <summary>
        /// Method uploads user profile photo on server
        /// </summary>
        /// <param name="profilePhotoFile">Handle to file provided by user in profile update form</param>
        /// <param name="fileName">Photo file name builed based on user id</param>
        /// <returns>User profile photo file name</returns>
         Task<string> UploadProfilePhoto(IFormFile profilePhotoFile, string fileName);
    }
}