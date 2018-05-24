using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models;

namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Class handles operations on database
    /// </summary>
    public class BlogDatabaseContext : IdentityDbContext<ApplicationUser> 
    {
        /// <summary>
        /// Default construcotr
        /// </summary>
        /// <param name="options">Options to be used in database entity framework</param>
        public BlogDatabaseContext(DbContextOptions<BlogDatabaseContext> options) : base(options)
        {
            
        }

        /// <summary>
        /// Setter to get access to the posts in database
        /// </summary>
        /// <returns>Set ot posts from the database</returns>
        public DbSet<PostViewModel> Posts {get; set;}
    }
}