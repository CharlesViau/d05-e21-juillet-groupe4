using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TP_BLL;
using TP_Model;

namespace TP_View
{
    public partial class ContactListWindow : Window
    {
        private Guid UserID { get; }
        private List<Contact> Contacts { get; set; }

        public ContactListWindow(Guid userId)
        {
            UserID = userId;
            InitializeComponent();
            DefaultInitialization();
            Show();
        }

        private void DefaultInitialization()
        {
            ContactListBoxInitilization();
            SearchByComboBox.Items.Add("Name");
            SearchByComboBox.Items.Add("Email");
            SearchByComboBox.Items.Add("Favorites");
            SearchByComboBox.SelectedItem = "Name";
        }

        private void ContactListBoxInitilization()
        {
            Contacts = ContactManager.GetContactsBy(UserID);
            Contacts = Contacts.OrderBy(contact => contact.FirstName).ToList();
            RefreshListBox(Contacts);
        }


        private void RefreshListBox(List<Contact> list)
        {
            ContactListBox.Items.Clear();
            switch (SearchByComboBox.SelectedItem)
            {
                case "Name":
                    list = list.OrderBy(contact => contact.FirstName).ToList();
                    break;
                case "Email":
                    list = list.Where(contact => !string.IsNullOrEmpty(contact.Email)).OrderBy(contact => contact.Email).ToList();
                    break;
                case "Favorites":
                    list = list.Where(contact => contact.IsFavorite == true).OrderBy(contact => contact.FirstName).ToList();
                    break;
                default:
                    break;
            }

            foreach (var contact in list)
            {
                ContactListBox.Items.Add(contact);
            }
        }

        private void OnAddButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            new AddContactForm(UserID);
            Close();
        }

        private void OnLookButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (ContactListBox.SelectedItem == null) return;
        }

        private void OnEditButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (ContactListBox.SelectedItem == null) return;
            new ModifyForm(UserID, (Contact)ContactListBox.SelectedItem);
            Close();
        }

        private void OnDeleteButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (ContactListBox.SelectedItem == null) return;
            ContactManager.Delete((Contact)ContactListBox.SelectedItem);
            Contacts.Remove((Contact)ContactListBox.SelectedItem);
            RefreshListBox(Contacts);
        }

        private void OnQuitButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            Close();
        }


        private void SearchByTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ContactListBox.Items.Clear();
            List<Contact> TempContactList = new List<Contact>();
            if (string.IsNullOrEmpty(SearchByTextBox.Text)) RefreshListBox(Contacts);

            switch (SearchByComboBox.SelectedItem)
            {
                case "Name":
                    ContactManager.SearchByName(SearchByTextBox.Text, Contacts, TempContactList);
                    break;
                case "Email":
                    ContactManager.SearchByEmail(SearchByTextBox.Text, Contacts, TempContactList);
                    break;
                case "Favorites":
                    ContactManager.SearchByIsFavorite(SearchByTextBox.Text, Contacts, TempContactList);
                    break;
                default:
                    break;
            }

            RefreshListBox(TempContactList);
        }

        private void SearchByComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            RefreshListBox(Contacts);
            SearchByTextBox_TextChanged(null, null);
        }
    }
}
