using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14FirstDB
{
    class Program
    {
        const string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ipd\Documents\2019-ipd16-dotnet\Day14FirstDB\FirstDB.mdf;Integrated Security=True;Connect Timeout=30";

        static SqlConnection conn;

        static void Main(string[] args)
        {
            try
            {
                conn = new SqlConnection(DbConnectionString);
                conn.Open();
                // insert example
                try
                {
                    Console.Write("Enter person name: ");
                    string name = Console.ReadLine();
                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO People (Name, Age) OUTPUT INSERTED.ID VALUES (@Name, @Age)", conn);
                    cmdInsert.Parameters.AddWithValue("Name", name);
                    cmdInsert.Parameters.AddWithValue("Age", new Random().Next(100));
                    int insertId = (int) cmdInsert.ExecuteScalar();
                    Console.WriteLine("Record inserted successfully with id=" + insertId);
                } catch (Exception ex)
                {
                    if (ex is InvalidCastException || ex is SqlException)
                    {
                        Console.WriteLine("Error executing query: " + ex.Message);
                    } else
                    {
                        throw ex;
                    }
                }
                /* // select example
                try
                {
                    SqlCommand cmdSelect = new SqlCommand("SELECT * FROM People", conn);
                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = (int)reader[0];
                            string name = (string)reader[1];
                            int age = (int)reader[2];
                            Console.WriteLine("Person({0}): {1} is {2} y/o", id, name, age);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidCastException || ex is SqlException)
                    {
                        Console.WriteLine("Error executing query: " + ex.Message);
                    }
                    else
                    {
                        throw ex;
                    }
                } */

                /* // update below - ask user for record ID and new name, then update
                try {
                    Console.Write("UPDATING: Enter existing record id: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("UPDATING: Enter new name: ");
                    string name = Console.ReadLine();
                    SqlCommand cmdUpdate = new SqlCommand("UPDATE People SET Name=@Name WHERE Id=@Id", conn);
                    cmdUpdate.Parameters.AddWithValue("Id", id);
                    cmdUpdate.Parameters.AddWithValue("Name",name);
                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected >= 1)
                    {
                        Console.WriteLine("Record updated");
                    } else
                    {
                        Console.WriteLine("No record to update found");
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidCastException || ex is SqlException)
                    {
                        Console.WriteLine("Error executing query: " + ex.Message);
                    }
                    else
                    {
                        throw ex;
                    }
                } */
                /* // delete below - ask user for record ID and delete it
                try
                {
                    Console.Write("DELETING: Enter existing record id: ");
                    int id = int.Parse(Console.ReadLine());
                    SqlCommand cmdDelete = new SqlCommand("DELETE FROM People WHERE Id=@Id", conn);
                    cmdDelete.Parameters.AddWithValue("Id", id);
                    int rowsAffected = cmdDelete.ExecuteNonQuery();
                    if (rowsAffected >= 1)
                    {
                        Console.WriteLine("Record deleted");
                    }
                    else
                    {
                        Console.WriteLine("No record to delete found");
                    }
                }
                catch (Exception ex)
                {
                    if (ex is InvalidCastException || ex is SqlException)
                    {
                        Console.WriteLine("Error executing query: " + ex.Message);
                    }
                    else
                    {
                        throw ex;
                    }
                } */
            } catch (Exception ex)
            {                
                if (ex is SqlException || ex is InvalidOperationException)
                {
                    Console.WriteLine("Error connecting to database: " + ex.Message);
                } else
                { // we MUST throw exception we didn't handle
                    throw ex;
                }
            } finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
