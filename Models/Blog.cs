using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlogApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string BlogUrl { get; set; }
        public string BlogName { get; set; }
        public string Image { get; set; }
        public DateTime BlogDate { get; set; }
        public string BlogDescription { get; set; }
        public string BlogContent { get; set; }

        public Category category { get; set; }

    }
}
