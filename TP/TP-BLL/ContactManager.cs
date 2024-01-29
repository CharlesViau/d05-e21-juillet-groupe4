using System;
using System.Collections.Generic;
using TP_Model;
using TP_DAL;
using System.Linq;

namespace TP_BLL
{
    public class ContactManager
    {
        public static List<Contact> GetContactsBy(Guid userId)
        {
            return ContactServices.GetContactsBy(userId);
        }

        public static void AddContact(Contact contact)
        {
            ContactServices.Add(contact);
        }

        public static void Delete(Contact contact)
        {
            ContactServices.Delete(contact);
        }

        public static void Modify(Contact contact)
        {
            ContactServices.Modify(contact);
        }

        public static void SearchByName(string name, List<Contact> list, List<Contact> tempList)
        {
            foreach (var contact in list)
            {
                if (contact.FirstName.ToLower().Contains(name.ToLower())|| contact.LastName.ToLower().Contains(name.ToLower()))
                {
                    tempList.Add(contact);
                }
            }
        }

        public static void SearchByEmail(string email, List<Contact> list, List<Contact> tempList)
        {
            foreach (var contact in list)
            {
                if(contact.Email != null && contact.Email.ToLower().Contains(email.ToLower()))
                {
                    tempList.Add(contact);
                }
            }
        }

        public static void SearchByIsFavorite(string name, List<Contact> list, List<Contact> tempList)
        {
            foreach (var contact in list)
            {
                if(contact.IsFavorite && (contact.FirstName.ToLower().Contains(name.ToLower()) || contact.LastName.ToLower().Contains(name.ToLower())))
                {
                    tempList.Add(contact);
                }
            }
        }
    }
}
