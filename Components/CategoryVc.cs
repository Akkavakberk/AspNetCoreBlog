using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Entity;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Components
{
    public class CategoryVc:ViewComponent
    {
        private ICategoryRepository _categoryRespository;
        private IMapper _mapper;
        public CategoryVc(ICategoryRepository categoryRespository, IMapper mapper)
        {
            _categoryRespository = categoryRespository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var kategoriler = _categoryRespository.GetCategories();
            var kategoriMap = _mapper.Map<List<CategoryModel>>(kategoriler);
            return View(kategoriMap);
        }
    }
}
