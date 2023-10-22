using Microsoft.EntityFrameworkCore;
using SmartWallet.DAL;
using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using SmartWallet.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private UserProvider _userProvider; 

        public RegistrationWindow()
        {
            InitializeComponent();

            SmartWalletContext context = new SmartWalletContext();

            IRepository<User> userRepository = new Repository<User>(context);

            _userProvider = new UserProvider(userRepository);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            //TODO Redirection to login window
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
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
            if (IsValidEmail(email) == false)
            {
                tb_Email.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if(IsValidPhoneNumber(phone) == false)
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
                Password = password
            };

            string message = _userProvider.UserExists(newUser);

            if (message != "0")
            {
                tb_Error.Text = message;
                return;
            }

            _userProvider.AddUser(newUser);

            //TODO Redirection to login window
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(email);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\+\d{10,}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
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
