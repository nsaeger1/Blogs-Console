namespace BlogsConsole.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        
        public Post() {}

        public Post(string title, string content, int blogId)
        {
            Title = title;
            Content = content;
            BlogId = blogId;
        }

        public Post(string title, string content, int blogId, Blog blog)
        {
            Title = title;
            Content = content;
            BlogId = blogId;
            Blog = blog;
        }
    }
}
