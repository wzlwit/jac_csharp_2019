using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14PeopleDB
{
    public class Database
    {
        const string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd\Documents\2019-ipd16-dotnet\Day14FirstDB\FirstDB.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(DbConnectionString);
            conn.Open();
        }

        public List<Person> GetAllPeople( /* string orderBy = "Id" */ ) {
            List<Person> list = new List<Person>();
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM People", conn);
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader[0];
                    string name = (string)reader[1];
                    int age = (int)reader[2];
                    list.Add(new Person() { Id = id, Name = name, Age = age });
                }
            }
            return list;
        }

        public int AddPerson(Person person) {
            SqlCommand cmdInsert = new SqlCommand("INSERT INTO People (Name, Age) OUTPUT INSERTED.ID VALUES (@Name, @Age)", conn);
            cmdInsert.Parameters.AddWithValue("Name", person.Name);
            cmdInsert.Parameters.AddWithValue("Age", person.Age);
            int insertId = (int)cmdInsert.ExecuteScalar();
            person.Id = insertId; // we may choose to do it
            return insertId;
        }

        public bool UpdatePerson(Person person) {
            SqlCommand cmdUpdate = new SqlCommand("UPDATE People SET Name=@Name, Age=@Age WHERE Id=@Id", conn);
            cmdUpdate.Parameters.AddWithValue("Id", person.Id);
            cmdUpdate.Parameters.AddWithValue("Name", person.Name);
            cmdUpdate.Parameters.AddWithValue("Age", person.Age);
            int rowsAffected = cmdUpdate.ExecuteNonQuery();
            // Maybe I would prefer to throw SqlException in case row was not found?
            // Problem: if row exists but was updated with the same values then
            // affected rows is still 0, so we'd have to execute select to find record first.
            return rowsAffected > 0;
        }

        public bool DeletePerson(int id) {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM People WHERE Id=@Id", conn);
            cmdDelete.Parameters.AddWithValue("Id", id);
            int rowsAffected = cmdDelete.ExecuteNonQuery();
            return rowsAffected > 0;
        }

    }
}
