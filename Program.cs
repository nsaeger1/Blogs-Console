using NLog;
using BlogsConsole.Models;
using System;
using System.Collections;
using System.ComponentModel.Design;
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
                    Console.WriteLine("9) Exit");
                    Console.Write("Enter your selection: ");
                    choice = PromptForChoice();

                    switch (choice)
                    {
                        case 1:
                            // Display all Blogs from the database
                            Console.WriteLine("All blogs in the database:");
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.BlogId}) {item.Name}");
                            }

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
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.BlogId}) {item.Name}");
                            }

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
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.BlogId}) Posts from {item.Name}");
                            }

                            Console.Write("Select a blog: ");
                            requestedBlog = PromptForChoice();
                            if (requestedBlog == 0)
                            {
                                foreach (var item in posts)
                                {
                                    Console.WriteLine($"Blog: {item.Blog.Name}");
                                    Console.WriteLine($"Title: {item.Title}");
                                    Console.WriteLine($"Content: {item.Content}");
                                }
                            }
                            else
                            {
                                var selectedBlog = posts.Where(b => b.BlogId == requestedBlog);

                                
                                    foreach (var thing in selectedBlog)
                                    {
                                        Console.WriteLine($"Blog: {thing.Blog.Name}");
                                        Console.WriteLine($"Title: {thing.Title}");
                                        Console.WriteLine($"Content: {thing.Content}");  
                                    }
                                
                            }

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

        private static int PromptForChoice()
        {
            return int.Parse(Console.ReadLine());
        }
    }
}