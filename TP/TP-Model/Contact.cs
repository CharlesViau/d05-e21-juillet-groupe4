using System;

namespace TP_Model
{
    public class Contact
    {
        public const int NameCharLimit = 50;
        public const int AddressCharLimit = 100;
        public const int EmailCharLimit = 100;
        public const int PhoneNumberCharLimit = 14;
        
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime? Birthday { get; set; }
        public Guid UserID { get; set; }


        public Contact(string firstName, string lastName, string address, string email, string phoneNumber, string phoneNumber2, bool isFavorite, DateTime? birthday, Guid userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            PhoneNumber2 = phoneNumber2;
            IsFavorite = isFavorite;
            Birthday = birthday;
            UserID = userId;
        }

        public Contact(Guid contactId, string firstName, string lastName, string address, string email, string phoneNumber, string phoneNumber2, bool isFavorite, DateTime? birthday, Guid userId)
        {
            Id = contactId;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            Email = email;
            PhoneNumber = phoneNumber;
            PhoneNumber2 = phoneNumber2;
            IsFavorite = isFavorite;
            Birthday = birthday;
            UserID = userId;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
