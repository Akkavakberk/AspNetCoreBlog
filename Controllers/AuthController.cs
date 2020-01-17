using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Entity;
using MyBlogApp.Identity;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    [Authorize] //id:berk pass:123456789
    public class AuthController : Controller
    {
        private UserManager<AppIdentityUser> _userManager;
        private SignInManager<AppIdentityUser> _signInManager;
        private IBlogRepository _blogRespository;
        private IMapper _mapper;
        public AuthController(IMapper mapper, IBlogRepository blogRespository, UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {

            _blogRespository = blogRespository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
             
        public IActionResult Index()
        {
            var bloglar = _blogRespository.GetBlogs();
            var bloglarMap = _mapper.Map<List<BlogModel>>(bloglar);
            return View(bloglarMap);

        }
        public IActionResult BlogRemove(int id)
        {
            var blog = _blogRespository.GetBlog(id);
            _blogRespository.Delete(blog);
            _blogRespository.SaveAll();
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet("Auth/BlogAdd")]
        public IActionResult BlogAdd()
        {
            return View();
        }

        [HttpPost("Auth/BlogAdd")]
        public IActionResult BlogAdd(Blog blog)
        {
            _blogRespository.Add(blog);
            _blogRespository.SaveAll();
            return RedirectToAction("Index", "Auth");
        }


        [HttpGet("Auth/BlogEdit/{id:int}")]
        public IActionResult BlogEdit(int id)
        {
            var blog = _blogRespository.GetBlog(id);
            var blogMap = _mapper.Map<BlogModel>(blog);
            return View(blogMap);
        }
        [HttpPost("Auth/BlogEdit/{id:int}")]
        public IActionResult BlogEdit(Blog blog) 
        {          
            _blogRespository.Update(blog);
            _blogRespository.SaveAll();
            return RedirectToAction("Index", "Auth");        
        }
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(AppRegisterModel appRegisterModel)
        {

            if (!ModelState.IsValid)
            {
                return View(appRegisterModel);
            }

            var user = new AppIdentityUser
            {
                UserName= appRegisterModel.UserName

            };
            var result = await _userManager.CreateAsync(user, appRegisterModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Auth");
            }

            return View(appRegisterModel);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
      
        [HttpPost("Auth/Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(AppLoginModel appLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(appLoginModel);
            }
          
            var result = await _signInManager.PasswordSignInAsync(appLoginModel.UserName, appLoginModel.Password,false, true);

            if (result.Succeeded)
            {
                return RedirectToAction("Index","Auth");
            }
            ModelState.AddModelError(String.Empty, "Login failed");
            return View(appLoginModel);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}