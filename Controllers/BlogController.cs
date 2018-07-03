using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using MyBlog.Infrastructure;
using MyBlog.Services;
using MyBlog.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace MyBlog.Controllers
{
    /// <summary>
    /// Controller for blog content management
    /// </summary>
    [Authorize]
    public class BlogController : Controller
    {
        /// <summary>
        /// Manage user data
        /// </summary>
        private readonly IUserManagement _userManagement;

        /// <summary>
        /// Blog database context
        /// </summary>
        private readonly IDatabaseProvider _databaseProvider;

        /// <summary>
        /// Format post content
        /// </summary>
        private readonly IFormatContent _formatContent;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="userManagement">Manage user data</param>
        /// <param name="databaseProvider">Blog database context</param>
        public BlogController(IUserManagement userManagement, IDatabaseProvider databaseProvider, IFormatContent formatContent)
        {
            _userManagement = userManagement;
            _databaseProvider = databaseProvider;
            _formatContent = formatContent;
        }

        /// <summary>
        /// Displays view with list of posts
        /// </summary>
        /// <returns>View with list of blog posts</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<PostViewModel> allPosts = await _databaseProvider.GetAllPosts();

            return View(allPosts);
        }

        /// <summary>
        /// Dsiplayes view with post search results
        /// </summary>
        /// <param name="searchString">Search string</param>
        /// <returns>View with post search results</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            if(string.IsNullOrEmpty(searchString))
            {
                return View(new List<PostViewModel>());
            }
            else
            {
                List<PostViewModel> selectedPosts = await _databaseProvider.FindPosts(searchString);

                return View(selectedPosts);
            }
        }


        /// <summary>
        /// Displays view with posts selected by tag
        /// </summary>
        /// <param name="tag">Tag name to be searched</param>
        /// <returns>View with post search results by post tags</returns>
        [AllowAnonymous]
        public async Task<IActionResult> SearchByTag(string tag)
        {
            if(string.IsNullOrEmpty(tag))
                return View("Index", new List<PostViewModel>());
            
            List<PostViewModel> selectedPosts = await _databaseProvider.GetByTag(tag);

            return View("Index", selectedPosts);
        }

        /// <summary>
        /// Displays view with post details
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>View with post details</returns>
        [AllowAnonymous]
        [AcceptVerbs("GET", "HEAD", Route = "Details/{slug}")]
        [AcceptVerbs("GET", "HEAD", Route = "Blog/Details/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            if(slug == String.Empty)
            {
                return NotFound();
            }

            var postDetails = await _databaseProvider.GetPostBySlug(slug);

            postDetails.Content = _formatContent.FormatXMLBlocks(postDetails.Content, "[xmldata]", "[/xmldata]");
            postDetails.Content = _formatContent.NewLineToHTML(postDetails.Content);

            if(postDetails == null)
            {
                return NotFound();
            }

            return View(postDetails);
        }

        /// <summary>
        /// Displays view with create new post form
        /// </summary>
        /// <returns>View with create new post form</returns>
        public async Task<IActionResult> Create()
        {
            var user = await _userManagement.FindUserByPrincipal(HttpContext.User);

            var model = new PostViewModel() {Author = user.FirstName + " " + user.LastName, DatePublished = DateTime.Now};

            return View(model);
        }


        /// <summary>
        /// Method adds post to the database
        /// </summary>
        /// <param name="model">Post view model</param>
        /// <returns>Redirectes to the blog home page or return view with validation summary in case of failure</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel model)
        {
            if(ModelState.IsValid)
            {
                await _databaseProvider.AddPost(model);

                return Redirect(nameof(Index));
            }

            return View(model);
        }

        /// <summary>GetUserByPrincipal
        /// Displayes view with post data to edit
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>View with post data to edit</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postToEdit = await _databaseProvider.GetPostById(id);

            if (postToEdit == null)
            {
                return NotFound();
            }

            return View(postToEdit);
        }


        /// <summary>
        /// Method saves changes in database
        /// </summary>
        /// <param name="model">Post view model</param>
        /// <returns>Redirects to blog index page in case of success or displays edit view in case of invalid model state</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel model)
        {
            if(model == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await _databaseProvider.UpdatePost(model);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        /// <summary>
        /// Displays view fith post delete confirmation form
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>View with post id to be deleted</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postToDelete = await _databaseProvider.GetPostById(id);

            if (postToDelete == null)
            {
                return NotFound();
            }

            return View(postToDelete);
        }

        /// <summary>
        /// Removes post from database
        /// </summary>
        /// <param name="id">Post Id</param>
        /// <returns>Redirects to blog index page on success</returns>
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _databaseProvider.DeletePost(id);

            return RedirectToAction(nameof(Index));
        }
    }
}