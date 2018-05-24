using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;
using MyBlog.Models;
using MyBlog.Services;
using Microsoft.Extensions.Options;

namespace MyBlog.Infrastructure
{
    /// <summary>
    /// Class initalize database with default user data
    /// </summary>
    public class SeedDatabase : ISeedDatabase
    {
        /// <summary>
        /// Blog database context
        /// </summary>
        private readonly IDatabaseProvider _blogDbContext;

        /// <summary>
        /// Manage users data
        /// </summary>
        private readonly IUserManagement _userManagement;

        /// <summary>
        /// Manage users roles
        /// </summary>
        private readonly IRoleManagement _roleManager;

        /// <summary>
        /// Default user data
        /// </summary>
        private readonly DefaultUserData _defaultUserData;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="blogDbContext">Blog database context</param>
        /// <param name="userManager">Provides API to manage users</param>
        /// <param name="roleManager">Provides API to handle user roles assignment</param>
        public SeedDatabase(IDatabaseProvider blogDbContext, IUserManagement userManager, IRoleManagement roleManager, IOptionsSnapshot<DefaultUserData> defaultUserData )
        {
            _blogDbContext = blogDbContext;
            _userManagement = userManager;
            _roleManager = roleManager;
            _defaultUserData = defaultUserData.Value;
        }

        /// <summary>
        /// Method initalize database with default data
        /// </summary>
        /// <remarks>
        /// Do not forget to update your e-mail address, user name and password
        /// </remarks>
        /// <returns></returns>
        public async void Initalize()
        {
            _blogDbContext.EnsureCreated();

            // administrator already exist
            if(_blogDbContext.Roles.Any(r => r.Name == "Administrator")) return;

            await _roleManager.CreateAsync(new IdentityRole("Administrator"));

            await _userManagement.CreateAsync(new ApplicationUser 
                {
                    UserName = _defaultUserData.Email, 
                    Email = _defaultUserData.Email, 
                    EmailConfirmed = true, 
                    FirstName = _defaultUserData.Firstname, 
                    LastName = _defaultUserData.Lastname,
                    AboutAuthor = _defaultUserData.AboutAuthor,
                    LinkedinProfile = _defaultUserData.LinkedinProfile,
                    GithubProfile = _defaultUserData.GithubProfile, 
                    DefaultPasswordChanged = 0,
                    ProfilePhoto = "default_author_photo.png"

                }, _defaultUserData.Password);

            await _userManagement.AddToRoleAsync(await _userManagement.FindByEmailAsync(_defaultUserData.Email), "Administrator");
        }
    }
}