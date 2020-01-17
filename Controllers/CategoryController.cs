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

    [Route("Category")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRespository;
       
        private IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRespository, IMapper mapper)
        {
            _categoryRespository = categoryRespository;
            _mapper = mapper;

        }
        public IActionResult Index()
        {
            var kategoriler = _categoryRespository.GetCategories();
            var kategoriMap = _mapper.Map<List<CategoryModel>>(kategoriler);
            return View(kategoriMap);
        }
                
    }
}