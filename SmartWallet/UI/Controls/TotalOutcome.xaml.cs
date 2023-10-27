using System.Windows.Controls;

namespace SmartWallet.UI.Controls
{
    /// <summary>
    /// Interaction logic for TotalOutcome.xaml
    /// </summary>
    public partial class TotalOutcome : UserControl
    {
        private int cardId;
        public int CardId
        {
            get => cardId;
            set
            {
                cardId = value;
                SetOutcome();
            }
        }
        public TotalOutcome()
        {
            InitializeComponent();
        }

        private void SetOutcome()
        {
            // var outcome = CardProvider.GetOutcomeByMonth(DateTime.Now.Month, cardId);
            // tb_Outcome.Text = $"{outcome:f2}";
        }
    }
}
