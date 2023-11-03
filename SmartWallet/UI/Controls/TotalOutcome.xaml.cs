using System;
using System.Windows.Controls;
using SmartWallet.Providers;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalOutcome.xaml
    /// </summary>
    public partial class TotalOutcome : UserControl
    {
        public int CardId { get; set; }
        public TransactionProvider TransactionProvider;
        
        public TotalOutcome()
        {
            InitializeComponent();
        }

        public void Refresh()
        {
            SetOutcome();
        }

        private void SetOutcome()
        {
            if (TransactionProvider == null) return;
            var outcome = TransactionProvider.GetOutcomeByMonth(DateTime.Now, CardId);
            tb_Outcome.Text = $"{outcome:f2}";
        }
    }
}
