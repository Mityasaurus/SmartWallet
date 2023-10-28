using SmartWallet.Providers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartWallet
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
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
                MainWindow mainWindow = new MainWindow(userProvider.GetUserByCredentials(email, password).Id);
                mainWindow.Show();
                this.Close();
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }
    }
}
