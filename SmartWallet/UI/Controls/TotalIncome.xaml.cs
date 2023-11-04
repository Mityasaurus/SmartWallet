using System;
using System.Windows.Controls;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalIncome.xaml
    /// </summary>
    public partial class TotalIncome : UserControl
    {
        public int CardId { get; set; }

        public TransactionProvider TransactionProvider;
        
        public TotalIncome()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            SetIncome();
        }

        private void SetIncome()
        {
            if (TransactionProvider == null) return;
            var income = TransactionProvider.GetIncomeByMonth(DateTime.Now, CardId);
            tb_Income.Text = $"{income:N2}";
        }
    }
}
