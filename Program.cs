using NLog;
using BlogsConsole.Models;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                int choice;
                do
                {
                    var db = new BloggingContext();
                    var query = db.Blogs;
                    var posts = db.Posts;
                    Console.WriteLine("1) Display Blogs");
                    Console.WriteLine("2) Add Blog");
                    Console.WriteLine("3) Create Post");
                    Console.WriteLine("4) Display Post");
                    Console.WriteLine("5) Edit Blog");
                    Console.WriteLine("6) Delete Blog");
                    Console.WriteLine("7) Edit Post");
                    Console.WriteLine("8) Delete Post");
                    Console.WriteLine("9) Exit");
                    Console.Write("Enter your selection: ");
                    choice = PromptForChoice();


                    switch (choice)
                    {
                        case 1:
                            // Display all Blogs from the database
                            Console.WriteLine("All blogs in the database:");
                            BlogsQuery();

                            break;
                        case 2:
                            string name;
                            do
                            {
                                Console.Write("Enter a name for a new Blog: ");
                                name = Console.ReadLine();
                            } while (name.Length <= 0);

                            var blog = new Blog(name);
                            db.AddBlog(blog);
                            logger.Info("Blog added - {name}", name);
                            break;
                        case 3:
                            int requestedBlog;
                            string postTitle = "", postContent = "";
                            BlogsQuery();

                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            //Blog selectedBlog = query.Where(b => b.BlogId == requestedBlog);
                            while (postTitle.Length < 1)
                            {
                                Console.Write("Enter a Post Title: ");
                                postTitle = Console.ReadLine();
                            }

                            while (postContent.Length < 1)
                            {
                                Console.Write("Enter Post Content: ");
                                postContent = Console.ReadLine();
                            }

                            Post post = new Post(postTitle, postContent, requestedBlog);

                            db.AddPost(post);


                            break;
                        case 4:
                            Console.WriteLine("0) Posts from all blogs");
                            BlogsQuery();
                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            PostsQuery(requestedBlog);

                            break;
                        case 5:
                            BlogsQuery();
                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            var editingBlog = query.FirstOrDefault(b => b.BlogId == requestedBlog);
                            string newBlogName;
                            do
                            {
                                Console.Write("Enter a new name for  {0}: ", editingBlog.Name);
                                newBlogName = Console.ReadLine();
                            } while (newBlogName.Length <= 0);

                            db.EditBlog(editingBlog, newBlogName);
                            break;
                        case 6:
                            BlogsQuery();
                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            var removingBlog = query.FirstOrDefault(b => b.BlogId == requestedBlog);
                            db.RemoveBlog(removingBlog);
                            break;
                        case 7:
                            BlogsQuery();
                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            PostsQuery(requestedBlog);
                            Console.Write("Select a post: ");
                            int requestedPost = PromptForChoice();
                            var editingPost = posts.FirstOrDefault(p => p.PostId == requestedPost);
                            Console.Write("Enter a new Title for {0} or leave blank to keep current: ",
                                editingPost.Title);
                            string newPostTitle = Console.ReadLine();
                            Console.Write("Enter new Content for {0} or leave blank to keep current: ",
                                editingPost.Title);
                            string newPostContent = Console.ReadLine();
                            db.EditPost(editingPost, newPostTitle, newPostContent);
                            break;
                        case 8:
                            BlogsQuery();
                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            PostsQuery(requestedBlog);
                            Console.Write("Select a post: ");
                            requestedPost = PromptForChoice();
                            var removingPost = posts.FirstOrDefault(p => p.PostId == requestedPost);
                            db.RemovePost(removingPost);
                            break;

                        case 9:
                            Console.WriteLine("Goodbye");
                            logger.Info("Program ended");
                            Environment.Exit(0);
                            break;
                    }
                } while (choice != 9);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private static void PostsQueryTest(int requestedBlog)
        {
            throw new NotImplementedException();
        }

        private static void BlogsQuery()
        {
            var blogs = new BloggingContext().Blogs;
            foreach (var blog in blogs)
            {
                Console.WriteLine($"{blog.BlogId}) {blog.Name}");
            }
        }

        private static void PostsQuery(int requestedBlog)
        {
            var blogs = new BloggingContext().Blogs;
            var posts = new BloggingContext().Posts;

            if (requestedBlog == 0)
            {
                foreach (var blog in blogs)
                {
                    foreach (var post in posts)
                    {
                        Console.WriteLine($"Blog: {blog.Name}");
                        Console.WriteLine($"Title: {post.Title}");
                        Console.WriteLine($"Content: {post.Content}");
                    }
                }
            }
            else
            {
                var selectedBlog = posts.Where(b => b.BlogId == requestedBlog);


                foreach (var post in selectedBlog)
                {
                    Console.WriteLine($"Post ID: {post.PostId}");
                    Console.WriteLine($"Title: {post.Title}");
                    Console.WriteLine($"Content: {post.Content}");
                }
            }
        }


        private static int PromptForChoice()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}