using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyBlog.Services
{
    /// <summary>
    /// Interface defines operations on blog database
    /// </summary>
    public interface IDatabaseProvider
    {
        /// <summary>
        /// Gets the roles definned by Identity framework in database
        /// </summary>
        DbSet<IdentityRole> Roles {get;}

        /// <summary>
        /// Adds new post to database
        /// </summary>
        /// <param name="model">Single post model</param>
        /// <returns>Returns number of objects written to the database</returns>
        Task<int> AddPost(PostViewModel entity);

        /// <summary>
        /// Updates single post in database
        /// </summary>
        /// <param name="model">Single post model</param>
        /// <returns>Returns number of objects written to the database</returns>
        Task<int> UpdatePost(PostViewModel entity);

        /// <summary>
        /// Deletes single post by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Returns number of objects written to the database</returns>
        Task<int> DeletePost(int? id);

        /// <summary>
        /// Method ensures that dabase has been creaed
        /// </summary>
        /// <returns>True when created or already exists, false in other case</returns>
        bool EnsureCreated();

        /// <summary>
        /// Gets all blog posts from the database
        /// </summary>
        /// <returns>All posts from database ordered by publishing date</returns>
        Task<List<PostViewModel>> GetAllPosts();

        /// <summary>
        /// Gets all blogs post that content maches the search string
        /// </summary>
        /// <param name="searchString">Search phrase</param>
        /// <returns>All blogs post that content maches the search string</returns>
        Task<List<PostViewModel>> FindPosts(string searchString);

        /// <summary>
        /// Gets list of most current blog posts
        /// </summary>
        /// <param name="count">Number of posts to be returned from the databae </param>
        /// <returns>List of most current blog posts</returns>
        Task<List<PostViewModel>> GetMostRecentPosts(int range);

        /// <summary>
        /// Gest single post by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Single post by id</returns>
        Task<PostViewModel> GetPostById(int? id);    

        /// <summary>
        /// Gets single post by slug
        /// </summary>
        /// <param name="slug">Post slug</param>
        /// <returns>Single post by slug</returns>
        Task<PostViewModel> GetPostBySlug(string slug);
    }
}