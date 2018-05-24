using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace MyBlog.Models
{
    /// <summary>
    /// A base class for single post model. This class is mapped to database by Entity Framework
    /// </summary>
    public class PostViewModel
    {
        /// <summary>
        /// Post identifier
        /// </summary>
        /// <returns>Post identifier</returns>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId {get; set;}

        /// <summary>
        /// Post title
        /// </summary>
        /// <returns>Post title</returns>
        public string Title {get; set;}

        /// <summary>
        /// Post content
        /// </summary>
        /// <returns>Post content</returns>
        public string Content {get; set;}

        /// <summary>
        /// Post brief description/summary
        /// </summary>
        /// <returns>Post brief description/summary</returns>
        [Display(Name = "Post abstract")]
        public string Abstract {get; set;}

        /// <summary>
        /// Post author
        /// </summary>
        /// <returns>Post author</returns>
        public string Author {get; set;}
        

        /// <summary>
        /// Post publishing date
        /// </summary>
        /// <returns>Post publishing date in following</returns>
        [Display(Name = "Date published")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatePublished {get; set;}    

        /// <summary>
        /// Post tags
        /// </summary>
        /// <returns>Post tags</returns>
        public string Tags {get; set;}

        /// <summary>
        /// Post slug
        /// </summary>
        /// <returns>Post slug</returns>
        public string Slug {get; set;}
    }
}





