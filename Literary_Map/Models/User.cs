using System.Collections.Generic;

namespace Literary_Map.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<FavoriteBooks> FavoriteBooks { get; set; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public User()
        {
            FavoriteBooks = new List<FavoriteBooks>();
        }
    }
}
