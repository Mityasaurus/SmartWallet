using System;
using System.Globalization;
using SmartWallet.Aplication.Services;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartWallet.Aplication.Navigator;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : UserControl
    {
        // private UserProvider _userProvider; 

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new LoginWindow());
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

            NavigatorObject.Switch(new LoginWindow());
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

        private void English_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Language = new CultureInfo("en-US");
        }

        private void Ukrainian_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            App.Language = new CultureInfo("uk-UK");
        }
    }
}
