using System.Collections.Generic;
using MyBlog.Infrastructure;
using MyBlog.Services;

namespace MyBlog.Models
{
    /// <summary>
    /// Represents home view model
    /// </summary>
    public class HomeViewModel
    {
        /// <summary>
        /// Gets or sets the list of most recent posts from blog
        /// </summary>
        /// <returns>List of most recent posts from blog</returns>
        public List<PostViewModel> RecentPosts { get; set;}


        /// <summary>
        /// Collection of tags
        /// </summary>
        /// <returns>List of tags</returns>
        public List<string> TagsCloud {get; set;}

        /// <summary>
        /// Gets or sets the first and the last name of the blog author
        /// </summary>
        /// <returns>First and the last name of the blog author</returns>
        public string BlogAuthor {get; set;}

        /// <summary>
        /// Gets or sets the link to the Github profile
        /// </summary>
        /// <returns>Link to the Github profile</returns>
        public string GithubProfile {get; set;}

        /// <summary>
        /// Gets or sets the link to the Linkedin profile
        /// </summary>
        /// <returns>Link to the Linkedin profile</returns>
        public string LinkedinProfile {get; set;}

        /// <summary>
        /// Gets or sets the blog author e-mail
        /// </summary>
        /// <returns>Blog author e-mail</returns>
        public string Email {get; set;}

        /// <summary>
        /// Gets ors sets the blog author short biography
        /// </summary>
        /// <returns>Blog author description</returns>
        public string AboutAuthor {get; set;}

        /// <summary>
        /// Gets or sets the name of the image file with blog author photo
        /// </summary>
        /// <returns>Name of the image file with blog author photo</returns>
        public string ProfilePhoto {get; set;}
    }
    
}