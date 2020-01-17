using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.helper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Blog, BlogModel>();
            CreateMap<Category, CategoryModel>();
            
        }
    }
}
