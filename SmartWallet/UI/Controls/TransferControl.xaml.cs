using SmartWallet.DAL.Entity;
using SmartWallet.Providers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartWallet.UI.Pages
{
    /// <summary>
    /// Interaction logic for NewCardWindow.xaml
    /// </summary>
    public partial class TransferControl : UserControl
    {
        public static CardProvider CardProvider;
        public static TransactionProvider TransactionProvider;
        public static int CardId;
        // public int UserId;

        public delegate void MainWindowHandler();
        public event MainWindowHandler CloseTransferControl;

        public event MainWindowHandler UpdateUI; // Temporary solution

        public TransferControl()
        {
            InitializeComponent();
        }

        private void Number_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDigitKey(e.Key) && e.Key != Key.Back && e.Key != Key.Delete && e.Key != Key.Right && e.Key != Key.Left) e.Handled = true;
        }
        
        private void Amount_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string text = tb_Amount.Text;
            
            if (e.Key == Key.OemComma && (text.Contains(',') || text.Length == 0))
            {
                e.Handled = true;
            }
            else if (!IsDigitKey(e.Key) && e.Key != Key.Back && e.Key != Key.Delete && e.Key != Key.Right && e.Key != Key.Left && e.Key != Key.OemComma)
            {
                e.Handled = true;
            }
        }

        private bool IsDigitKey(Key key)
        {
            return (key >= Key.D0 && key <= Key.D9) || (key >= Key.NumPad0 && key <= Key.NumPad9);
        }
        
        private int CountDigits(string text)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsDigit(c))
                {
                    count++;
                }
            }

            return count;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            tb_Number.Text = "";
            tb_Amount.Text = "";
            ErrorMoney.Visibility = Visibility.Collapsed;
            ErrorEmptyMoney.Visibility = Visibility.Collapsed;
            ErrorNumber.Visibility = Visibility.Collapsed;
            ErrorOther.Visibility = Visibility.Collapsed;
            CloseTransferControl?.Invoke();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (CardProvider == null || TransactionProvider == null || CardId == null) return;
            
            ErrorMoney.Visibility = Visibility.Collapsed;
            ErrorEmptyMoney.Visibility = Visibility.Collapsed;
            ErrorNumber.Visibility = Visibility.Collapsed;
            ErrorOther.Visibility = Visibility.Collapsed;

            if (!CardProvider.DoesCardExist(tb_Number.Text))
            {
                ErrorNumber.Visibility = Visibility.Visible;
                return;
            }

            if (!CardProvider.DoesCardExist(CardId))
            {
                ErrorOther.Visibility = Visibility.Visible;
                return;
            }

            if (tb_Amount.Text == "")
            {
                ErrorEmptyMoney.Visibility = Visibility.Visible;
                return;
            }
            
            if (CardProvider.GetCardById(CardId).Balance < double.Parse(tb_Amount.Text))
            {
                ErrorMoney.Visibility = Visibility.Visible;
                return;
            }
            
            TransactionProvider.AddNewTransaction(CardProvider.GetCardById(CardId).Number, 
                tb_Number.Text, 
                double.Parse(tb_Amount.Text));
            CloseTransferControl?.Invoke();
            UpdateUI?.Invoke();
        }
    }
}
