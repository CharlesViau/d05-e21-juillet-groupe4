using System;
using System.Windows;
using System.Windows.Input;
using TP_BLL;

namespace TP_View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Show();
        }

        private void OnLoginButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            if (UserManager.VerifyLoginPassword(UsernameTextBox.Text, PasswordBox.Password))
            {
                new ContactListWindow(UserManager.GetId(UsernameTextBox.Text));
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        private void OnRegisterButtonLeftClickEvent(object sender, MouseButtonEventArgs e)
        {
            new RegisterForm();
            Close();
        }
    }
}
