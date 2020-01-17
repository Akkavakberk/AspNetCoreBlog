using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Entity
{
    public interface IBlogRepository
    {
        void Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        void Update<T>(T entity) where T : class;
        bool SaveAll();
      
        List<Blog> GetBlogs();

        List<Blog> GetBlogDate(int date);

        Blog GetBlog(int id);

        List<Blog> GetCategoryBlogs(int id);

        List<Blog> GetBlogsWordKey(string word);
    }
}
