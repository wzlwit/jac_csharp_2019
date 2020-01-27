using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16TodosDB
{
    public class Database
    {
        const string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd\Documents\2019-ipd16-dotnet\Day16TodosDB\TodosDB.mdf;Integrated Security=True;Connect Timeout=30";

        private SqlConnection conn;

        public Database()
        {
            conn = new SqlConnection(DbConnectionString);
            conn.Open();
        }

        public List<Todo> GetAllTodos(string orderBy = "Id")
        {
            List<Todo> list = new List<Todo>();
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM Todos ORDER BY " + orderBy, conn);
            using (SqlDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string task = (string)reader[1];
                    DateTime dueDate = (DateTime)reader[2];
                    ETaskStatus isDone = (ETaskStatus)Enum.Parse(typeof(ETaskStatus), (string)reader[3]); // ???
                    list.Add(new Todo() { Id = id, Task = task, DueDate = dueDate, IsDone = isDone });
                }
            }
            return list;
        }

        public int AddTodo(Todo Todo)
        {
            SqlCommand cmdInsert = new SqlCommand("INSERT INTO Todos (Task, DueDate, IsDone) OUTPUT INSERTED.ID VALUES (@Task, @DueDate, @IsDone)", conn);
            cmdInsert.Parameters.AddWithValue("Task", Todo.Task);
            cmdInsert.Parameters.AddWithValue("DueDate", Todo.DueDate);
            cmdInsert.Parameters.AddWithValue("IsDone", Todo.IsDone.ToString()); // ???
            int insertId = (int)cmdInsert.ExecuteScalar();
            Todo.Id = insertId; // we may choose to do it
            return insertId;
        }

        public bool UpdateTodo(Todo Todo)
        {
            SqlCommand cmdUpdate = new SqlCommand("UPDATE Todos SET Task=@Task, DueDate=@DueDate, IsDone=@IsDone WHERE Id=@Id", conn);
            cmdUpdate.Parameters.AddWithValue("Task", Todo.Task);
            cmdUpdate.Parameters.AddWithValue("DueDate", Todo.DueDate);
            cmdUpdate.Parameters.AddWithValue("IsDone", Todo.IsDone); // ???
            int rowsAffected = cmdUpdate.ExecuteNonQuery();
            // Maybe I would prefer to throw SqlException in case row was not found?
            // Problem: if row exists but was updated with the same values then
            // affected rows is still 0, so we'd have to execute select to find record first.
            return rowsAffected > 0;
        }

        public bool DeleteTodo(int id)
        {
            SqlCommand cmdDelete = new SqlCommand("DELETE FROM Todos WHERE Id=@Id", conn);
            cmdDelete.Parameters.AddWithValue("Id", id);
            int rowsAffected = cmdDelete.ExecuteNonQuery();
            return rowsAffected > 0;
        }

    }
}
