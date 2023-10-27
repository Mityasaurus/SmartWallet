using System.Windows.Controls;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalIncome.xaml
    /// </summary>
    public partial class TotalIncome : UserControl
    {
        private int cardId;
        public int CardId
        {
            get => cardId;
            set
            {
                cardId = value;
                SetIncome();
            }
        }
        public TotalIncome()
        {
            InitializeComponent();
        }

        private void SetIncome()
        {
            // var income = CardProvider.GetIncomeByMonth(DateTime.Now.Month, cardId);
            // tb_Income.Text = $"{income:f2}";
        }
    }
}
