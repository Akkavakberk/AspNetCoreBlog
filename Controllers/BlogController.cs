using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Entity;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    
    public class BlogController : Controller
    {

        private IBlogRepository _blogRespository;
        private IMapper _mapper;
        public BlogController(IMapper mapper, IBlogRepository blogRespository)
        {
            _blogRespository = blogRespository;
            _mapper = mapper;
        }
      
        [Route("Blog/Details/{id:int}")]
        public IActionResult Details(int id)
        {
            var blog = _blogRespository.GetBlog(id);
            var blogMap= _mapper.Map<BlogModel>(blog);
            return View(blogMap);
        }
        [Route("Blog/ListCategoryBlog/{id:int}")]
        public IActionResult ListCategoryBlog(int id)
        {         
                var list = _blogRespository.GetCategoryBlogs(id);
                var listMap = _mapper.Map<List<BlogModel>>(list);
                return View(listMap);                   
        }
        [Route("Blog/ListBlogForWord")]
        public IActionResult ListBlogForWord(string key)
        {
            var result = _blogRespository.GetBlogsWordKey(key);
            var resultMap = _mapper.Map<List<BlogModel>>(result);
            return View(resultMap);
        }
        [Route("Blog/ListBlogForDate/{id:int}")]
        public IActionResult ListBlogForDate(int id)
        {
            int date = id;
            var blogs = _blogRespository.GetBlogDate(date);
            var blogsMap = _mapper.Map<List<BlogModel>>(blogs);
            return View(blogsMap);
        }

    }
}