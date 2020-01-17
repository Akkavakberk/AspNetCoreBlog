using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBlogApp.Models;

namespace MyBlogApp.Entity
{
    public class CategoryRepository : ICategoryRepository
    {
        private BlogContext _context;
        public CategoryRepository(BlogContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public List<Category> GetCategories()
        {
            var kategoriler = _context.Categories.ToList();
            return kategoriler;
        }
         
        public bool SaveAll()
        {
            throw new NotImplementedException();
        }
    }
}
