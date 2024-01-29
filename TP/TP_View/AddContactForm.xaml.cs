using System;
using System.Text.RegularExpressions;
using System.Windows;
using TP_BLL;
using TP_Model;

namespace TP_View
{
    public partial class AddContactForm : Window
    {
        private Guid UserID { get; }

        public AddContactForm(Guid userId)
        {
            InitializeComponent();
            UserID = userId;
            FirstNameTextBox.MaxLength = Contact.NameCharLimit;
            LastNameTextBox.MaxLength = Contact.NameCharLimit;
            AddressTextBox.MaxLength = Contact.AddressCharLimit;
            EmailTextBox.MaxLength = Contact.EmailCharLimit;
            PhoneNumberTextBox.MaxLength = Contact.PhoneNumberCharLimit;
            PhoneNumber2TextBox.MaxLength = Contact.PhoneNumberCharLimit;
            Show();
        }

        private bool CheckName()
        {
            if (FirstNameTextBox.Text.Equals("") || LastNameTextBox.Text.Equals(""))
            {
                MessageBox.Show("First name and last name fields must not be empty");
                return false;
            }
            else return true;
        }

        private bool CheckPhones()
        {
            if (CheckPhoneNumber(PhoneNumberTextBox.Text) && CheckPhoneNumber(PhoneNumber2TextBox.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Phone number format isn't valid");
                return false;
            }
        }

        private static bool CheckPhoneNumber(string number)
        {

            return string.IsNullOrEmpty(number) ? true : Regex.Match(number, @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$").Success;
        }


        private void OnAddButtonLeftClickEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CheckName() && CheckPhones())
            {
                ContactManager.AddContact(new Contact
                (
                    FirstNameTextBox.Text,
                    LastNameTextBox.Text,
                    AddressTextBox.Text,
                    EmailTextBox.Text,
                    PhoneNumberTextBox.Text,
                    PhoneNumber2TextBox.Text,
                    (bool)FavoriteCheckBox.IsChecked,
                    BirthdayPicker.SelectedDate,
                    UserID
                ));
                new ContactListWindow(this.UserID).Show();
                Close();
               
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            new ContactListWindow(this.UserID).Show();
            Close();
        }
    }
}
