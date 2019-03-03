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
        private enum IdentificationType { username, password};

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

        public bool CheckDataIfUsernameExists(string query, string data)
        {
            return CheckDataIfExists(query, IdentificationType.username, data);
        }
        
        public bool CheckDataIfUserExists(string query, string username, string password)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

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

        private bool CheckDataIfExists(string query, IdentificationType identificationType, string data)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@"+identificationType, data);

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

                    PrintWithColumns(reader);
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void ReserveDataBook(int bookID, IRegularUser user)
        {
            try
            {
                Connect();

                var command = connection.CreateCommand();

                command.CommandText = "SELECT id FROM librarycatalog.users WHERE username = @username";
                command.Parameters.AddWithValue("@username", user.Username);
                var id = command.ExecuteScalar();

                command.CommandText = "UPDATE librarycatalog.books SET isReserved = @isReserved, reservedByUserID = @loggedInUserID where id = @id AND isCheckedOut = @isCheckedOut AND isReserved = @CurrentlyIsReserved";
                command.Parameters.AddWithValue("@id", bookID);
                command.Parameters.AddWithValue("@loggedInUserID", id);
                command.Parameters.AddWithValue("@isCheckedOut", "No");
                command.Parameters.AddWithValue("@isReserved", "Yes");
                command.Parameters.AddWithValue("@CurrentlyIsReserved", "No");
                var result = command.ExecuteNonQuery();

                if (result != 0)
                {
                    Console.WriteLine("Book has been reserved!");

                }

                else
                    Console.WriteLine("This book cannot be reserved.");

                
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

        public void CheckInDataBook(int bookID, IRegularUser user)
        {

            try
            {
                Connect();
                var command = connection.CreateCommand();

                command.CommandText = "UPDATE librarycatalog.books SET isCheckedOut = @isCheckedOut, isReserved = @isReserved, takenByUserID = @takenByUserID where id = @id AND isReserved = @isReserved AND isCheckedOut=@CurrentIsCheckedOut AND takenByUserID = @CurrenttakenByUserID";
                command.Parameters.AddWithValue("@id", bookID);
                command.Parameters.AddWithValue("@isCheckedOut", "No");
                command.Parameters.AddWithValue("@CurrentIsCheckedOut", "Yes");
                command.Parameters.AddWithValue("@takenByUserID", 0);
                command.Parameters.AddWithValue("@CurrenttakenByUserID", user.ID);
                command.Parameters.AddWithValue("@isReserved", "No");

                var result = command.ExecuteNonQuery();

                if (result != 0)
                    Console.WriteLine("Book has been returned.");
                else
                    Console.WriteLine("You can't return this book.");

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void CheckOutDataBook(int bookID, IRegularUser user)
        {

            try
            {
                Connect();
                var command = connection.CreateCommand();

                command.CommandText = "SELECT id FROM librarycatalog.users WHERE username=@username";
                command.Parameters.AddWithValue("@username", user.Username);
                var loggedInUserID = command.ExecuteScalar();

                command.CommandText = "UPDATE librarycatalog.books SET isCheckedOut = @isCheckedOut, isReserved = @isReserved, takenByUserID = @takenByUserID, reservedByUserId = @reservedByUserId WHERE id = @id AND isReserved = @isReserved OR reservedByUserID = @takenByUserID";
                command.Parameters.AddWithValue("@id", bookID);
                command.Parameters.AddWithValue("@isCheckedOut", "Yes");
                command.Parameters.AddWithValue("@isReserved", "No");
                command.Parameters.AddWithValue("@takenByUserID", loggedInUserID);
                command.Parameters.AddWithValue("@reservedByUserID", 0);

                var result = command.ExecuteNonQuery();

                if (result != 0)
                    Console.WriteLine("Book is checked out.");
                else
                    Console.WriteLine("Book cannot be checked out");

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void SelectDataRegularUserInfo(IRegularUser user)
        {
            try
            {
                Connect();
                var command = connection.CreateCommand();
                var columns = new List<string>();

                command.CommandText = "SELECT id, title, numberOfPages, ISBN, isCheckedOut, isReserved FROM librarycatalog.books WHERE isReserved = @isReserved AND isCheckedOut = @isCheckedOut";
                command.Parameters.AddWithValue("@isReserved", "No");
                command.Parameters.AddWithValue("@isCheckedOut", "No");

                MySqlDataReader reader = command.ExecuteReader();

                PrintWithColumns(reader);
              
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

        public void SelectDataAvailableBooks(string query)
        {
            Connect();

            var command = connection.CreateCommand();
            var columns = new List<string>();

            command.CommandText = query;
            command.Parameters.AddWithValue("@isReserved", "No");
            command.Parameters.AddWithValue("@isCheckedOut", "No");
            command.ExecuteNonQuery();

            var reader = command.ExecuteReader();

            PrintWithColumns(reader);
        }

        public void SelectDataBooks(string query, string columnName, IRegularUser user)
        {
            Connect();

            var command = connection.CreateCommand();
            var columns = new List<string>();

            command.CommandText = query;
            command.Parameters.AddWithValue("@"+columnName, user.ID);
            command.ExecuteNonQuery();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                PrintWithColumns(reader);
            }
            else
            {
                Console.WriteLine("You did not take any book. \n");
            }
        }

        public void SelectDataBook(string bookName)
        {
            try
            {
                Connect();
                var command = connection.CreateCommand();
                var columns = new List<string>();

                command.CommandText = "SELECT id, title, ISBN, numberOfPages, isCheckedOut, isReserved FROM librarycatalog.books WHERE title LIKE @word";
                command.Parameters.AddWithValue("@word", bookName);

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    PrintWithColumns(reader);
                }
                else
                {
                    Console.WriteLine("Book was not found.");
                }

                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void PrintWithColumns(MySqlDataReader reader)
        {
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

    }
}
