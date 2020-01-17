using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyBlogApp.Models;

namespace MyBlogApp.Entity
{
    public class BlogRepository : IBlogRepository
    {
        private BlogContext _context;
        public BlogRepository(BlogContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public Blog GetBlog(int id)
        {
            var blog = _context.Blogs.Where(i => i.Id==id).FirstOrDefault();
            return blog;
        }

        public List<Blog> GetBlogs()
        {
            var blogs = _context.Blogs.ToList();
            return blogs;                    
        }

        public List<Blog> GetBlogsWordKey(string word)
        {
            var blogsWord = _context.Blogs.Where(i => i.BlogName.Contains(word)).ToList();
            return blogsWord;
        }

        public List<Blog> GetCategoryBlogs(int id)
        {
            var kategoriBlog = _context.Blogs.Where(i => i.CategoryId == id).ToList();
            return kategoriBlog;
        }
        public List<Blog> GetBlogDate(int date)
        {
            var blogs = _context.Blogs.Where(i => i.BlogDate.Year == date).ToList();
            return blogs;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

       
    }
}
