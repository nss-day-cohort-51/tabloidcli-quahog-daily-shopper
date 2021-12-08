using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Journal> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                                     VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", entry.CreateDateTime.ToShortDateString());
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Journal entry)
        {
            throw new NotImplementedException();
        }
    }
}
