using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using LibraryCatalog.Books;
using LibraryCatalog.Users;
using System.ComponentModel.DataAnnotations;

namespace LibraryCatalog.Database
{
    public class MySQL : IMySQL
    {
        private string db;
        private MySqlConnection connection;

        private void Connect()
        {
            db = "datasource=localhost;port=3306;username=root;password=;";
            connection = new MySqlConnection(db);
            connection.Open();
        }

        public void AddDataBook(Book book)
        {
            try
            {
                Connect();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO librarycatalog.books(title, numberOfPages, ISBN, isCheckedOut, isReserved) VALUES(@title, @numOfPages, @ISBN, @isCheckedOut, @isReserved)";
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@numOfPages", book.NumberOfPages);
                command.Parameters.AddWithValue("@ISBN", book.ISBN);
                command.Parameters.AddWithValue("@isCheckedOut", "false");
                command.Parameters.AddWithValue("@isReserved", "false");
                command.ExecuteNonQuery();

                Console.WriteLine("Book has been succesfully added.");
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddDataUser<T>(T user) where T : IRegularUser
        {
            try
            {
                var connection = new MySqlConnection(db);
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO librarycatalog.users(username, firstName, lastName, password, registredDate) VALUES(@username, @firstName, @lastName, @password, @registredDate)";
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@firstName", user.Name);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@registredDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine("Registration completed!");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool CheckDataIfExists(string query, string data)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@username", data);

                var result = command.ExecuteScalar();

                return result != null ? true : false;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public BaseUser GetDataUser(string username)
        {
            Connect();
            BaseUser user = new BaseUser(); ;
            var command = connection.CreateCommand();

            command.CommandText = "SELECT *  FROM librarycatalog.users WHERE username=@username";
            command.Parameters.AddWithValue("@username", username);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                user.ID = (int)reader["id"];
                user.Username = (string)reader["username"];
                user.Name = (string)reader["firstName"];
                user.LastName = (string)reader["lastName"];
                user.Password = (string)reader["password"];
                user.DateRegistred = (string)reader["registredDate"];
                user.Role = (int)reader["userRole"];
                user.IsLoggedIn = true;
            }

            return user;
        }

        public void DeleteData(string query, int id)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();

                command.CommandText = query;

                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                Console.WriteLine("Record has been deleted.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ShowDataAll(string query)
        {
            try
            {
                Connect();
                var command = connection.CreateCommand();
                command.CommandText = query;

                MySqlDataReader reader = command.ExecuteReader();

                var columns = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                            Console.WriteLine(columns[i] + ": " + reader.GetValue(i));
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ShowDataUser(string query, int id)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);

                var result = command.ExecuteScalar();

                if (result == null)
                {
                    Console.WriteLine("User was not found.");
                }

                MySqlDataReader reader = command.ExecuteReader();


                while (reader.HasRows)
                {

                    var columns = new List<string>();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            columns.Add(reader.GetName(i));
                            Console.WriteLine(columns[i] + ": " + reader.GetValue(i));
                        }
                    }

                    Console.WriteLine();
                    reader.NextResult();
                    Console.WriteLine("Taken books: ");

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine(reader.GetValue(i));
                        }
                    }
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
