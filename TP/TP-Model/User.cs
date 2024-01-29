using System;
using System.Collections.Generic;

namespace TP_Model
{
    public class User
    {
        public Guid Id { get; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public DateTime Birthday { get; private set; }
        public bool IsAdmin { get; }
        public List<Contact> ContactList { get; set; }

        public User(string username, string password, DateTime birthday)
        {
            Username = username;
            Password = password;
            Birthday = birthday;
            ContactList = new List<Contact>();
        }

        public User(Guid id, string username, string password, DateTime birthday, bool isAdmin, List<Contact> list)
        {
            Id = id;
            Username = username;
            Password = password;
            Birthday = birthday;
            IsAdmin = isAdmin;
            ContactList = list;
        }
    }
}
