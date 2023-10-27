using SmartWallet.DAL.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for UserInformation.xaml
    /// </summary>
    public partial class UserInformation : UserControl
    {
        public delegate void CloseMainWindowHandler();

        public event CloseMainWindowHandler CloseMainWindow;

        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                UpdateInfo();
            }
        }
        public UserProvider UserProvider;

        public UserInformation()
        {
            InitializeComponent();
        }

        private void UpdateInfo()
        {
            User user = UserProvider.GetUserById(UserId);

            tb_Name.Text = user.Name;
            tb_Lastname.Text = user.LastName;
            tb_Phone.Text = user.Phone;
            tb_Email.Text = user.Email;
            tb_Password.Text = new string('*', user.Password.Length);
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            CloseMainWindow.Invoke();
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            border_Cancel.Visibility = Visibility.Visible;
            border_Confirm.Visibility = Visibility.Visible;

            tb_Name.IsReadOnly = false;
            tb_Lastname.IsReadOnly = false;
            tb_Phone.IsReadOnly = false;
            tb_Email.IsReadOnly = false;
            tb_Password.IsReadOnly = false;
            tb_Password.Text = UserProvider.GetUserById(UserId).Password;
        }

        private void CloseEditingMode()
        {
            tb_Name.IsReadOnly = true;
            tb_Lastname.IsReadOnly = true;
            tb_Phone.IsReadOnly = true;
            tb_Email.IsReadOnly = true;
            tb_Password.IsReadOnly = true;

            tb_Password.Text = new string('*', tb_Password.Text.Length);

            border_Cancel.Visibility = Visibility.Hidden;
            border_Confirm.Visibility = Visibility.Hidden;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            CloseEditingMode();
            UpdateInfo();
        }
    }
}
