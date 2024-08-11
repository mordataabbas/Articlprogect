using ArticlProgect.Core;
using ArticlProgect.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ArticlProgect.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IDataHelper<AuthorPost> dataHelper;
        private readonly IDataHelper<Author> dataHelperForAuthor;
        private readonly IDataHelper<category> dataHelperForcategory;
        private readonly IWebHostEnvironment webHost;
        private readonly IAuthorizationService authorizationService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly Code.FilesHelper filesHelper;
        private int pageItem;
        private Task<AuthorizationResult> result;
        private string UserId;

        public PostController(
            IDataHelper<AuthorPost> dataHelper,
            IDataHelper<Author> dataHelperForAuthor,
             IDataHelper<category> dataHelperForcategory,
            IWebHostEnvironment webHost,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser>userManager,
            SignInManager<IdentityUser>signInManager
            )
        {
            this.dataHelper = dataHelper;
            this.dataHelperForAuthor = dataHelperForAuthor;
            this.dataHelperForcategory = dataHelperForcategory;
            this.webHost = webHost;
            this.authorizationService = authorizationService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            filesHelper = new Code.FilesHelper(this.webHost);
            pageItem = 10;
          
        }
        // GET: PostController
        public ActionResult Index(int? id)
        {
            SetUser();
            if(result.Result.Succeeded)
            {
                // Admin
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetAllData().Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetAllData().Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }
            }
            else
            {
                if (id == 0 || id == null)
                {
                    return View(dataHelper.GetDataByUser(UserId).Take(pageItem));
                }
                else
                {
                    var data = dataHelper.GetDataByUser(UserId).Where(x => x.Id > id).Take(pageItem);
                    return View(data);
                }
            }
        
            // user id
        }
        public ActionResult search(string searchItem)
        {
            SetUser();
            if (result.Result.Succeeded )
            {
                if (searchItem == null)
                {
                    return View("Index", dataHelper.GetAllData());
                }
                else
                {
                    return View("Index", dataHelper.Search(searchItem));
                }
            }
            else
            {
                if (searchItem == null)
                {
                    return View("Index", dataHelper.GetDataByUser(UserId));
                }
                else
                {
                    return View("Index", dataHelper.Search(searchItem).Where(x => x.UserId == UserId).ToList());
                }
            }
        

        }


        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            SetUser();
            return View(dataHelper.Find(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            SetUser();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoreView.AuthorPostView collection)
        {
            SetUser();
            try
            {
                var url = filesHelper.UploadFile(collection.PostImageUrl,"wwwroot\\Images");
                var Post = new AuthorPost
                {
                    AddedDate=DateTime.Now,
                    Author = collection.Author,
                   AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                    category = collection.category,
                    CategoryId = dataHelperForcategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                    FullNamee = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullNamee).First(),
                    PostCategory = collection.PostCategory,
                   PostDescription = collection.PostDescription,
                   PostTitle= collection.PostTitle,
                  UserId = UserId,
                   UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                    PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "wwwroot\\Images")

                };
                dataHelper.Add(Post);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {

                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            var authorpost = dataHelper.Find(id);
            CoreView.AuthorPostView authorPostView = new CoreView.AuthorPostView
            {
                AddedDate = authorpost.AddedDate,
                Author = authorpost.Author,
                AuthorId = authorpost.AuthorId,
                category = authorpost.category,
                CategoryId = authorpost.CategoryId,
                FullNamee = authorpost.FullNamee,
                PostCategory = authorpost.PostCategory,
                PostDescription = authorpost.PostDescription,
                PostTitle = authorpost.PostTitle,
                UserId = authorpost.UserId,
                UserName = authorpost.UserName,
                Id = authorpost.Id
            };
                return View(authorPostView);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoreView.AuthorPostView collection)
        {
            try
            {
                SetUser();
              
                    var Post = new AuthorPost
                    {
                        AddedDate = DateTime.Now,
                        Author = collection.Author,
                        AuthorId = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.Id).First(),
                        category = collection.category,
                        CategoryId = dataHelperForcategory.GetAllData().Where(x => x.Name == collection.PostCategory).Select(x => x.Id).First(),
                        FullNamee = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.FullNamee).First(),
                        PostCategory = collection.PostCategory,
                        PostDescription = collection.PostDescription,
                        PostTitle = collection.PostTitle,
                        UserId = UserId,
                        UserName = dataHelperForAuthor.GetAllData().Where(x => x.UserId == UserId).Select(x => x.UserName).First(),
                        PostImageUrl = filesHelper.UploadFile(collection.PostImageUrl, "wwwroot\\Images"),
                        Id=collection.Id 

                    };
                    dataHelper.Edit(id,Post);
                    return RedirectToAction(nameof(Index));
                }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(dataHelper.Find(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AuthorPost  collection)
        {
            try
            {
                dataHelper.Delete(id);
                string filepath = "~/Images/" + collection.PostImageUrl;
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void SetUser()
        {
            result = authorizationService.AuthorizeAsync(User, "Admin");
            UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
            
    }
}
