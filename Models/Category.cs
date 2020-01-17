using System.Collections.Generic;

namespace MyBlogApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public List<Blog> Blogg { get; set; }
    }
}