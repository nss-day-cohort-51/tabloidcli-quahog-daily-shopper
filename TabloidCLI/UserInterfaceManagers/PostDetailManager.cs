using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        
        private PostRepository _postRepository;
     
        private int _postId;
        private string _connectionString;
        private Post _post;
       
        
        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, Post post, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
            _post = post;
            _postId = postId;
         
        }
        
        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View Details");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"\nTitle:{post.Title}\nUrl: {post.Url}\nDate Published:{post.PublishDateTime}\nAuthor:{post.Author.Id}\nBlog:{post.Blog.Id}");

        }

       
    }
}

