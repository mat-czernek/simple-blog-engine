using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlog.Services;
using MyBlog.Infrastructure;
using MyBlog.Models;
using Microsoft.Extensions.Options;

namespace MyBlog.Controllers
{
    /// <summary>
    /// Controller for home page management
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Blog database context
        /// </summary>
        private readonly IDatabaseProvider _databaseProvider;

        /// <summary>
        /// Default user data stored in json configuration file
        /// </summary>
        private  DefaultUserData _defaultUserData;
        
        /// <summary>
        /// Format post content
        /// </summary>
        private readonly IFormatContent _formatContent;

        /// <summary>
        /// Manage user data
        /// </summary>
        private readonly IUserManagement _userManagement;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="databaseProvider">Blog database context</param>
        /// <param name="userManagement">Manage user data</param>
        /// <param name="defaultUserData">Default user data stored in json configuration file</param>
        /// <param name="formatContent">Format post content</param>
        public HomeController(IDatabaseProvider databaseProvider, IUserManagement userManagement, IOptionsSnapshot<DefaultUserData> defaultUserData, IFormatContent formatContent)
        {
            _databaseProvider = databaseProvider;
            _defaultUserData = defaultUserData.Value;
            _userManagement = userManagement;
            _formatContent = formatContent;
        }

        /// <summary>
        /// Displays view with home page
        /// </summary>
        /// <returns>View with home page</returns>
        public async Task<IActionResult> Index()
        {  
            ApplicationUser user = null;

            /*
                If user is not authenticated then get bloag aouthor data from json file,
                in other case query data from database
             */
            if(!User.Identity.IsAuthenticated)
                user = await _userManagement.FindByEmailAsync(_defaultUserData.Email);
            else
                user = await _userManagement.FindUserByPrincipal(HttpContext.User);
            
            // create model for home view
            HomeViewModel model = new HomeViewModel() {

                RecentPosts = await _databaseProvider.GetMostRecentPosts(5),
                BlogAuthor = user.FirstName + " " + user.LastName,
                GithubProfile = user.GithubProfile,
                LinkedinProfile = user.LinkedinProfile,
                Email = user.Email,
                AboutAuthor = _formatContent.NewLineToHTML(user.AboutAuthor),
                ProfilePhoto = user.ProfilePhoto,
                TagsCloud = await _databaseProvider.GetUniqueTags()
            };

            return View(model);
        }
    }
}