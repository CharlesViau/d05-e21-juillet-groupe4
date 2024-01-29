using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using TP_BLL;
using TP_Model;

namespace TP_View
{
    public partial class RegisterForm : Window
    {
        public readonly SolidColorBrush InvalidColor = new SolidColorBrush(Colors.Crimson);
        public readonly SolidColorBrush ValidColor = new SolidColorBrush(Colors.Green);

        //Minimum 4 characters and max 20, only a-z, A-Z, "_" and "-" allowed
        private readonly Regex usernameRegex = new Regex(@"^(?=.{4,20}$)[a-zA-Z0-9-_]+$");

        //Minimum 8 characters, at least 1 uppercase letter, 1 lowercase letter and one number
        private readonly Regex passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");


        public RegisterForm()
        {
            InitializeComponent();
            DefaultInitialization();
            Show();
        }

        private void DefaultInitialization()
        {
            BirthdayPicker.DisplayDateStart =
                new DateTime(DateTime.Now.Year - 120, DateTime.Now.Month, DateTime.Now.Day);
            BirthdayPicker.DisplayDateEnd = DateTime.Now;
            BirthdayPicker.SelectedDate = DateTime.Now;
        }

        private void ChangeForeground(bool isValid, params Label[] labels)
        {
            foreach (var label in labels)
            {
                label.Foreground = isValid ? ValidColor : InvalidColor;
            }
        }

        private bool ConfirmPasswordCheck()
        {
            return PasswordBox.Password.Equals(ConfirmPasswordBox.Password);
        }

        private void OnUsernameLostKeyBoardFocusEvent(object sender, KeyboardFocusChangedEventArgs e)
        {
            ChangeForeground(usernameRegex.IsMatch(UsernameTextBox.Text), UsernameLabel);
        }

        private void OnPasswordLostKeyBoardFocusEvent(object sender, KeyboardFocusChangedEventArgs e)
        {
            ChangeForeground(passwordRegex.IsMatch(PasswordBox.Password), PasswordLabel);
        }

        private void OnConfirmPasswordLostKeyBoardFocusEvent(object sender, KeyboardFocusChangedEventArgs e)
        {
            ChangeForeground(ConfirmPasswordCheck() && passwordRegex.IsMatch(PasswordBox.Password), 
                PasswordLabel, ConfirmPasswordLabel); 
        }

        private void OnRegisterButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (!usernameRegex.IsMatch(UsernameTextBox.Text))
            {
                MessageBox.Show("Invalid username - Minimum 4 characters Maximum 20, only A-Z, a-z, \"-\" and \"_\" allowed");
            }
            else if (!ConfirmPasswordCheck())
            {
                MessageBox.Show("Invalid password - Passwords must be identical");
            }
            else if (!passwordRegex.IsMatch(PasswordBox.Password))
            {
                MessageBox.Show("Invalid password - Minimum 8 characters, at least 1 uppercase letter, 1 lowercase letter and 1 number");
            }
            else if (!UserManager.RegisterNewUser(new User
            (
                UsernameTextBox.Text,
                PasswordBox.Password,
                BirthdayPicker.SelectedDate.Value
            )))
            {
                MessageBox.Show("Invalid username - Username already taken");
            }
            else
            {
                new MainWindow();
                Close();
            }
        }

        private void OnBackButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            new MainWindow();
            Close();
        }
    }
}
