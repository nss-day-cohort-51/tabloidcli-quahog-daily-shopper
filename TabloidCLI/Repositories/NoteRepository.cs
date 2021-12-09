using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Note note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId ) VALUES (@firstName, @lastName, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@firstName", note.Title);
                    cmd.Parameters.AddWithValue("@lastName", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime.ToShortDateString());
                    cmd.Parameters.AddWithValue("@postId", note.PostId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Note Note)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
