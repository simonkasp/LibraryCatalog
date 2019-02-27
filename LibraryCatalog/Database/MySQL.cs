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
        public void AddDataUser<T>(T user) where T : IBaseUser
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

        public int CheckUserRole(string username)
        {
            Connect();

            var command = connection.CreateCommand();

            command.CommandText = "SELECT userRole from librarycatalog.users WHERE username=@username";
            command.Parameters.AddWithValue("@username", username);

            var userRole = command.ExecuteScalar();

            connection.Close();

            return Convert.ToInt32(userRole);

        }
    }
}
