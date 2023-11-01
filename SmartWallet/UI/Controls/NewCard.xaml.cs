using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartWallet.UI.Pages
{
    /// <summary>
    /// Interaction logic for NewCardWindow.xaml
    /// </summary>
    public partial class NewCardWindow : UserControl
    {
        public CardProvider CardProvider;
        public int UserId;

        public delegate void MainWindowHandler();
        public event MainWindowHandler CloseNewCardWindow;

        public event MainWindowHandler UpdateUI; // Temporary solution

        public NewCardWindow()
        {
            InitializeComponent();

            cmbBox_Currencies.ItemsSource = Enum.GetNames(typeof(Currency)).Take(168);
            cmbBox_Currencies.SelectedIndex = 154;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string text = tb_Balance.Text;

            if (e.Key == Key.OemComma && (text.Contains(',') || text.Length == 0))
            {
                e.Handled = true;
            }
            else if (!IsDigitKey(e.Key) && e.Key != Key.Back && e.Key != Key.OemComma)
            {
                e.Handled = true;
            }
        }

        private bool IsDigitKey(Key key)
        {
            return (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            cmbBox_Currencies.SelectedIndex = 170;
            tb_Balance.Text = "";
            rdBtn_Money.IsChecked = true;
            CloseNewCardWindow?.Invoke();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            string balanceString = tb_Balance.Text != "" ? tb_Balance.Text : "0";
            double balance = double.Parse(balanceString);

            string cardNumber = CardProvider.GenerateNewNumber();

            string cvv = CardProvider.GenerateCVV();

            DateTime dateExpire = CardProvider.GenerateDateExpire();

            string type = "";

            if(rdBtn_Money.IsChecked == true)
            {
                type = "Money";
            }
            else if(rdBtn_Crypto.IsChecked == true)
            {
                type = "Crypto";
            }

            Currency currency;

            if(Enum.TryParse(cmbBox_Currencies.SelectedItem.ToString(), out currency) == false)
            {
                return;
            }

            CardProvider.AddNewCard(cardNumber, dateExpire, cvv, type, currency, balance, UserId);

            CloseNewCardWindow?.Invoke();
            UpdateUI?.Invoke();
        }

        private void rdBtn_Money_Checked(object sender, RoutedEventArgs e)
        {
            cmbBox_Currencies.ItemsSource = Enum.GetNames(typeof(Currency)).Take(168);
            cmbBox_Currencies.SelectedIndex = 154;
        }

        private void rdBtn_Crypto_Checked(object sender, RoutedEventArgs e)
        {
            cmbBox_Currencies.ItemsSource = Enum.GetNames(typeof(Currency)).Skip(168);
            cmbBox_Currencies.SelectedIndex = 0;
        }
    }
}
