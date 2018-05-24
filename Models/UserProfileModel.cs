using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class UserProfileModel
    {
        /// <summary>
        /// Gets or sets the user unique id
        /// </summary>
        /// <returns>User unique id</returns>
        public string Id {get; set;}


        /// <summary>
        /// Gets or sets the user e-mail address
        /// </summary>
        /// <returns>User e-mail address</returns>
        [Display(Name = "E-mail")]
        public string Email {get; set;}

        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        /// <returns>User first name</returns>
        [Display(Name = "First name")]
        public string FirstName {get; set;}

        /// <summary>
        /// Gets or sets user last name
        /// </summary>
        /// <returns>User last name</returns>
        [Display(Name = "Last name")]
        public string LastName {get; set;}

        /// <summary>
        /// Gets ors sets the blog author short biography
        /// </summary>
        /// <returns>Blog author description</returns>
        [Display(Name = "About me")]
        public string AboutAuthor {get; set;}

        /// <summary>
        /// Gets or sets the link to the Github profile
        /// </summary>
        /// <returns>Link to the Github profile</returns>
        [Display(Name = "Github profile")]
        public string GithubProfile {get; set;}

        /// <summary>
        /// Gets or sets the link to the Linkedin profile
        /// </summary>
        /// <returns>Link to the Linkedin profile</returns>
        [Display(Name = "Linkedin profile")]
        public string LinkedinProfile {get; set;}

        /// <summary>
        /// Gets or sets the name of the image file with blog author photo
        /// </summary>
        /// <returns>Name of the image file with blog author photo</returns>
        public string ProfilePhoto {get; set;}

        /// <summary>
        /// Gets or sets the image file with user photo
        /// </summary>
        /// <returns>Image file with user photo</returns>
        [NotMapped]
        [Display(Name = "Profile photo")]
        public IFormFile ProfileImageFile {get; set;}
    }
}