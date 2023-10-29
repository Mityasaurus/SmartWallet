using System;
using System.Globalization;
using SmartWallet.Providers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SmartWallet.Aplication.Navigator;
using SmartWallet.UI.Pages;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : UserControl
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            tb_Error.Text = "";

            TextBox textBox = sender as TextBox;

            if (textBox != null)
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

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            UserProvider userProvider = new UserProvider();
            string email = tb_Email.Text;
            string password = tb_Password.Password;

            string message = userProvider.CheckLogin(email, password);

            if (message != "")
            {
                tb_Error.Text = message;
                return;
            }
            else
            {
                NavigatorObject.Switch(new HomeScreen(userProvider.GetUserByCredentials(email, password).Id));
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new RegistrationWindow());
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
