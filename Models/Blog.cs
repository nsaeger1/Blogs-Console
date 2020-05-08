using System.Collections.Generic;

namespace BlogsConsole.Models 
{
    public class Blog
    {
        
        public int BlogId { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }

        public Blog(string name)
        {
            Name = name;
        }
        public Blog() {}
    }
}
