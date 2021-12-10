using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
namespace TabloidCLI.UserInterfaceManagers
{
    class PostsManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private string _connectionString;
        public PostsManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 6) Post Details");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3": Edit();
                    return this;
                case "4":
                    Console.WriteLine("Please choose a post:");
                    List<Post> posts = _postRepository.GetAll();
                    for (int i = 0; i < posts.Count; i++)
                    {
                        Post post = posts[i];
                        Console.WriteLine($" {i + 1}) {post.Title}");
                    }
                    Console.Write("> ");
                    string input = Console.ReadLine();
                    try
                    {
                        int pchoice = int.Parse(input);
                        return new NoteManager(this, _connectionString, posts[pchoice - 1], pchoice);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Selection");
                        return this;
                    }
                case "5":
                    Remove();
                    return this;
                case "6":
                    Console.WriteLine("Please choose a post:");
                    List<Post> postsDetails = _postRepository.GetAll();
                    for (int i = 0; i < postsDetails.Count; i++)
                    {
                        Post post = postsDetails[i];
                        Console.WriteLine($" {i + 1}) {post.Title}");
                    }
                    Console.Write("> ");
                    string userInput = Console.ReadLine();
                    try
                    {
                        int uchoice = int.Parse(userInput);
                        return new PostDetailManager(this, _connectionString, postsDetails[uchoice - 1], uchoice);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid Selection");
                        return this;
                    }
              
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"\nTitle:{post.Title}\nUrl: {post.Url}\nDate Published:{post.PublishDateTime}\nAuthor:{post.Author.Id}\nBlog:{post.Blog.Id}");
            }
        }
        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();
            Console.Write("Title: ");
            post.Title = Console.ReadLine();
            Console.Write("Url: ");
            post.Url = Console.ReadLine();
            Console.Write("Date Published: ");
            Console.Write("Enter a month: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Enter a day: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("Enter a year: ");
            int year = int.Parse(Console.ReadLine());
            post.PublishDateTime = new DateTime(year, month, day);
            Console.Write("Author: ");
            var author = new Author();
            author.Id = int.Parse(Console.ReadLine());
            Console.Write("Blog: ");
            var blog = new Blog();
            blog.Id= int.Parse(Console.ReadLine());
            post.Author = author;
            post.Blog = blog;
            _postRepository.Insert(post);
        }
        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }
            Console.WriteLine(prompt);
            List<Post> posts = _postRepository.GetAll();
            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        private void Edit()
        {
            Post postToEdit = Choose("Which post would you like to edit?");
            if (postToEdit == null)
            {
                return;
            }
            Console.Write("New post title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                postToEdit.Title = title;
            }
            Console.Write("New link (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                postToEdit.Url = url;
            }
            Console.Write("New Publish Date (blank to leave unchanged: ");
            DateTime datetime;
            if (DateTime.TryParse(Console.ReadLine(), out datetime))
            {
                postToEdit.PublishDateTime = datetime;
            };
            Console.Write("new blogId (blank to leave unchanged: ");
            int blogid;
            if (int.TryParse(Console.ReadLine(), out blogid))
            {
                postToEdit.Blog.Id = blogid;
            }
            Console.Write("new authorId (blank to leave unchanged: ");
            int authorId;
            if (int.TryParse(Console.ReadLine(), out authorId))
            {
                postToEdit.Author.Id = authorId;
            }
            _postRepository.Update(postToEdit);
        }

        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }
    }
}
