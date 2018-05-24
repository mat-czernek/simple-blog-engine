using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyBlog.Models;
using MyBlog.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Globalization;

namespace MyBlog.Services
{
    /// <summary>
    /// Defines operations on database
    /// </summary>
    public class DatabaseProvider : IDatabaseProvider
    {
        /// <summary>
        /// Database context
        /// </summary>
        private readonly BlogDatabaseContext _databaseContext;

        /// <summary>
        /// Gets the roles definned by Identity framework in database
        /// </summary>
        /// <returns>Roles definned by Identity framework in database</returns>
        public DbSet<IdentityRole> Roles{
            get {return _databaseContext.Roles;}
        }

        /// <summary>
        /// Default constructir
        /// </summary>
        /// <param name="databaseContext">Database context</param>
        public DatabaseProvider(BlogDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Gets all blog posts from the database
        /// </summary>
        /// <returns>All posts from database ordered by publishing date</returns>
        public async Task<List<PostViewModel>> GetAllPosts()
        {
            return await _databaseContext.Posts.OrderByDescending(post => post.DatePublished).ToListAsync();
        }

        /// <summary>
        /// Gets all blogs post that content maches the search string
        /// </summary>
        /// <param name="searchString">Search phrase</param>
        /// <returns>All blogs post that content maches the search string</returns>
        public async Task<List<PostViewModel>> FindPosts(string searchString)
        {
            CultureInfo culture = new CultureInfo("en");

            return await _databaseContext.Posts.Where(post => culture.CompareInfo.IndexOf(post.Content, searchString, CompareOptions.IgnoreCase) >= 0).OrderByDescending(post => post.DatePublished).ToListAsync();
        }

        /// <summary>
        /// Gets list of most current blog posts
        /// </summary>
        /// <param name="count">Number of posts to be returned from the databae </param>
        /// <returns>List of most current blog posts</returns>
        public async Task<List<PostViewModel>> GetMostRecentPosts(int count)
        {
            List<PostViewModel> test = await _databaseContext.Posts.OrderByDescending(post => post.DatePublished).Take(count).ToListAsync();

            return await _databaseContext.Posts.OrderByDescending(post => post.DatePublished).Take(count).ToListAsync();
        }

        /// <summary>
        /// Gest single post by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Single post by id</returns>
        public async Task<PostViewModel> GetPostById(int? id)
        {
            return await _databaseContext.Posts.SingleOrDefaultAsync(post => post.PostId == id);
        }


        /// <summary>
        /// Gets single post by slug
        /// </summary>
        /// <param name="slug">Post slug</param>
        /// <returns>Single post by slug</returns>
        public async Task<PostViewModel> GetPostBySlug(string slug)
        {
            return await _databaseContext.Posts.SingleOrDefaultAsync(post => post.Slug == slug);
        }

        /// <summary>
        /// Adds new post to database
        /// </summary>
        /// <param name="model">Single post model</param>
        /// <returns>Returns number of objects written to the database</returns>
        public async Task<int> AddPost(PostViewModel model)
        {
            model.Slug = model.Title.GenerateSlug();

            _databaseContext.Add(model);
            return await _databaseContext.SaveChangesAsync();  
        }


        /// <summary>
        /// Updates single post in database
        /// </summary>
        /// <param name="model">Single post model</param>
        /// <returns>Returns number of objects written to the database</returns>
        public async Task<int> UpdatePost(PostViewModel model)
        {
            var postToEdit = await _databaseContext.Posts.AsNoTracking().Where(post => post.PostId == model.PostId).SingleOrDefaultAsync();
                
            model.Slug = model.Title.GenerateSlug();      

            _databaseContext.Update(model);
            return await _databaseContext.SaveChangesAsync(); 
        }

        /// <summary>
        /// Deletes single post by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Returns number of objects written to the database</returns>
        public async Task<int> DeletePost(int? id)
        {
            var postToDelete = await _databaseContext.Posts.SingleOrDefaultAsync(m => m.PostId == id);

            if (postToDelete == null)
            {
                return 0;
            }

            _databaseContext.Remove(postToDelete);
            return await _databaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Method ensures that dabase has been creaed
        /// </summary>
        /// <returns>True when created or already exists, false in other case</returns>
        public bool EnsureCreated()
        {
            return _databaseContext.Database.EnsureCreated();
        }
    }
}