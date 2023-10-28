using System;
using SmartWallet.Aplication.Services;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        // private UserProvider _userProvider; 

        public RegistrationWindow()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            UserProvider userProvider = new UserProvider();
            
            bool isOkay = true;

            string name = tb_Name.Text;
            string lastname = tb_LastName.Text;
            string email = tb_Email.Text;
            string phone = tb_Phone.Text;
            string password = tb_Password.Password;

            if(string.IsNullOrWhiteSpace(name))
            {
                tb_Name.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if (string.IsNullOrWhiteSpace(lastname))
            {
                tb_LastName.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if (DataValidatorService.IsValidEmail(email) == false)
            {
                tb_Email.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if(DataValidatorService.IsValidPhoneNumber(phone) == false)
            {
                tb_Phone.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if(string.IsNullOrWhiteSpace(password))
            {
                tb_Password.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }

            if(isOkay == false)
            {
                return;
            }

            User newUser = new User()
            {
                Name = name,
                LastName = lastname,
                Email = email,
                Phone = phone,
                Password = password,
                RegistrationDate = DateTime.Now
            };

            string message = userProvider.UserExists(newUser);

            if (message != "0")
            {
                tb_Error.Text = message;
                return;
            }

            userProvider.AddUser(newUser);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            tb_Error.Text = "";

            TextBox textBox = sender as TextBox;

            if(textBox != null)
            {
                textBox.BorderBrush = new SolidColorBrush(Colors.GhostWhite);
            }
            else
            {
                PasswordBox passwordBox = sender as PasswordBox;
                if (passwordBox != null)
                {
                    passwordBox.BorderBrush = new SolidColorBrush(Colors.GhostWhite);
                }
            }
        }
    }
}
