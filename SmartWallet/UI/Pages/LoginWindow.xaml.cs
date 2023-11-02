using System;
using System.Globalization;
using System.Threading.Tasks;
using SmartWallet.Providers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
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

        private async void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string email = tb_Email.Text;
            string password = tb_Password.Password;
            
            // animation start
            LoadingPanel.Visibility = Visibility.Visible;
            LoadingAnimation.PlayAnimation();

            await Task.Run(() => CheckLogin(email, password));
            
            // animation stop
            LoadingPanel.Visibility = Visibility.Hidden;
            LoadingAnimation.StopAnimation();
        }

        private async Task CheckLogin(string email, string password)
        {
            UserProvider userProvider = new UserProvider();

            string message = userProvider.CheckLogin(email, password);

            if (message != "")
            {
                Application.Current.Dispatcher.Invoke(() => RaiseError(message));
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => LoginSuccess(new HomeScreen(userProvider.GetUserByCredentials(email, password).Id)));
            }
        }

        private void RaiseError(string message)
        {
            // TODO localization
            tb_Error.Text = message;
        }

        private void LoginSuccess(UserControl startPage)
        {
            NavigatorObject.Switch(startPage);
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
