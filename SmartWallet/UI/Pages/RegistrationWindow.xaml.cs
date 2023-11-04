using System;
using System.Globalization;
using SmartWallet.Aplication.Services;
using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using SmartWallet.Aplication.Navigator;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : UserControl
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new LoginWindow());
        }

        private async void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_Name.Text;
            string lastname = tb_LastName.Text;
            string email = tb_Email.Text;
            string phone = tb_Phone.Text;
            string password = tb_Password.Password;
            
            // animation start
            LoadingPanel.Visibility = Visibility.Visible;
            LoadingAnimation.PlayAnimation();
            
            await Task.Run(() => AddUser(name, lastname, email, phone, password));
            
            // animation stop
            LoadingPanel.Visibility = Visibility.Hidden;
            LoadingAnimation.StopAnimation();
        }

        private void EmptyName()
        {
            tb_Name.BorderBrush = new SolidColorBrush(Colors.Red);
            RaiseError(UserStatuses.EmptyFiled);
        }
        
        private void EmptyLastName()
        {
            tb_LastName.BorderBrush = new SolidColorBrush(Colors.Red);
            RaiseError(UserStatuses.EmptyFiled);
        }
        
        private void NotValidEmail()
        {
            tb_Email.BorderBrush = new SolidColorBrush(Colors.Red);
            RaiseError(UserStatuses.EmailNotValid);
        }
        
        private void NotValidPhone()
        {
            tb_Phone.BorderBrush = new SolidColorBrush(Colors.Red);
            RaiseError(UserStatuses.PhoneNotValid);
        }
        
        private void EmptyPassword()
        {
            tb_Password.BorderBrush = new SolidColorBrush(Colors.Red);
            RaiseError(UserStatuses.EmptyFiled);
        }

        private async Task AddUser(string name, string lastname, string email, string phone, string password)
        {
            UserProvider userProvider = new UserProvider();
            
            bool isOkay = true;

            if(string.IsNullOrWhiteSpace(name))
            {
                isOkay = false;
                Application.Current.Dispatcher.Invoke(EmptyName);
            }
            if (string.IsNullOrWhiteSpace(lastname))
            {
                isOkay = false;
                Application.Current.Dispatcher.Invoke(EmptyLastName);
            }
            if (DataValidatorService.IsValidEmail(email) == false)
            {
                isOkay = false;
                Application.Current.Dispatcher.Invoke(NotValidEmail);
            }
            if(DataValidatorService.IsValidPhoneNumber(phone) == false)
            {
                isOkay = false;
                Application.Current.Dispatcher.Invoke(NotValidPhone);
            }
            if(string.IsNullOrWhiteSpace(password))
            {
                isOkay = false;
                Application.Current.Dispatcher.Invoke(EmptyPassword);
            }

            if(isOkay == false) return;
            
            User newUser = new User()
            {
                Name = name,
                LastName = lastname,
                Email = email,
                Phone = phone,
                Password = password,
                RegistrationDate = DateTime.Now
            };

            UserStatuses status = userProvider.UserExists(newUser);

            Application.Current.Dispatcher.Invoke(() => RaiseError(status));

            if (status == UserStatuses.Success)
            {
                userProvider.AddUser(newUser);
                Application.Current.Dispatcher.Invoke(() => NavigatorObject.Switch(new LoginWindow()));
            }
        }

        private void RaiseError(UserStatuses status)
        {
            switch (status)
            {
                case UserStatuses.EmailRegistered:
                    tb_EmailError.Visibility = Visibility.Visible;
                    tb_PhoneError.Visibility = Visibility.Collapsed;
                    tb_EmptyError.Visibility = Visibility.Collapsed;
                    tb_EmptyFieldError.Visibility = Visibility.Collapsed;
                    tb_ValidEmailError.Visibility = Visibility.Collapsed;
                    tb_ValidPhoneError.Visibility = Visibility.Collapsed;
                    break;
                case UserStatuses.PhoneRegistered:
                    tb_EmailError.Visibility = Visibility.Collapsed;
                    tb_PhoneError.Visibility = Visibility.Visible;
                    tb_EmptyError.Visibility = Visibility.Collapsed;
                    tb_EmptyFieldError.Visibility = Visibility.Collapsed;
                    tb_ValidEmailError.Visibility = Visibility.Collapsed;
                    tb_ValidPhoneError.Visibility = Visibility.Collapsed;
                    break;
                case UserStatuses.EmptyFiled:
                    tb_EmailError.Visibility = Visibility.Collapsed;
                    tb_PhoneError.Visibility = Visibility.Collapsed;
                    tb_EmptyError.Visibility = Visibility.Collapsed;
                    tb_EmptyFieldError.Visibility = Visibility.Visible;
                    tb_ValidEmailError.Visibility = Visibility.Collapsed;
                    tb_ValidPhoneError.Visibility = Visibility.Collapsed;
                    break;
                case UserStatuses.EmailNotValid:
                    tb_EmailError.Visibility = Visibility.Collapsed;
                    tb_PhoneError.Visibility = Visibility.Collapsed;
                    tb_EmptyError.Visibility = Visibility.Collapsed;
                    tb_EmptyFieldError.Visibility = Visibility.Collapsed;
                    tb_ValidEmailError.Visibility = Visibility.Visible;
                    tb_ValidPhoneError.Visibility = Visibility.Collapsed;
                    break;
                case UserStatuses.PhoneNotValid:
                    tb_EmailError.Visibility = Visibility.Collapsed;
                    tb_PhoneError.Visibility = Visibility.Collapsed;
                    tb_EmptyError.Visibility = Visibility.Collapsed;
                    tb_EmptyFieldError.Visibility = Visibility.Collapsed;
                    tb_ValidEmailError.Visibility = Visibility.Collapsed;
                    tb_ValidPhoneError.Visibility = Visibility.Visible;
                    break;
                default:
                    tb_EmailError.Visibility = Visibility.Collapsed;
                    tb_PhoneError.Visibility = Visibility.Collapsed;
                    tb_EmptyError.Visibility = Visibility.Visible;
                    tb_EmptyFieldError.Visibility = Visibility.Collapsed;
                    tb_ValidEmailError.Visibility = Visibility.Collapsed;
                    tb_ValidPhoneError.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
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
