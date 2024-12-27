using System;
using System.Collections.Generic;
using Literary_Map.Models;
using Microsoft.Data.Sqlite;

namespace Literary_Map.Services
{
    public class DatabaseService
    {
        private string _connectionString = "Data Source=C:\\Users\\HP\\source\\repos\\Literary_Map\\Literary_Map\\dbas.db";

        public User GetUserByUsername(string username)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM User WHERE Username = @username";
                command.Parameters.AddWithValue("@username", username);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                    }
                }
            }
            return null;
        }
        public List<Book> SearchBooks(string query)
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Book WHERE Title LIKE @query OR Author LIKE @query";
                command.Parameters.AddWithValue("@query", $"%{query}%");

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Year = reader.GetString(3),
                            Genre = reader.GetString(4),
                            Description = reader.GetString(5),
                            Longitude = reader.GetDouble(6),
                            Latitude = reader.GetDouble(7)
                        });
                    }
                }
            }
            return books;
        }



        // Получение пользователя по email
        public User GetUserByEmail(string email)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM User WHERE Email = @email";
                command.Parameters.AddWithValue("@email", email);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                    }
                }
            }
            return null;
        }

        // Добавление нового пользователя

        public bool RegisterUser(User user)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Проверка, существует ли пользователь с таким email
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = "SELECT COUNT(*) FROM User WHERE Email = @email";
                checkCommand.Parameters.AddWithValue("@email", user.Email);
                var count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count > 0)
                {
                    return false; // Пользователь уже существует
                }

                // Добавление нового пользователя
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO User (Username, Email, Password) VALUES (@username, @email, @password)";
                insertCommand.Parameters.AddWithValue("@username", user.Username);
                insertCommand.Parameters.AddWithValue("@email", user.Email);
                insertCommand.Parameters.AddWithValue("@password", user.Password);
                insertCommand.ExecuteNonQuery();

                return true; // Регистрация успешна
            }
        }

        // Получение всех книг
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Book";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Year = reader.GetString(3),
                            Genre = reader.GetString(4),
                            Description = reader.GetString(5),
                            Longitude = reader.GetDouble(6),
                            Latitude = reader.GetDouble(7)
                        });
                    }
                }
            }
            return books;

        }

        // Получение избранных книг пользователя
        public List<Book> GetFavoriteBooks(int userId)
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                SELECT b.* FROM Book b
                JOIN FavoriteBooks fb ON b.Id = fb.BookId
                WHERE fb.UserId = @userId";
                command.Parameters.AddWithValue("@userId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Author = reader.GetString(2),
                            Year = reader.GetString(3),
                            Genre = reader.GetString(4),
                            Description = reader.GetString(5),
                            Longitude = reader.GetDouble(6),
                            Latitude = reader.GetDouble(7)
                        });
                    }
                }
            }
            return books;
        }

        // Проверка, находится ли книга в избранном
        public bool IsBookInFavorites(int userId, int bookId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM FavoriteBooks WHERE UserId = @userId AND BookId = @bookId";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@bookId", bookId);
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        // Добавление книги в избранное
        public void AddBookToFavorites(int userId, int bookId)
        {

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO FavoriteBooks (UserId, BookId) VALUES (@userId, @bookId)";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@bookId", bookId);
                command.ExecuteNonQuery();
            }
        }



        // Удаление книги из избранного
        public void RemoveBookFromFavorites(int userId, int bookId)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM FavoriteBooks WHERE UserId = @userId AND BookId = @bookId";
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@bookId", bookId);
                command.ExecuteNonQuery();
            }
        }


    }
}

