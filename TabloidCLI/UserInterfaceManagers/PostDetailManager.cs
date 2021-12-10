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
        private TagRepository _tagRepository;
        private int _postId;
        private string _connectionString;
        private Post _post;
       
        
        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, Post post, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
            _post = post;
            _postId = postId;
         
        }
        
        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View Details");
            Console.WriteLine(" 2) Add Tag");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
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
            Console.WriteLine($"\n\nTitle:{post.Title}\n" +
                $"Url: {post.Url}\n" +
                $"Date Published:{post.PublishDateTime}\n" +
                $"Tags: {string.Join(' ', _postRepository.GetTags(post))}\n" +
                $"Author:{post.Author.Id}\n" +
                $"Blog:{post.Blog.Id}");
    }
        private void AddTag()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Which tag would you like to add to {post.Title}?");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.InsertTag(post, tag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't add any tags.");
            }
        }
        private void RemoveTag()
        {
            
            List<Tag> tags = _postRepository.GetTags(_post);
            Console.WriteLine($"Which tag would you like to remove from {_post.Title}?");
 
            for (int i = 0; i < tags.Count; i++)
            {
                Console.WriteLine($" {i + 1}) {tags[i]}");
            }
            Console.Write("> ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.DeleteTag(_post.Id, tag.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
            }
        }
    }
}

