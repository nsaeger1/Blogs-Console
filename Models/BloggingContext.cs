using System.Data.Entity;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext")
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }

        public void EditBlog(Blog blog, string title)
        {
            blog.Name = title;
            this.SaveChanges();
        }

        public void RemoveBlog(Blog blog)
        {
            this.Blogs.Remove(blog);
            this.SaveChanges();
        }

        public void AddPost(Post post)
        {
            this.Posts.Add(post);
            this.SaveChanges();
        }

        public void EditPost(Post post, string title, string content)
        {
            if (!title.Equals(""))
            {
                post.Title = title;
            }

            if (!content.Equals(""))
            {
                post.Content = content;
            }

            this.SaveChanges();
        }

        public void RemovePost(Post post)
        {
            this.Posts.Remove(post);
            this.SaveChanges();
        }
    }
}