using SmartWallet.DAL.Entity;
using SmartWallet.DAL.Repository;
using SmartWallet.DAL;
using SmartWallet.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserProvider _userProvider;

        public LoginWindow()
        {
            InitializeComponent();

            SmartWalletContext context = new SmartWalletContext();

            IRepository<User> userRepository = new Repository<User>(context);

            _userProvider = new UserProvider(userRepository);
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
            string email = tb_Email.Text;
            string password = tb_Password.Password;

            string message = _userProvider.CheckLogin(email, password);

            if (message != "")
            {
                tb_Error.Text = message;
                return;
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
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
