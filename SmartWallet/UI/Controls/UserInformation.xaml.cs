using SmartWallet.Aplication.Services;
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
using SmartWallet.Aplication.Navigator;

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
            tb_LastName.Text = user.LastName;
            tb_Phone.Text = user.Phone;
            tb_Email.Text = user.Email;
            tb_Password.Text = new string('*', user.Password.Length);
        }

        private void btn_Logout_Click(object sender, RoutedEventArgs e)
        {
            NavigatorObject.Switch(new LoginWindow());
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            border_Cancel.Visibility = Visibility.Visible;
            border_Confirm.Visibility = Visibility.Visible;

            tb_Name.IsReadOnly = false;
            tb_LastName.IsReadOnly = false;
            tb_Phone.IsReadOnly = false;
            tb_Email.IsReadOnly = false;
            tb_Password.IsReadOnly = false;
            tb_Password.Text = UserProvider.GetUserById(UserId).Password;
        }

        private void CloseEditingMode()
        {
            tb_Name.IsReadOnly = true;
            tb_LastName.IsReadOnly = true;
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

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            bool isOkay = true;

            string name = tb_Name.Text;
            string lastname = tb_LastName.Text;
            string email = tb_Email.Text;
            string phone = tb_Phone.Text;
            string password = tb_Password.Text;

            if (string.IsNullOrWhiteSpace(name))
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
            if (DataValidatorService.IsValidPhoneNumber(phone) == false)
            {
                tb_Phone.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                tb_Password.BorderBrush = new SolidColorBrush(Colors.Red);
                isOkay = false;
            }

            if (isOkay == false)
            {
                return;
            }

            User editedUser = UserProvider.GetUserById(UserId);

            editedUser.Name = name;
            editedUser.LastName = lastname;
            editedUser.Phone = phone;
            editedUser.Email = email;
            editedUser.Password = password;
            

            UserProvider.UpdateUser(editedUser);
            CloseEditingMode();
            UpdateInfo();
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null)
            {
                textBox.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }
    }
}
