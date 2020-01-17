using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlogApp.Entity;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    
    public class HomeController : Controller
    {
       
        private IBlogRepository _blogRespository;
        private IMapper _mapper;
        public HomeController(IMapper mapper, IBlogRepository blogRespository)
        {
            _blogRespository = blogRespository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var bloglar = _blogRespository.GetBlogs();
            var bloglarMap = _mapper.Map<List<BlogModel>>(bloglar);
            return View(bloglarMap);
        }
        
    }
}
