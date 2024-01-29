using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TP_Model;
using TP_BLL;

namespace TP_View
{
    /// <summary>
    /// Interaction logic for ModifyForm.xaml
    /// </summary>
    public partial class ModifyForm : Window
    {
        Guid UserID { get; }
        Contact TempContact { get; set; }

        public ModifyForm(Guid userID, Contact contact)
        {
            InitializeComponent();
            UserID = userID;
            TempContact = contact;
            FirstNameTextBox.Text = contact.FirstName;
            LastNameTextBox.Text = contact.LastName;
            AddressTextBox.Text = contact.Address;
            EmailTextBox.Text = contact.Email;
            PhoneNumberTextBox.Text = contact.PhoneNumber;
            PhoneNumber2TextBox.Text = contact.PhoneNumber2;
            BirthdayPicker.SelectedDate = contact.Birthday;
            FavoriteCheckBox.IsChecked = contact.IsFavorite;
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            new ContactListWindow(this.UserID).Show();
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckName() && CheckPhones())
            {
                TempContact.FirstName = FirstNameTextBox.Text;
                TempContact.LastName = LastNameTextBox.Text;
                TempContact.Address = AddressTextBox.Text;
                TempContact.Email = EmailTextBox.Text;
                TempContact.PhoneNumber = PhoneNumberTextBox.Text;
                TempContact.PhoneNumber2 = PhoneNumber2TextBox.Text;
                TempContact.IsFavorite = (bool)FavoriteCheckBox.IsChecked;
                TempContact.Birthday = BirthdayPicker.SelectedDate;

                ContactManager.Modify(TempContact);
                
                new ContactListWindow(this.UserID).Show();
                Close();

            }
        }
    }
}
