﻿using System;
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
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": List();
                    return this;
                case "2":
                    
                    return this;
                case "3":
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
                Console.WriteLine($"\nTitle:{post.Title}\nUrl: {post.Url}\n");
            }
        }
    }
}
