using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;
        private Post _post;
        private int _postId;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, Post post, int postId)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
            _post = post;
            _postId = postId;
            _noteRepository.postId = postId;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Go Back");
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void Add()
        {
            Console.WriteLine("New Note");
            Note note = new Note();
            Console.Write("Title: ");
            note.Title = Console.ReadLine();
            Console.Write("Content: ");
            note.Content = Console.ReadLine();
            note.CreateDateTime = DateTime.Now;
            note.PostId = _postId;
            _noteRepository.Insert(note);
        }
        private void List()
        {
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                Console.WriteLine(note.Title);
            }
        }
    }
}
